using CustomAuthorizationFilter.Infrastructure;
using EastWood.Infrastructure;
using EastWood.Models;
using EastWood.Utilities;
using EastWood.ViewModel.Admin;
using EastWood.ViewModel.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace EastWood.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Index(int Id = 0)
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            ViewBag.SelectedCourseId = Id;
            int userId = int.Parse(Session["UserId"].ToString());
            viewModel.Courses = GetCoursesViewModel(userId);
            if (viewModel.Courses != null && viewModel.Courses.Courses.Count > Id)
            {
                viewModel.Semesters = GetSemesters(viewModel.Courses.Courses[Id].CourseId);
                viewModel.Subjects = new List<Subject>();
                foreach (var sem in viewModel.Semesters)
                {
                    viewModel.Subjects.AddRange(GetSubjectsBySemester(sem.SemesterId));
                }
                
            }
            using(EastWoodEntities db = new EastWoodEntities())
            {
                var student = db.Students.Where(w => w.UserId == userId).FirstOrDefault();
                if (student != null && viewModel.Subjects != null && viewModel.Semesters != null)
                {
                    // get marks for student for subjects
                    foreach (var sub in viewModel.Subjects)
                    {
                        sub.StudentAvgMarksForSubject = MarksHelper.GetAvgMarksForSubject(sub.SubjectId, student.StudentId);
                        sub.StudentGradeForSubject = MarksHelper.GetGradeForSubject(sub.SubjectId, student.StudentId);
                    }
                    // get marks for student for semesters
                    foreach (var sem in viewModel.Semesters)
                    {
                        sem.StudentGradeForSemester = MarksHelper.GetGradeForSemester(sem.SemesterId, student.StudentId);
                    }
                }
            }
            
            return View(viewModel);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Courses()
        {
            int userId = int.Parse(Session["UserId"].ToString());
            var viewModel = GetCoursesViewModel(userId);
            if (viewModel == null)
            {
                return Redirect("/");
            }
            return View(viewModel);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult CoursesToEnroll()
        {
            CoursesViewModel viewModel = new CoursesViewModel();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                viewModel.Courses = db.Courses
                    .Join(
                        db.Coordinators,
                        course => course.CoordinatorId,
                        coordinator => coordinator.CoordinatorId,
                        (course, coordinator) => new
                        {
                            Course = course,
                            Coordinator = coordinator
                        })
                    .Join(
                        db.Users,
                        cd => cd.Coordinator.UserId,
                        user => user.UserId,
                        (cd, user) => new CourseItem
                        {
                            CourseName = cd.Course.CourseName,
                            CourseId = cd.Course.CourseId,
                            CourseDescription = cd.Course.CourseDescription,
                            CourseEndsOn = cd.Course.CourseEndsOn,
                            CourseStartsOn = cd.Course.CourseStartsOn,
                            CoordinatorDetails = new ViewModel.CoordinatorDetails
                            {
                                UserId = user.UserId,
                                UserBio = user.UserBio,
                                UserFirstName = user.UserFirstName,
                                UserLastLoggedIn = user.UserLastLoggedIn,
                                UserLastName = user.UserLastName,
                                UserEmail = user.UserEmail,
                                UserContactNo = user.UserContactNo,
                                CoordinatorId = cd.Coordinator.CoordinatorId
                            }
                        }
                     )
                    .OrderByDescending(o => o.CourseStartsOn).ToList();

                // Remove Already enrolled courses
                int userId = int.Parse(Session["UserId"].ToString());
                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId != 0)
                {
                    var studentCourseIds = db.StudentCourses
                        .Where(w => w.StudentId == studentId)
                        .Select(s=> s.CourseId)
                        .Distinct()
                        .ToList();

                    foreach(var id in studentCourseIds)
                    {
                        var itemFromList = viewModel.Courses.Where(w => w.CourseId == id).FirstOrDefault();
                        if (itemFromList != null)
                        {
                            viewModel.Courses.Remove(itemFromList);
                        }
                    }
                }
            }
            return View(viewModel);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Enroll(int Id)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            if (Id == 0) {
                return Redirect("/Student/Courses");
            }
            using (EastWoodEntities db = new EastWoodEntities())
            {
                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId == 0)
                {
                    return Redirect("/");
                }
                StudentCourse studentCourse = new StudentCourse();
                studentCourse.CourseId = Id;
                studentCourse.StudentId = studentId;
                db.StudentCourses.Add(studentCourse);
                db.SaveChanges();
            }
            return Redirect("/Student/Courses");
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Semesters(int Id)
        {
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var list = db.Semesters.Where(w => w.CourseId == Id).ToList();
                ViewBag.CourseId = Id;
                return View(list);
            }
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Subjects(int Id = 0)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            List<Subject> subjects = new List<Subject>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId == 0)
                {
                    return Redirect("/");
                }
                List<Subject> items = new List<Subject>();
                if (Id == 0)
                {
                    items = db.Subjects
                        .Join(
                            db.Semesters,
                            sub => sub.SemesterId,
                            sem => sem.SemesterId,
                            (sub, sem) => new { Semester = sem, Subject = sub }
                        )
                        .Join(
                            db.StudentCourses.Where(w => w.StudentId == studentId),
                            SemesterSubject => SemesterSubject.Semester.CourseId,
                            sc => sc.CourseId,
                            (SemesterSubject, sc) => new { Subject = SemesterSubject.Subject }
                        )
                        .Select(s => s.Subject)
                        .ToList() ;
                }
                else
                {
                    items = db.Subjects.Where(w => w.SemesterId == Id).ToList();
                }
                foreach (var item in items)
                {
                    Subject sub = new Subject();
                    sub = item;
                    sub.Semester = item.Semester;
                    sub.Lecturer.User = item.Lecturer.User;
                    sub.Semester.Course = item.Semester.Course;
                    subjects.Add(sub);
                }
            }
            ViewBag.SemesterId = Id;
            return View(subjects);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult Assignments(int Id = 0)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            List<AssignmentItem> assignments = new List<AssignmentItem>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId == 0)
                {
                    return Redirect("/");
                }
                List<Assignment> items = new List<Assignment>();
                if (Id == 0)
                {
                    items = db.Assignments
                        .Join(
                            db.Subjects,
                            a => a.SubjectId,
                            s => s.SubjectId,
                            (a, s) => new {Subject = s, Assignment = a }
                         )
                        .Join(
                            db.Semesters,
                            asSub => asSub.Subject.SemesterId,
                            sem => sem.SemesterId,
                            (asSub, sem) => new { Semester = sem, Assignment = asSub.Assignment }
                        )
                        .Join(
                            db.StudentCourses.Where(w=> w.StudentId == studentId),
                            asSubCourse => asSubCourse.Semester.CourseId,
                            sc => sc.CourseId,
                            (asSubCourse, sc) => new { Assignment = asSubCourse.Assignment }
                        )
                        .Select(s=> s.Assignment)
                        .ToList();
                }
                else
                {
                    items = db.Assignments.Where(w => w.SubjectId == Id).ToList();
                }
                foreach (var item in items)
                {
                    AssignmentItem assignment = new AssignmentItem();
                    assignment.Assignment = item;
                    assignment.Subject = item.Subject;
                    assignment.AssignmentMarks = MarksHelper.GetMarksForAssignment(item.AssignmentId, studentId);
                    assignment.AssignmentGrade = MarksHelper.GetGradeForAssignment(item.AssignmentId, studentId);
                    assignments.Add(assignment);
                }
            }
            ViewBag.SubjectId = Id;
            return View(assignments);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult ViewAssignment(int Id = 0, string Message = "")
        {
            int userId = int.Parse(Session["UserId"].ToString());
            ViewBag.Message = Message;
            if (Id == 0)
            {
                return Redirect("/Student/Assignments");
            }
            else
            {
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                    if (studentId == 0)
                    {
                        return Redirect("/");
                    }
                    var assignment = db.Assignments.Where(w => w.AssignmentId == Id).FirstOrDefault();
                    assignment.Subject = assignment.Subject;
                    assignment.AssignmentUploads = assignment.AssignmentUploads.Where(w => w.StudentId == studentId).ToList();
                    foreach(var upload in assignment.AssignmentUploads)
                    {
                        upload.Attachments = db.Attachments.
                        Where(w => w.AttachmentReferenceId == upload.AssignmentUploadId && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT_UPLOADS)
                        .ToList();
                    }
                    assignment.Attachments = db.Attachments.
                        Where(w => w.AttachmentReferenceId == assignment.AssignmentId && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT)
                        .ToList();
                    return View(assignment);
                }
            }
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult SubmitAssignment(AssignmentUpload assignmentUpload)
        {
            
            int userId = int.Parse(Session["UserId"].ToString());
            using (EastWoodEntities db = new EastWoodEntities())
            {
                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                // validate file size
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = files[0];
                    var assignment = db.Assignments.Where(w => w.AssignmentId == assignmentUpload.AssignmentId).FirstOrDefault();
                    if (assignment == null || file.ContentLength > assignment.AssignmentMaxFileSize)
                    {
                        ViewBag.Message = "File size is exceede the allowed maximum upload size : " + FileHelper.GetConvertedFileSize(assignment.AssignmentMaxFileSize) ;
                        return Redirect("/Student/ViewAssignment/" + assignmentUpload.AssignmentId + "?Message=" + "File size is exceede the allowed maximum upload size : " + FileHelper.GetConvertedFileSize(assignment.AssignmentMaxFileSize));
                    }
                }

                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId == 0)
                {
                    return Redirect("/");
                }
                List<Attachment> attachments = new List<Attachment>();
                AssignmentUpload newUpload;
                // create Item
                if (assignmentUpload.AssignmentUploadId != 0)
                {
                    newUpload = db.AssignmentUploads.Where(w => w.AssignmentUploadId == assignmentUpload.AssignmentUploadId).FirstOrDefault();
                }
                else
                {
                    newUpload = new AssignmentUpload();
                }
                
                newUpload.AssignmentUploadSubmittedOn = DateTime.Now;
                newUpload.AssignmentUploadIsAfterDeadline = assignmentUpload.AssignmentUploadIsAfterDeadline;
                newUpload.AssignmentUploadGrade = Grades.NOT_MARKED;
                newUpload.AssignmentUploadMarks = 0;
                newUpload.StudentId = studentId;
                newUpload.AssignmentId = assignmentUpload.AssignmentId;
                if (assignmentUpload.AssignmentUploadId == 0)
                {
                    db.AssignmentUploads.Add(newUpload);
                }
                db.SaveChanges();

                int fileCount = db.Attachments
                    .Where(w => w.AttachmentReferenceId == newUpload.AssignmentUploadId && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT_UPLOADS)
                    .Count();

                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }

                        string[] find = fname.Split('.');
                        string extention = find[find.Length - 1];
                        fname = newUpload.Assignment.AssignmentName + "_upload_auid_"+ newUpload.AssignmentUploadId + "_" + (i + fileCount) + "." + extention;

                        // Get the complete folder path and store the file inside it.  
                        var finalName = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(finalName);

                        // Create attachment
                        Attachment attachment = new Attachment();
                        attachment.AttachmentName = fname;
                        attachment.AttachmentUrl = "/Uploads/" + fname;
                        attachment.AttachmentReferenceType = AttachmentReferenceTypes.ASSIGNMENT_UPLOADS;
                        attachment.AttachmentReferenceId = newUpload.AssignmentUploadId;
                        attachment.AttachmentFileSize = file.ContentLength;
                        db.Attachments.Add(attachment);
                        db.SaveChanges();

                    }
                }
            }
            return Redirect("/Student/Assignments");
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult LectureNotes(int Id = 0)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            List<LectureNoteItem> lectureNotes = new List<LectureNoteItem>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                List<LectureNote> items = new List<LectureNote>();
                if (Id == 0)
                {
                    int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                    if (studentId == 0)
                    {
                        items = db.LectureNotes.ToList();
                    }
                    else
                    {
                        items = db.LectureNotes
                                .Where(w => w.Subject.Semester.Course.StudentCourses.Where(w2 => w2.StudentId == studentId).Count() > 0)
                                .ToList();
                    }
                    
                }
                else
                {
                    items = db.LectureNotes.Where(w => w.SubjectId == Id).ToList();
                }
                foreach (var item in items)
                {
                    LectureNoteItem note = new LectureNoteItem();
                    note.LectureNote = item;
                    note.Subject = item.Subject;
                    note.Attachments = db.Attachments.
                        Where(w => w.AttachmentReferenceId == item.LectureNoteid && w.AttachmentReferenceType == AttachmentReferenceTypes.LECTURE_NOTE)
                        .ToList();
                    lectureNotes.Add(note);
                }
            }
            ViewBag.SubjectId = Id;
            return View(lectureNotes);
        }
        [CustomAuthorize(RoleTypes.STUDENT)]
        public ActionResult ViewLectureNote(int Id = 0, int SubjectId = 0)
        {
            if (Id == 0 && SubjectId == 0)
            {
                return Redirect("/Admin/Assignments");
            }
            if (Id != 0)
            {
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    var note = db.LectureNotes.Where(w => w.LectureNoteid == Id).FirstOrDefault();
                    note.Subject = note.Subject;
                    note.Attachments = db.Attachments.
                        Where(w => w.AttachmentReferenceId == note.LectureNoteid && w.AttachmentReferenceType == AttachmentReferenceTypes.LECTURE_NOTE)
                        .ToList();
                    return View(note);
                }
            }
            LectureNote sub = new LectureNote();
            sub.SubjectId = SubjectId;
            return View(sub);
        }
        public ActionResult MyProfile()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
        public ActionResult Login(string ReturnUrl = "/Admin")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user, string ReturnUrl = "/Admin")
        {
            string message = "";
            ReturnUrl = ViewBag.ReturnUrl;
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var v = db.Users.Where(w => w.UserEmail == user.UserEmail).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(user.UserPassword), v.UserPassword) != 0)
                    {
                        message = "Invalid credential provided";
                    }
                    else if (v.RoleId != RoleTypes.STUDENT_ID)
                    {
                        message = "This user is an admin. Please use admin login";
                    }
                    else
                    {
                        user.UserLastLoggedIn = DateTime.Now;
                        Session["UserId"] = v.UserId;
                        Session["UserName"] = v.UserFirstName + " " + v.UserLastName;
                        Session["UserRole"] = v.Role.RoleName;
                        Session["UserLastLoggedIn"] = v.UserLastLoggedIn;

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Student");
                        }
                    }
                }
                else
                {
                    message = "Invalid credential provided";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        public ActionResult Logout()
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            FormsAuthentication.SignOut();
            return Redirect("/Student/Login");
        }
        public ActionResult NoAccess()
        {
            return View();
        }
        private CoursesViewModel GetCoursesViewModel(int userId)
        {
            CoursesViewModel viewModel = new CoursesViewModel();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                int studentId = db.Students.Where(w => w.UserId == userId).Select(s => s.StudentId).FirstOrDefault();
                if (studentId == 0)
                {
                    return null;
                }
                viewModel.Courses = db.Courses
                    .Join(
                        db.StudentCourses.Where(w => w.StudentId == studentId),
                        c => c.CourseId,
                        sc => sc.CourseId,
                        (c, sc) => new { Course = c }
                    )
                    .Join(
                        db.Coordinators,
                        course => course.Course.CoordinatorId,
                        coordinator => coordinator.CoordinatorId,
                        (course, coordinator) => new
                        {
                            Course = course,
                            Coordinator = coordinator
                        })

                    .Join(
                        db.Users,
                        cd => cd.Coordinator.UserId,
                        user => user.UserId,
                        (cd, user) => new CourseItem
                        {
                            CourseName = cd.Course.Course.CourseName,
                            CourseId = cd.Course.Course.CourseId,
                            CourseDescription = cd.Course.Course.CourseDescription,
                            CourseEndsOn = cd.Course.Course.CourseEndsOn,
                            CourseStartsOn = cd.Course.Course.CourseStartsOn,
                            CoordinatorDetails = new ViewModel.CoordinatorDetails
                            {
                                UserId = user.UserId,
                                UserBio = user.UserBio,
                                UserFirstName = user.UserFirstName,
                                UserLastLoggedIn = user.UserLastLoggedIn,
                                UserLastName = user.UserLastName,
                                UserEmail = user.UserEmail,
                                UserContactNo = user.UserContactNo,
                                CoordinatorId = cd.Coordinator.CoordinatorId
                            }
                        }
                     )
                    .OrderByDescending(o => o.CourseStartsOn).ToList();
            }
            return viewModel;
        }
        private List<Semester> GetSemesters(int Id)
        {
            using (EastWoodEntities db = new EastWoodEntities())
            {
                var list = db.Semesters.Where(w => w.CourseId == Id).ToList();
                ViewBag.CourseId = Id;
                return list;
            }
        }
        private List<Subject> GetSubjectsBySemester(int Id)
        {
            List<Subject> subjects = new List<Subject>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                List<Subject> items = db.Subjects.Where(w => w.SemesterId == Id).ToList();
                
                foreach (var item in items)
                {
                    Subject sub = new Subject();
                    sub = item;
                    sub.Semester = item.Semester;
                    sub.Lecturer.User = item.Lecturer.User;
                    subjects.Add(sub);
                }
            }
            return subjects;
        }
    }
}