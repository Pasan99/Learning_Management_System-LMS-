//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EastWood.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Semester
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Semester()
        {
            this.SemesterResults = new HashSet<SemesterResult>();
            this.Subjects = new HashSet<Subject>();
        }
        public double StudentAvgMarksForSemester { get; set; }
        public string StudentGradeForSemester { get; set; }
        public int SemesterId { get; set; }
        public System.DateTime SemesterStartDate { get; set; }
        public System.DateTime SemesterEndDate { get; set; }
        public string SemesterName { get; set; }
        public int CourseId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SemesterResult> SemesterResults { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual Course Course { get; set; }
    }
}
