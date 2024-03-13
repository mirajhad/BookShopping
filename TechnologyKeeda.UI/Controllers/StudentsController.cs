using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using TechnologyKeeda.Entities;
using TechnologyKeeda.Repositories.Interfaces;
using TechnologyKeeda.UI.ViewModels.StudentViewModels;

namespace TechnologyKeeda.UI.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentRepo _studentRepo;
        private ISkillRepo _skillRepo;

        public StudentsController(IStudentRepo studentRepo, ISkillRepo skillRepo)
        {
            _studentRepo = studentRepo;
            _skillRepo = skillRepo;
        }

        public async Task<IActionResult> Index()
        {
            var students= await _studentRepo.GetAll();
            List<StudentViewModel> studentList =  new List<StudentViewModel>();
            foreach (var student in students)
            {
                studentList.Add(new StudentViewModel { Id = student.Id, Name = student.Name });
            }
            return View(studentList);
        }
        [HttpGet]

        public async Task<IActionResult> Create()
        {
            CreateStudentViewModel vm = new CreateStudentViewModel();
            var skills =  await _skillRepo.GetAll();
            foreach (var skill in skills)
            {
                vm.SkillList.Add(new CheckBoxTable { SkillId = skill.Id, SkillName = skill.Title, IsChecked = false });

            }
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Name = vm.StudentName,
                    PermanentAddress = vm.PhysicalAddress

                };
                var selectedSkillIds = vm.SkillList.Where(x => x.IsChecked == true)
                    .Select(y => y.SkillId).ToList();  //1,2,3

                foreach (var skillId in selectedSkillIds)
                {
                    student.StudentSkills.Add(new StudentSkill
                    {
                        SkillId = skillId
                    });

                }

                await _studentRepo.Save(student);
                return RedirectToAction("Index");
            }
            return View();



        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            EditStudentViewModel vm = new EditStudentViewModel();
            var student =  await _studentRepo.GetById(id);
            vm.StudentName = student.Name;
            vm.PhysicalAddress = student.PermanentAddress;
            var existingSkillIds = student.StudentSkills.Select(x => x.SkillId).ToList();

            var skills = await _skillRepo.GetAll();
            foreach (var skill in skills)
            {
                vm.SkillList.Add(new CheckBoxTable { SkillId = skill.Id,
                    SkillName = skill.Title,
                    
                    IsChecked = existingSkillIds.Contains(skill.Id)
                });

            }
            return View(vm);



        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditStudentViewModel vm)
        {
            var student = await _studentRepo.GetById(vm.Id);
            var existingSkillIds = student.StudentSkills.
                Select(x => x.SkillId).ToList();  //1,2,4

            student.Name = vm.StudentName;
            student.PermanentAddress = vm.PhysicalAddress;

            var selectedSkillIds = vm.SkillList.Where(x => x.IsChecked == true)
            .Select(y => y.SkillId).ToList(); //1,3,4,5

            var toRemove =  existingSkillIds.Except(selectedSkillIds).ToList();
            var toAdd = selectedSkillIds.Except(existingSkillIds).ToList();

            foreach (var skillId in toRemove)
            {
                var studentSkill = student.StudentSkills.FirstOrDefault(x => x.SkillId == skillId);
                student.StudentSkills.Remove(studentSkill);
            }
            foreach (var skillId in toAdd)
            {
                student.StudentSkills.Add(new StudentSkill
                {
                    SkillId = skillId
                });

            }
            await _studentRepo.Edit(student);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentRepo.GetById(id);
            await _studentRepo.RemoveData(student);

            return RedirectToAction("Index");
        }


        }
}
