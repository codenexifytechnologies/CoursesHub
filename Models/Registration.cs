using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CoursesHub.Models
{
    public class Registration
    {
        public StudentRegistration objStudentRegistration { get; set; }
        public UniversityRegistration objUniversityRegistration { get; set; }
        public string LoginType { get; set; }
    }
    public class StudentRegistration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "First name can contain letters only")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Middle name can contain letters only")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Last name can contain letters only")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        [RegularExpression("^[0-9]{10,15}$", ErrorMessage = "Mobile number must be 10-15 digits")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public string SqlMsg { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public bool isAustraliaResident { get; set; }
        public string State { get; set; }
        public string Area { get; set; }
        
        public string VerifyLink { get; set; }

    }
    public class UniversityRegistration
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter RTO ID")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Please enter exactly 6 digits.")]
        public string RTO_ID { get; set; }

        [RegularExpression(@"^\d{5}E$", ErrorMessage = "Code must be 5 digits followed by an uppercase 'E' (e.g. 12345E).")]
        public string CRICOS_Code { get; set; }

        [Required(ErrorMessage = "Please enter Legal Name Organisation")]
        public string Legal_Name_Organisation { get; set; }

        public string Trading_Name { get; set; }

        [Required(ErrorMessage = "Please enter ABN/ACN")]
        [RegularExpression(@"^(\d{9}|\d{11})$", ErrorMessage = "Please enter 9 digits for ACN or 11 digits for ABN.")]
        public string ABN_ACN { get; set; }

        [Required(ErrorMessage = "Please enter Business Address")]
        public string Business_Address { get; set; }

        [Required(ErrorMessage = "Please enter Postal Address")]
        public string Postal_Address { get; set; }

        public string Website { get; set; }

        [Required(ErrorMessage = "Please enter Contact Number")]
        public string Contact_Number { get; set; }

        [Required(ErrorMessage = "Please enter General Contact Email")]
        public string General_Contact_Email { get; set; }

        [Required(ErrorMessage = "Please enter Full Name")]
        public string Full_Name { get; set; }

        [Required(ErrorMessage = "Please enter Position / Job Title")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please enter Direct Phone")]
        public string Direct_Phone { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        public string Email { get; set; }

        public bool RTO_Status { get; set; }

        [Required(ErrorMessage = "Please select Relevant Courses")]
        public List<string> Relevant_Courses { get; set; }

        public string Relevant_Courses_String { get; set; }

        public List<SelectListItem> Relevant_Courses_Options { get; set; }

        public string Signature { get; set; }

        public string Logo { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public bool? isUniversityActive { get; set; }

        public DateTime CreatedDate { get; set; }
        public string SqlMsg { get; set; }

        public string State { get; set; }
        public string Zipcode { get; set; }
        public string RejectionReason { get; set; }
        public bool IsReject { get; set; }
    }
}

