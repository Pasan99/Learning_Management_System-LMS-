using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.ViewModel.Admin
{
    public class CoursesViewModel
    {
        public List<CourseItem> Courses { get; set; }
    }

    public class CourseItem : Course
    {
        public CoordinatorDetails CoordinatorDetails { get; set; }
    }
}