using CleanStudentManagement.BLL.Services;
using CleanStudentManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechnologyKeeda.ConcertBooking.Repositories;

namespace CleanStudentManagement.UI.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentService _studentService;
        private IExamService _examService;
        private IQnAsService _qnasService;
        private string containerName = "StudentImage";
        private string cvContainerName = "StudentCV";

        private IUtilityService _utilityService;

        public StudentsController(IStudentService studentService, IExamService examService,
            IQnAsService qnasService, IUtilityService utilityService)
        {
            _studentService = studentService;
            _examService = examService;
            _qnasService = qnasService;
            _utilityService = utilityService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Profile()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetails");
            if (sessionObj!=null)
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(sessionObj);
                var studentDetails = _studentService.GetStudentById(loginViewModel.Id);
                return View(studentDetails);
            }
            return RedirectToAction("Login","Accounts");
        }
        [HttpPost]
        public async Task<IActionResult> Profile(StudentProfileViewModel vm)
        {
            if(vm.ProfilePictureUrl!=null)
                vm.ProfilePicture = await  _utilityService.SaveImage(containerName, vm.ProfilePictureUrl);
            if (vm.CvFileUrl != null)
                vm.CVFileName = await _utilityService.SaveImage(cvContainerName, vm.CvFileUrl);

            _studentService.UpdateProfile(vm);
            return RedirectToAction("profile");

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel vm)
        {
            var success = await _studentService.AddStudentAsync(vm);
            if (success > 0)
            {
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        public IActionResult Index(int pageNumber=1,int pageSize=10)
        {
           
            return View(_studentService.GetAllStudents(pageNumber,pageSize));
        }
        [HttpGet]
        public IActionResult AttendExam()
        {
            var model =  new AttendExamViewModel();
            string loginObj = HttpContext.Session.GetString("loginDetails");
            LoginViewModel sessionObj =  JsonConvert.DeserializeObject<LoginViewModel>(loginObj);
            if (sessionObj != null)
            {
                     model.StudentId = sessionObj.Id;
                    var todayExam =  _examService.GetAllExams().
                    Where(x=>x.StartDate.Date==DateTime.Today.Date).FirstOrDefault();
                if (todayExam== null) {
                    model.Message = "No Exam Scheduled today";
                    return View(model);
                }
                else
                {
                   if(!_qnasService.IsAttendExam(todayExam.Id, model.StudentId))
                        {
                        model.QnAsList = _qnasService.GetAllByExamId(todayExam.Id).ToList();
                        model.ExamName = todayExam.Title;
                        model.Message = "";
                        return View(model);
                    }
                   else
                    {
                        model.Message = "You have already attend this exam";
                        return View(model);
                    }
                }
            }
            return RedirectToAction("Login", "Accounts");
        }
        [HttpPost]
        public IActionResult AttendExam(AttendExamViewModel viewModel)

        {
            bool result = _studentService.SetExamResult(viewModel);
            return RedirectToAction("");

        }
        public IActionResult Result()
        {
            var sessionObj = HttpContext.Session.GetString("loginDetails");
            if (sessionObj != null)
            {
                var loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(sessionObj);
                var model = _studentService.GetExamResults(Convert.ToInt32(loginViewModel.Id));
                return View(model);

            }
            return RedirectToAction("Login","Accounts");  
        }
    }
}
