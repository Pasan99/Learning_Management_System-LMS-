using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.ViewModel.Admin
{
    public class EditCourseItem: Course
    {
        public List<CoordinatorDetails> Coordinators { get; set; }
    }
}