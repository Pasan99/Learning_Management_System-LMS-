using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.ViewModel.Admin
{
    public class AssignmentItem
    {
        public Assignment Assignment { get; set; }
        public Subject Subject { get; set; }
        public string AssignmentGrade { get; set; }
        public double AssignmentMarks { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}