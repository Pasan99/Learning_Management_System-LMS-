using EastWood.Models;
using EastWood.ViewModel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.ViewModel.Users
{
    public class HomePageViewModel
    {
        public CoursesViewModel Courses { get; set; }
        public List<Semester> Semesters { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}