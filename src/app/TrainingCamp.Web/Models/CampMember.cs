using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrainingCamp.Web.Models
{
    public class CampMember
    {
        [DisplayName("KenshiNr")]
        [Key]
        [Required]
        public string CampMemberId { get; set; }
        [DisplayName("Club Nr")]
        public string ClubNr { get; set; }
        [DisplayName("Club Name")]
        public string ClubName { get; set; }
        public string Name { get; set; }
        [RegularExpression(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}\b")]
        public string Email { get; set; }
        public bool Paid { get; set; }
        public float PaidAmount { get; set; }
        public string PaidCurrency { get; set; }
        public DateTime PaidDateTime { get; set; }
        public string Comment { get; set; }
    }
}