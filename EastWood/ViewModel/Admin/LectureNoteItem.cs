using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.ViewModel.Admin
{
    public class LectureNoteItem
    {
        public LectureNote LectureNote { get; set; }
        public Subject Subject { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}