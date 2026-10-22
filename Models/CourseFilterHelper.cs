using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CoursesHub.Areas.Panel.Models;

namespace CoursesHub.Models
{
    /// <summary>One training provider on the A-Z Training Providers page.</summary>
    public class ProviderSummary
    {
        public string Name { get; set; }
        public string Logo { get; set; }
        public int CourseCount { get; set; }
        public List<string> StudyAreas { get; set; }
        public List<string> Locations { get; set; }
    }

    /// <summary>
    /// Builds the All Courses sidebar filter values from the course data that is actually listed.
    /// </summary>
    /// <summary>Model for the Find a provider results page.</summary>
    public class FindProviderResult
    {
        public string Course { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public bool IsResident { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public List<CourseDetails> InArea { get; set; } = new List<CourseDetails>();
        public List<CourseDetails> Elsewhere { get; set; } = new List<CourseDetails>();
    }

    public static class CourseFilterHelper
    {
        public class FilterOption
        {
            public string Key { get; set; }
            public string Text { get; set; }
            public int Count { get; set; }
        }

        // Longest names first so "Advanced Diploma" is not read as "Diploma", "Certificate IV" not as "Certificate I", etc.
        private static readonly string[] QualificationNames =
        {
            "Graduate Certificate", "Graduate Diploma", "Advanced Diploma", "Associate Degree",
            "Certificate IV", "Certificate III", "Certificate II", "Certificate I",
            "Bachelor", "Master", "Doctoral", "Diploma"
        };

        public static readonly string[] DurationBands =
        {
            "Less than one Year", "1-2 Years", "2-3 Years", "3-4 Years", "4+ Years"
        };

        public static readonly string[] StudentTypes = { "Domestic Student", "International Student" };

        public static readonly string[] FundingTypes = { "Government Funded", "Fee for Service" };

        /// <summary>
        /// Reads the GovernmentFunding (varchar) column: true = government funded, false = fee for service, null = not set.
        /// Accepts "Yes"/"No", "1"/"0", "true"/"false", "Y"/"N", "Government Funded"/"Fee for Service".
        /// </summary>
        public static bool? FundingFlag(string value)
        {
            string v = (value ?? string.Empty).Trim().ToLowerInvariant();
            if (v.Length == 0) return null;
            switch (v)
            {
                case "yes": case "y": case "1": case "true": case "government funded": case "governmentfunded": case "funded":
                    return true;
                case "no": case "n": case "0": case "false": case "fee for service": case "feeforservice": case "not funded":
                    return false;
                default:
                    return null;
            }
        }

        /// <summary>Value written to the GovernmentFunding column: "Yes", "No" or null when not chosen.</summary>
        public static string FundingToStore(string value)
        {
            bool? flag = FundingFlag(value);
            return flag == null ? null : (flag.Value ? "Yes" : "No");
        }

        /// <summary>
        /// Reads/normalises the StudentType (varchar) column: returns "Domestic", "International" or null when not set.
        /// Accepts "Domestic"/"International", "Domestic Student"/"International Student", "D"/"I", "Local"/"Overseas".
        /// </summary>
        public static string StudentTypeValue(string value)
        {
            string v = (value ?? string.Empty).Trim().ToLowerInvariant();
            if (v.Length == 0) return null;
            if (v == "d" || v == "local" || v.StartsWith("domestic")) return "Domestic";
            if (v == "i" || v == "overseas" || v.StartsWith("international")) return "International";
            return null;
        }

        /// <summary>Filter label for a course's StudentType value ("Domestic Student" / "International Student"); null when not set.</summary>
        public static string StudentTypeLabel(string studentType)
        {
            string v = StudentTypeValue(studentType);
            if (v == null) return null;
            return v == "Domestic" ? StudentTypes[0] : StudentTypes[1];
        }

        /// <summary>
        /// Tidies rich-text course content for display: removes empty paragraphs/headings/list items
        /// (only spaces, &amp;nbsp; or &lt;br&gt;), collapses repeated line breaks and trims leading/trailing breaks.
        /// </summary>
        public static string CleanRichText(string html)
        {
            if (string.IsNullOrWhiteSpace(html)) return string.Empty;
            const string blank = @"(?:\s|&nbsp;|&#160;|&#xa0;| |<br\s*/?>)*";
            string s = html;
            // empty blocks, repeated until nothing changes (handles <p><strong>&nbsp;</strong></p> too)
            string previous;
            do
            {
                previous = s;
                s = Regex.Replace(s, @"<(strong|b|em|i|u|span)[^>]*>" + blank + @"</\1>", " ", RegexOptions.IgnoreCase);
                s = Regex.Replace(s, @"<(p|h[1-6]|li|div)[^>]*>" + blank + @"</\1>", string.Empty, RegexOptions.IgnoreCase);
            } while (s != previous);
            // 2+ line breaks in a row -> one
            s = Regex.Replace(s, @"(?:<br\s*/?>\s*(?:&nbsp;| )?\s*){2,}", "<br>", RegexOptions.IgnoreCase);
            // line breaks right after an opening / before a closing paragraph
            s = Regex.Replace(s, @"(<p[^>]*>)\s*(?:<br\s*/?>\s*)+", "$1", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"(?:\s*<br\s*/?>)+\s*(</p>)", "$1", RegexOptions.IgnoreCase);
            // leading / trailing breaks and spaces
            s = Regex.Replace(s, @"^(?:\s|&nbsp;| |<br\s*/?>)+|(?:\s|&nbsp;| |<br\s*/?>)+$", string.Empty, RegexOptions.IgnoreCase);
            return s;
        }

        /// <summary>Filter label for a course's GovernmentFunding value; null when not set.</summary>
        public static string Funding(string governmentFunding)
        {
            bool? flag = FundingFlag(governmentFunding);
            if (flag == null) return null;
            return flag.Value ? FundingTypes[0] : FundingTypes[1];
        }

        public static string Key(string text)
        {
            return Regex.Replace((text ?? string.Empty).Trim().ToLowerInvariant(), @"\s+", "-");
        }

        /// <summary>Splits a field that may hold several values ("NSW, VIC") into its parts.</summary>
        public static IEnumerable<string> Values(string text)
        {
            return (text ?? string.Empty)
                .Split(new[] { ',', '|', ';', '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .Where(x => x.Length > 0);
        }

        public static string Keys(string text)
        {
            return string.Join("|", Values(text).Select(Key));
        }

        /// <summary>Qualification level taken from the course name (e.g. "CHC33021 Certificate III in ..."), else the course level.</summary>
        public static string Qualification(CourseDetails course)
        {
            string name = course.CourseName ?? string.Empty;
            foreach (var q in QualificationNames)
            {
                if (Regex.IsMatch(name, @"\b" + Regex.Escape(q) + @"\b", RegexOptions.IgnoreCase))
                {
                    return q;
                }
            }
            return string.IsNullOrWhiteSpace(course.CourseLevelName) ? null : course.CourseLevelName.Trim();
        }

        /// <summary>"Masters" (filter menu) and "Master" (from the course name) are the same level.</summary>
        public static bool SameQualification(string a, string b)
        {
            Func<string, string> norm = s => (s ?? string.Empty).Trim().ToLowerInvariant().TrimEnd('s');
            return norm(a).Length > 0 && norm(a) == norm(b);
        }

        /// <summary>Duration band for a course. CourseDuration is stored in weeks.</summary>
        public static string DurationBand(string courseDuration)
        {
            var match = Regex.Match(courseDuration ?? string.Empty, @"\d+(\.\d+)?");
            if (!match.Success)
            {
                return null;
            }

            double value = double.Parse(match.Value, System.Globalization.CultureInfo.InvariantCulture);
            string lower = courseDuration.ToLowerInvariant();
            double weeks = lower.Contains("year") ? value * 52 : lower.Contains("month") ? value * 52 / 12 : value;

            if (weeks < 52) return DurationBands[0];
            if (weeks < 104) return DurationBands[1];
            if (weeks < 156) return DurationBands[2];
            if (weeks < 208) return DurationBands[3];
            return DurationBands[4];
        }

        /// <summary>Distinct values with course counts, for one sidebar group.</summary>
        public static List<FilterOption> Options(IEnumerable<CourseDetails> courses, Func<CourseDetails, IEnumerable<string>> selector)
        {
            return courses
                .SelectMany(c => (selector(c) ?? Enumerable.Empty<string>())
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Select(v => v.Trim())
                    .GroupBy(Key).Select(g => g.First()))
                .GroupBy(Key)
                .Select(g => new FilterOption { Key = g.Key, Text = g.First(), Count = g.Count() })
                .OrderBy(o => o.Text)
                .ToList();
        }

        static readonly Regex CourseCodeRx = new Regex(@"^([A-Z]{2,5}\d{3,6}[A-Z]?)\s+(.+)$", RegexOptions.Compiled);

        /// <summary>"CHC43015 Certificate IV in Ageing Support" -> "CHC43015"; null when the name has no national code.</summary>
        public static string CourseCode(string courseName)
        {
            var m = CourseCodeRx.Match((courseName ?? "").Trim());
            return m.Success ? m.Groups[1].Value : null;
        }

        /// <summary>"CHC43015 Certificate IV in Ageing Support" -> "Certificate IV in Ageing Support".</summary>
        public static string CourseTitle(string courseName)
        {
            var name = (courseName ?? "").Trim();
            var m = CourseCodeRx.Match(name);
            return m.Success ? m.Groups[2].Value : name;
        }

        /// <summary>TrendingCourse (varchar) is set by the admin: "1" = trending, "0" = not trending.</summary>
        public static bool IsTrending(string value)
        {
            string v = (value ?? string.Empty).Trim().ToLowerInvariant();
            return v == "1" || v == "yes" || v == "true";
        }

        public static string TrendingToStore(string value)
        {
            return IsTrending(value) ? "1" : "0";
        }

        static readonly string[] FeaturedPriorities = { "High", "Medium", "Low" };

        /// <summary>FeaturedCourse (varchar) is set by the admin: "High", "Medium" or "Low"; null when the course is not featured.</summary>
        public static string FeaturedPriority(string value)
        {
            string v = (value ?? string.Empty).Trim();
            return FeaturedPriorities.FirstOrDefault(p => string.Equals(p, v, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<string> FeaturedPriorityOptions { get { return FeaturedPriorities; } }

        /// <summary>Featured courses only: High first, then Medium, then Low; newest first within a priority.</summary>
        public static List<CourseDetails> FeaturedCourses(IEnumerable<CourseDetails> courses)
        {
            return (courses ?? Enumerable.Empty<CourseDetails>())
                .Select(c => new { Course = c, Priority = FeaturedPriority(c.FeaturedCourse) })
                .Where(x => x.Priority != null)
                .OrderBy(x => Array.IndexOf(FeaturedPriorities, x.Priority))
                .ThenByDescending(x => x.Course.CreatedDate)
                .Select(x => x.Course)
                .ToList();
        }

        static readonly Dictionary<string, string> StateAbbr = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "NSW", "New South Wales" }, { "VIC", "Victoria" }, { "QLD", "Queensland" }, { "SA", "South Australia" },
            { "WA", "Western Australia" }, { "TAS", "Tasmania" }, { "NT", "Northern Territory" }, { "ACT", "Australian Capital Territory" }
        };

        /// <summary>Australian state for a 4-digit postcode ("5000" -> "South Australia"); null when unknown.</summary>
        public static string StateFromPostcode(string postcode)
        {
            int pc;
            if (string.IsNullOrWhiteSpace(postcode) || !int.TryParse(postcode.Trim(), out pc) || postcode.Trim().Length != 4) return null;
            string abbr =
                (pc >= 200 && pc <= 299) || (pc >= 2600 && pc <= 2618) || (pc >= 2900 && pc <= 2920) ? "ACT" :
                (pc >= 800 && pc <= 999) ? "NT" :
                (pc >= 1000 && pc <= 2999) ? "NSW" :
                (pc >= 3000 && pc <= 3999) || (pc >= 8000 && pc <= 8999) ? "VIC" :
                (pc >= 4000 && pc <= 4999) || (pc >= 9000 && pc <= 9999) ? "QLD" :
                (pc >= 5000 && pc <= 5999) ? "SA" :
                (pc >= 6000 && pc <= 6999) ? "WA" :
                (pc >= 7000 && pc <= 7999) ? "TAS" : null;
            return abbr == null ? null : StateAbbr[abbr];
        }

        /// <summary>"SA" and "South Australia" are the same state.</summary>
        public static bool SameState(string a, string b)
        {
            Func<string, string> full = s => { s = (s ?? "").Trim(); string f; return StateAbbr.TryGetValue(s, out f) ? f : s; };
            return string.Equals(full(a), full(b), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>Rich text as one line of plain text, cut at about <paramref name="max"/> characters.</summary>
        public static string PlainText(string html, int max = 0)
        {
            var text = Regex.Replace(System.Net.WebUtility.HtmlDecode(Regex.Replace(html ?? "", "<.*?>", " ")), @"\s+", " ").Trim();
            if (max > 0 && text.Length > max)
            {
                int cut = text.LastIndexOf(' ', max);
                text = text.Substring(0, cut > max - 25 ? cut : max).TrimEnd(' ', ',', '.', ':', ';') + "…";
            }
            return text;
        }

        /// <summary>
        /// Short job titles from the course outcomes (list items, or lines / commas / semicolons),
        /// for "career opportunities" chips. Empty when the outcomes are a paragraph rather than a list.
        /// </summary>
        public static List<string> CareerChips(string outcomesHtml, int maxChips = 6)
        {
            var html = outcomesHtml ?? "";
            IEnumerable<string> parts;
            var items = Regex.Matches(html, @"<li[^>]*>([\s\S]*?)</li>", RegexOptions.IgnoreCase);
            if (items.Count > 0)
                parts = items.Cast<Match>().Select(m => m.Groups[1].Value);
            else
                parts = Regex.Split(Regex.Replace(html, @"<br\s*/?>|</p>|</div>", "\n", RegexOptions.IgnoreCase), @"[\n,;•]");

            var chips = parts.Select(p => PlainText(p).Trim(' ', '-', '*', '.'))
                             .Where(p => p.Length >= 2)
                             .Distinct(StringComparer.OrdinalIgnoreCase)
                             .ToList();
            // a long sentence is a description, not a job title
            if (chips.Count == 0 || chips.Any(c => c.Length > 45)) return new List<string>();
            return chips.Take(maxChips).ToList();
        }

        /// <summary>
        /// Qualification levels for the header filter menu, built from the listed courses exactly like the
        /// All Courses sidebar "Qualification Level" group (name + course count). Cached for 10 minutes.
        /// </summary>
        public static List<FilterOption> MenuQualificationOptions()
        {
            const string cacheKey = "CH_MenuQualifications";
            var cached = System.Web.HttpRuntime.Cache[cacheKey] as List<FilterOption>;
            if (cached != null) return cached;

            var courses = new CourseDetailsOperation().GetCourseDetails(new CourseDetails(), "");
            var options = Options(courses, c => new[] { Qualification(c) });
            System.Web.HttpRuntime.Cache.Insert(cacheKey, options, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
            return options;
        }

        /// <summary>Course categories for the header search on every page (cached for 10 minutes).</summary>
        public static List<Category> HeaderCategories()
        {
            const string cacheKey = "CH_HeaderCategories";
            var cached = System.Web.HttpRuntime.Cache[cacheKey] as List<Category>;
            if (cached != null) return cached;

            var list = (Common.getcategory() ?? new List<System.Web.Mvc.SelectListItem>())
                .Select(x => new Category { Id = int.Parse(x.Value), CategoryValue = x.Text })
                .ToList();
            if (list.Count > 0)
                System.Web.HttpRuntime.Cache.Insert(cacheKey, list, null, DateTime.UtcNow.AddMinutes(10), System.Web.Caching.Cache.NoSlidingExpiration);
            return list;
        }
    }
}
