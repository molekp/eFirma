using System.Web.Mvc;
using BussinessLogic.AuthorizationLogic;
using BussinessLogic.DTOs.Employee;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Employee;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers
{
    [Authorize(Roles =ConstantsHelper.ADMIN_ROLE +","+ ConstantsHelper.EMPLOYEE_ROLE)]
    public class EmployeeController : Controller
    {
        public IEmployeeLogic EmployeeLogic { get; set; }

        public EmployeeController()
        {
            EmployeeLogic = RepositoryLogicFactory.GetEmployeeLogic();
        }
        //
        // GET: /Emplyees/
        public ActionResult Index()
        {
            var model = EmployeeLogic.GetAllDisplayEmployeesDto();

            return View(model);
        }

        public ActionResult DisplayEmployee(int a_employeeId)
        {
            var model = EmployeeLogic.GetDisplayEmployeeDto(a_employeeId,User.Identity.Name);

            return View(model);
        }

        [Authorize(Roles = ConstantsHelper.ADMIN_ROLE)]
        public ActionResult AddEmployee()
        {
            var model = EmployeeLogic.GetAddEmployeeDto();

            return View(model);
        }

        [Authorize(Roles = ConstantsHelper.ADMIN_ROLE)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(AddEmployeeDto a_employeeDto)
        {
            if (ModelState.IsValid)
            {

                if (!EmployeeLogic.DoesEmployeeExists(a_employeeDto.UserName))
                {

                    if(EmployeeLogic.CreateEmployee(a_employeeDto))
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", Resource.userExists);
                }
                else
                {
                    ModelState.AddModelError("", Resource.userExists);
                }
            }
            a_employeeDto = EmployeeLogic.GetAddEmployeeDto();

            return View(a_employeeDto);
        }

        [Authorize(Roles = ConstantsHelper.ADMIN_ROLE)]
        public ActionResult EditEmployee(int a_employeeid)
        {
            var model = EmployeeLogic.GetEditEmployee(a_employeeid);

            return View(model);
        }

        [Authorize(Roles = ConstantsHelper.ADMIN_ROLE)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(EditEmployeeDto a_editEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                if (EmployeeLogic.EditEmployee(a_editEmployeeDto))
                {
                    return RedirectToAction("DisplayEmployee", new {a_employeeId = a_editEmployeeDto.IdEmployee});
                }
                else
                {
                    ModelState.AddModelError("", Resource.editError);
                }
            }
            var model = EmployeeLogic.GetEditEmployee(a_editEmployeeDto.IdEmployee);

            return View(model);
        }
    }
}
