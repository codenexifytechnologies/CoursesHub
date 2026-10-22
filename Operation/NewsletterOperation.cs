using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoursesHub.Operation
{
    /// <summary>Result of a subscribe call: Subscribed, Resubscribed or AlreadySubscribed (see SQL/newsletter.sql).</summary>
    public class NewsletterSubscribeResult
    {
        public string Result { get; set; }
        public Guid Token { get; set; }
    }

    public class NewsletterUnsubscribeResult
    {
        public string Result { get; set; }
        public string Email { get; set; }
    }

    public class NewsletterSubscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime SubscribedDate { get; set; }
        public DateTime? UnsubscribedDate { get; set; }
        public string Source { get; set; }
    }

    public class NewsletterOperation
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCon"].ToString());

        static readonly Regex EmailRx = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled);

        /// <summary>Normalised email (trimmed, lower case), or null when it is not a valid address.</summary>
        public static string CleanEmail(string email)
        {
            var value = (email ?? string.Empty).Trim().ToLowerInvariant();
            if (value.Length == 0 || value.Length > 256 || !EmailRx.IsMatch(value)) return null;
            try { return new System.Net.Mail.MailAddress(value).Address == value ? value : null; }
            catch (FormatException) { return null; }
        }

        public NewsletterSubscribeResult Subscribe(string email, string source)
        {
            try
            {
                var para = new DynamicParameters();
                para.Add("@Email", email, DbType.String);
                para.Add("@Source", source, DbType.String);
                return con.Query<NewsletterSubscribeResult>("usp_newsletter_subscribe", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Subscribe", "NewsletterOperation");
                throw;
            }
        }

        public NewsletterUnsubscribeResult Unsubscribe(Guid token)
        {
            try
            {
                var para = new DynamicParameters();
                para.Add("@Token", token, DbType.Guid);
                return con.Query<NewsletterUnsubscribeResult>("usp_newsletter_unsubscribe", para, null, true, 0, CommandType.StoredProcedure).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "Unsubscribe", "NewsletterOperation");
                throw;
            }
        }

        public List<NewsletterSubscriber> GetSubscribers()
        {
            try
            {
                return con.Query<NewsletterSubscriber>("usp_get_newsletter_subscribers", null, null, true, 0, CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Common.errorlog(Convert.ToString(ex.Message) + " Inner Msg:" + Convert.ToString(ex.StackTrace), "GetSubscribers", "NewsletterOperation");
                throw;
            }
        }
    }
}
