using CustomAuthorizationFilter.Infrastructure;
using EastWood.Infrastructure;
using EastWood.Models;
using EastWood.Utilities;
using EastWood.ViewModel;
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
    public class AdminController : Controller
    {
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult Index()
        {
            HomePageViewModel viewModel = new HomePageViewModel();
            viewModel.Courses = GetCoursesViewModel();

            return View(viewModel);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult Courses()
        {
            CoursesViewModel viewModel = new CoursesViewModel();
            using(EastWoodEntities db = new EastWoodEntities())
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
            }
            return View(viewModel);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult NewCourse(int Id = 0)
        {
            List<CoordinatorDetails> coordinators = new List<CoordinatorDetails>();
            EditCourseItem viewModel = new EditCourseItem();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                coordinators = db.Coordinators
                    .Join(
                        db.Users,
                        c => c.UserId,
                        u => u.UserId,
                        (c, u) => new CoordinatorDetails
                        {
                            UserId = u.UserId,
                            UserBio = u.UserBio,
                            UserContactNo = u.UserContactNo,
                            UserEmail = u.UserEmail,
                            UserFirstName = u.UserFirstName,
                            UserLastLoggedIn = u.UserLastLoggedIn,
                            UserLastName = u.UserLastName,
                            UserPassword = u.UserPassword,
                            CoordinatorId = c.CoordinatorId,
                            RoleId = u.RoleId
                        }

                        ).ToList();

                if (Id != 0)
                {
                    viewModel = db.Courses
                        .Where(w => w.CourseId == Id)
                    .Join(
                        db.Coordinators,
                        course => course.CoordinatorId,
                        coordinator => coordinator.CoordinatorId,
                        (course, coordinator) => new EditCourseItem
                        {
                            CourseName = course.CourseName,
                            CourseId = course.CourseId,
                            CourseDescription = course.CourseDescription,
                            CourseEndsOn = course.CourseEndsOn,
                            CourseStartsOn = course.CourseStartsOn,
                            CoordinatorId = course.CoordinatorId
                        })
                    .FirstOrDefault();
                }
            }
            viewModel.Coordinators = coordinators;
            // New
            if (Id == 0)
            {
                return View(viewModel);
            }
            // Edit
            else
            {
                return View(viewModel);
            }
        }
        [HttpPost]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult NewCourse(EditCourseItem CourseItem)
        {
            Course course = new Course();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                if (CourseItem.CourseId != 0)
                {
                    course = db.Courses.Where(w => w.CourseId == CourseItem.CourseId).FirstOrDefault();
                }
                course.CourseName = CourseItem.CourseName;
                course.CourseDescription = CourseItem.CourseDescription;
                course.CourseEndsOn = CourseItem.CourseEndsOn;
                course.CourseStartsOn = CourseItem.CourseStartsOn;
                course.CoordinatorId = CourseItem.CoordinatorId;

                if (CourseItem.CourseId == 0)
                {
                    db.Courses.Add(course);
                }
                db.SaveChanges();
            }
            return Redirect("/Admin/Courses");
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult Semesters(int Id)
        {
            using(EastWoodEntities db = new EastWoodEntities())
            {
                var list = db.Semesters.Where(w => w.CourseId == Id).ToList();
                ViewBag.CourseId = Id;
                return View(list);
            }
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult NewSemester(int Id = 0, int CourseId= 0)
        {
            if (Id != 0)
            {
                using(EastWoodEntities db = new EastWoodEntities())
                {
                    var semester = db.Semesters.Where(w => w.SemesterId == Id).FirstOrDefault();
                    return View(semester);
                }
            }
            Semester semester1 = new Semester();
            semester1.CourseId = CourseId;
            return View(semester1);
        }
        [HttpPost]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult NewSemester(Semester semester)
        {
            using (EastWoodEntities db = new EastWoodEntities())
            {
                if (semester.SemesterId != 0)
                {

                    var sem = db.Semesters.Where(w => w.SemesterId == semester.SemesterId).FirstOrDefault();
                    sem.SemesterStartDate = semester.SemesterStartDate;
                    sem.SemesterEndDate = semester.SemesterEndDate;
                    sem.SemesterName = semester.SemesterName;
                    db.SaveChanges();

                }
                else
                {
                    db.Semesters.Add(semester);
                    db.SaveChanges();
                }
                
            }
            return Redirect("/Admin/Semesters/" + semester.CourseId);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult Subjects(int Id = 0)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            List<Subject> subjects = new List<Subject>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                Lecturer lecturer = db.Lecturers.Where(w => w.UserId == userId).FirstOrDefault();
                List<Subject> items = new List<Subject>();
                if (lecturer != null && Id == 0)
                {
                    items = db.Subjects
                        .Where(w => w.LecturerId == lecturer.LecturerId)
                        .ToList();
                }
                else
                {
                    if (Id == 0)
                    {
                        items = db.Subjects.ToList();
                    }
                    else
                    {
                        items = db.Subjects.Where(w => w.SemesterId == Id).ToList();
                    }
                }
                
                foreach (var item in items)
                {
                    Subject sub = new Subject();
                    sub = item;
                    sub.Semester = item.Semester;
                    sub.Lecturer.User = item.Lecturer.User;
                    subjects.Add(sub);
                }
            }
            ViewBag.SemesterId = Id;
            return View(subjects);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult NewSubject(int Id = 0, int SemesterId = 0)
        {
            if (Id != 0)
            {
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    var subject = db.Subjects.Where(w => w.SubjectId == Id).FirstOrDefault();
                    return View(subject);
                }
            }
            Subject sub = new Subject();
            sub.SemesterId = SemesterId;
            return View(sub);
        }
        [HttpPost]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult NewSubject(Subject subject)
        {
            using (EastWoodEntities db = new EastWoodEntities())
            {
                if (subject.SubjectId != 0)
                {

                    var sem = db.Subjects.Where(w => w.SubjectId == subject.SubjectId).FirstOrDefault();
                    sem.SubjectName = subject.SubjectName;
                    sem.SubjectDescription = subject.SubjectDescription;
                    sem.SemesterId = subject.SemesterId;
                    sem.LecturerId = subject.LecturerId;
                    db.SaveChanges();

                }
                else
                {
                    db.Subjects.Add(subject);
                    db.SaveChanges();
                }

            }
            return Redirect("/Admin/Subjects/"+ subject.SemesterId);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult Assignments(int Id = 0)
        {
            int userId = int.Parse(Session["UserId"].ToString());
            List<AssignmentItem> assignments = new List<AssignmentItem>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                Lecturer lecturer = db.Lecturers.Where(w => w.UserId == userId).FirstOrDefault();
                List<Assignment> items = new List<Assignment>();
                if (lecturer != null && Id == 0)
                {
                    items = db.Assignments
                        .Join(
                            db.Subjects,
                            a => a.SubjectId,
                            s => s.SubjectId,
                            (a, s) => new { Assignment = a, Subject = s }
                        )
                        .Where(w => w.Subject.LecturerId == lecturer.LecturerId)
                        .Select(s=> s.Assignment)
                        .ToList();
                }
                else
                {
                    if (Id == 0)
                    {
                        items = db.Assignments.ToList();
                    }
                    else
                    {
                        items = db.Assignments.Where(w => w.SubjectId == Id).ToList();
                    }
                }
                
                foreach (var item in items)
                {
                    AssignmentItem assignment = new AssignmentItem();
                    assignment.Assignment = item;
                    assignment.Subject = item.Subject;
                    assignments.Add(assignment);
                }
            }
            ViewBag.SubjectId = Id;
            return View(assignments);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult NewAssignment(int Id = 0, int SubjectId = 0)
        {
            if (Id == 0 && SubjectId == 0)
            {
                return Redirect("/Admin/Assignments");
            }
            if (Id != 0)
            {
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    var assignment = db.Assignments.Where(w => w.AssignmentId == Id).FirstOrDefault();
                    assignment.Subject = assignment.Subject;
                    assignment.Attachments = db.Attachments.
                        Where(w => w.AttachmentReferenceId == assignment.AssignmentId && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT)
                        .ToList();
                    return View(assignment);
                }
            }
            Assignment sub = new Assignment();
            sub.SubjectId = SubjectId;
            return View(sub);
        }
        [HttpPost]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult NewAssignment(Assignment assignment)
        {
            
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    List<Attachment> attachments = new List<Attachment>();

                    // create Item
                    if (assignment.AssignmentId != 0)
                    {
                        var a = db.Assignments.Where(w => w.AssignmentId == assignment.AssignmentId).FirstOrDefault();
                        a.AssignmentName = assignment.AssignmentName;
                        a.AssignmentDescription = assignment.AssignmentDescription;
                        a.AssignmentIssuedOn = DateTime.Now;
                        a.AssignmentMarksReleaseOn = DateTime.Now;
                        a.AssignmentDeadline = assignment.AssignmentDeadline;
                        a.AssignmentMaxFileSize = assignment.AssignmentMaxFileSize;

                    }
                    else
                    {
                        db.Assignments.Add(assignment);
                    }
                    assignment.AssignmentIssuedOn = DateTime.Now;
                    db.SaveChanges();

                    int fileCount = db.Attachments
                        .Where(w => w.AttachmentReferenceId == assignment.AssignmentId && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT)
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
                            fname = assignment.AssignmentName + "_" + (i + fileCount) + "." + extention;

                            // Get the complete folder path and store the file inside it.  
                            var finalName = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                            file.SaveAs(finalName);

                            // Create attachment
                            Attachment attachment = new Attachment();
                            attachment.AttachmentName = fname;
                            attachment.AttachmentUrl = "/Uploads/" + fname;
                            attachment.AttachmentReferenceType = AttachmentReferenceTypes.ASSIGNMENT;
                            attachment.AttachmentReferenceId = assignment.AssignmentId;
                            attachment.AttachmentFileSize = file.ContentLength;
                            db.Attachments.Add(attachment);
                            db.SaveChanges();

                        }
                    }
                }
            

            return Redirect("/Admin/Assignments/" + assignment.SubjectId);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult LectureNotes(int Id = 0)
        {
            List<LectureNoteItem> lectureNotes = new List<LectureNoteItem>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                List<LectureNote> items = new List<LectureNote>();
                if (Id == 0)
                {
                    items = db.LectureNotes.ToList();
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
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult NewLectureNote(int Id = 0, int SubjectId = 0)
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
        [HttpPost]
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult NewLectureNote(LectureNote lectureNote)
        {

            using (EastWoodEntities db = new EastWoodEntities())
            {
                //  Get all files from Request object  
                HttpFileCollectionBase files = Request.Files;
                List<Attachment> attachments = new List<Attachment>();

                // create Item
                if (lectureNote.LectureNoteid != 0)
                {
                    var a = db.LectureNotes.Where(w => w.LectureNoteid == lectureNote.LectureNoteid).FirstOrDefault();
                    a.LectureNoteName = lectureNote.LectureNoteName;
                    a.LectureNoteDescription = lectureNote.LectureNoteDescription;
                    a.LectureNoteSubmittedOn = DateTime.Now;
                    a.LectureNoteDate = lectureNote.LectureNoteDate;
                }
                else
                {
                    lectureNote.LectureNoteSubmittedOn = DateTime.Now;
                    db.LectureNotes.Add(lectureNote);
                }
                db.SaveChanges();

                int fileCount = db.Attachments
                    .Where(w => w.AttachmentReferenceId == lectureNote.LectureNoteid && w.AttachmentReferenceType == AttachmentReferenceTypes.LECTURE_NOTE)
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
                        fname = lectureNote.LectureNoteName + "_" + (i + fileCount) + "." + extention;

                        // Get the complete folder path and store the file inside it.  
                        var finalName = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(finalName);

                        // Create attachment
                        Attachment attachment = new Attachment();
                        attachment.AttachmentName = fname;
                        attachment.AttachmentUrl = "/Uploads/" + fname;
                        attachment.AttachmentReferenceType = AttachmentReferenceTypes.LECTURE_NOTE;
                        attachment.AttachmentReferenceId = lectureNote.LectureNoteid;
                        attachment.AttachmentFileSize = file.ContentLength;
                        db.Attachments.Add(attachment);
                        db.SaveChanges();

                    }
                }
            }


            return Redirect("/Admin/LectureNotes/" + lectureNote.SubjectId);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult AssignmentUploads(int Id = 0)
        {
            List<AssignmentUpload> assignmentUploads = new List<AssignmentUpload>();
            using (EastWoodEntities db = new EastWoodEntities())
            {
                if (Id == 0)
                {
                    assignmentUploads = db.AssignmentUploads.ToList();
                }
                else
                {
                    assignmentUploads = db.AssignmentUploads.Where(w => w.AssignmentId == Id).ToList();
                }
                foreach (var item in assignmentUploads)
                {
                    item.Student.User = item.Student.User;
                    item.Assignment = item.Assignment;
                    item.Attachments = db.Attachments
                        .Where(w => w.AttachmentReferenceId == item.AssignmentUploadId
                        && w.AttachmentReferenceType == AttachmentReferenceTypes.ASSIGNMENT_UPLOADS)
                        .OrderByDescending(o=> o.AttachmentId)
                        .ToList();
                }
            }
            ViewBag.AssignmentId = Id;
            return View(assignmentUploads);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER)]
        public ActionResult SetMarks(int Id, int Marks, int AssignmentId, string Comment)
        {
            using(EastWoodEntities db = new EastWoodEntities())
            {
                var upload = db.AssignmentUploads.Where(w => w.AssignmentUploadId == Id).FirstOrDefault();
                if (upload != null)
                {
                    upload.AssignmentUploadMarks = Marks;
                    upload.AssignmentUploadGrade = MarksHelper.GetGrade(Marks);
                    upload.AssignmentUploadLecturerComment = Comment;
                    db.SaveChanges();
                }
            }
            return Redirect("/Admin/AssignmentUploads/" + AssignmentId);
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
        public ActionResult Users()
        {
            using(EastWoodEntities db = new EastWoodEntities())
            {
                List<User> users = db.Users.ToList();
                foreach(var user in users)
                {
                    user.Role = user.Role;
                }
                return View(users);
            }
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER)]
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
        //Login POST
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
                    else if (!IsAdmin(v.RoleId))
                    {
                        message = "This user is not an admin";
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
                            return RedirectToAction("Index", "Admin");
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
        [CustomAuthorize(RoleTypes.MANAGER)]
        public ActionResult Register()
        {
            return View();
        }
        [CustomAuthorize(RoleTypes.MANAGER)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            bool Status = false;
            string message = "";
            //
            // Model Validation 
            if (true)
            {

                //Email is already Exist 
                var isExist = IsEmailExist(user.UserEmail);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }

                user.UserPassword = Crypto.Hash(user.UserPassword);
                user.UserLastLoggedIn = DateTime.Now;

                // Save to Database
                using (EastWoodEntities db = new EastWoodEntities())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    if (user.RoleId == RoleTypes.LECTURER_ID)
                    {
                        Lecturer lecturer = new Lecturer();
                        lecturer.UserId = user.UserId;
                        db.Lecturers.Add(lecturer);
                        db.SaveChanges();
                    }
                    else if (user.RoleId == RoleTypes.COORDINATOR_ID)
                    {
                        Coordinator coordinator = new Coordinator();
                        coordinator.UserId = user.UserId;
                        db.Coordinators.Add(coordinator);
                        db.SaveChanges();
                    }
                    else if (user.RoleId == RoleTypes.STUDENT_ID)
                    {
                        Student student = new Student();
                        student.UserId = user.UserId;
                        db.Students.Add(student);
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                message = "Invalid Request";
            }

            ViewBag.Message = message;
            ViewBag.Status = Status;
            return Redirect("/Admin/Users");
        }
        public ActionResult Logout()
        {
            Session["UserName"] = string.Empty;
            Session["UserId"] = string.Empty;
            FormsAuthentication.SignOut();
            return Redirect("/Admin/Login");
        }
        [CustomAuthorize(RoleTypes.COORDINATOR, RoleTypes.MANAGER, RoleTypes.LECTURER, RoleTypes.STUDENT)]
        public ActionResult Download(string Url)
        {
            string fullName = Server.MapPath("~" + Url);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Url);
        }
        public ActionResult NoAccess()
        {
            return View();
        }
        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
        private bool IsEmailExist(string Email)
        {
            using(EastWoodEntities db = new EastWoodEntities())
            {
                var user = db.Users.Where(w => w.UserEmail.ToLower() == Email.ToLower()).FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }
        private bool IsAdmin(int RoleId)
        {
            if (RoleId == RoleTypes.COORDINATOR_ID || RoleId == RoleTypes.LECTURER_ID || RoleId == RoleTypes.MANAGER_ID)
            {
                return true;
            }
            return false;
        }
        private CoursesViewModel GetCoursesViewModel()
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

                foreach(var item in viewModel.Courses)
                {
                    item.StudentCourses = db.StudentCourses.Where(w=> w.CourseId == item.CourseId).ToList();
                }
            }
            return viewModel;
        }
    }
}