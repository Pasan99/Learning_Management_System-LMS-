using EastWood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.Utilities
{
    public class MarksHelper
    {
        public static string GetGrade(double Marks)
        {
            if (Marks >= 75)
            {
                return Grades.DISTINCTION;
            }
            else if (Marks >= 60 && Marks < 75)
            {
                return Grades.MERIT;
            }
            else if (Marks >= 40 && Marks < 60)
            {
                return Grades.PASS;
            }
            else
            {
                if (Marks > 0)
                {
                    return Grades.FAILED;
                }

                return Grades.NOT_MARKED;
            }
        }

        public static double GetMarksForAssignment(int AssignmentId, int StudentId)
        {
            double marks = 0;
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var submitions = db.AssignmentUploads
                    .Where(w => w.AssignmentId == AssignmentId && w.StudentId == StudentId)
                    .FirstOrDefault();
                if (submitions != null)
                {
                    marks = submitions.AssignmentUploadMarks;
                }
                
            }
            return marks;
        }
        public static string GetGradeForAssignment(int AssignmentId, int StudentId)
        {
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var submitions = db.AssignmentUploads
                    .Where(w => w.AssignmentId == AssignmentId && w.StudentId == StudentId)
                    .FirstOrDefault();
                if (submitions != null)
                {
                    return submitions.AssignmentUploadGrade;
                }
                return "Not Submitted";
            }
        }
        public static double GetAvgMarksForSubject(int SubjectId, int StudentId)
        {
            double marks = 0;
            using(EastWoodEntities db = new EastWoodEntities())
            {
                var submittions = db.AssignmentUploads
                    .Where(w => w.Assignment.SubjectId == SubjectId && w.StudentId == StudentId)
                    .ToList();

                int count = 0;
                foreach(var sub in submittions)
                {
                    if (sub.AssignmentUploadMarks > 0)
                    {
                        count++;
                        marks += sub.AssignmentUploadMarks;
                    }
                }
                marks /= count;
            }
            return marks;
        }

        public static string GetGradeForSubject(int SubjectId, int StudentId)
        {
            double marks = 0;
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var submittions = db.AssignmentUploads
                    .Where(w => w.Assignment.SubjectId == SubjectId && w.StudentId == StudentId)
                    .ToList();

                int count = 0;
                foreach (var sub in submittions)
                {
                    if (sub.AssignmentUploadMarks > 0)
                    {
                        count++;
                        marks += sub.AssignmentUploadMarks;
                    }
                }
                marks /= count;
            }
            return GetGrade(marks);
        }

        public static string GetGradeForSemester(int SemesterId, int StudentId)
        {
            double marks = 0;
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var subjects = db.Subjects
                    .Where(w => w.SemesterId == SemesterId)
                    .ToList();
                int count = 0;
                foreach (var sub in subjects)
                {
                    var totalForSub = GetAvgMarksForSubject(sub.SubjectId, StudentId);
                    if (totalForSub > 0)
                    {
                        count++;
                        marks += GetAvgMarksForSubject(sub.SubjectId, StudentId);
                    }
                }
                marks /= count;
            }
            return GetGrade(marks);
        }
    }
}