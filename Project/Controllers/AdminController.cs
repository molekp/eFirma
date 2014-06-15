using System.Collections.Generic;
using System.Web.Mvc;
using BussinessLogic.DTOs.Admin;
using BussinessLogic.DTOs.ViewModelsOnly;
using BussinessLogic.DatabaseLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using BussinessLogic.Helpers;
using ResourceLibrary;

namespace Project.Controllers
{
    [Authorize(Roles = ConstantsHelper.ADMIN_ROLE)]
    public class AdminController : Controller
    {
        public ISafetyPointsLogic SafetyPointsLogic { get; set; }
        public IUserManagementAdminLogic UserManagementAdminLogic { get; set; }

        public AdminController()
        {
            SafetyPointsLogic = RepositoryLogicFactory.GetSafetyPointLogic();
            UserManagementAdminLogic = RepositoryLogicFactory.GetUserManagementAdminLogic();
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SafetyPoints()
        {
            var displaySafetyPoints = SafetyPointsLogic.GetAllDisplaySafetyPoints();

            return View(displaySafetyPoints);
        }

        //
        // GET: /Admin/
        public ActionResult AddSafetyPoint()
        {
            var addNewSafetyPointDto = new AddNewSafetyPointDto();

            ViewData[ Resource.SelectListTypesOfSafetyPoints ] = SafetyPointsLogic.GetAllTypesOfSafetyPoints();

            return View(addNewSafetyPointDto);
        }

        [HttpPost]
        public ActionResult AddSafetyPoint(AddNewSafetyPointDto a_newSafetyPointDto)
        {
            ViewData[Resource.SelectListTypesOfSafetyPoints] = SafetyPointsLogic.GetAllTypesOfSafetyPoints();
            if (ModelState.IsValid)
            {
                if (0 == a_newSafetyPointDto.IdOfPointInTable)//TODO zrobic validacje dla ddList (pewnie trzeba spod jQuery)
                {
                    ModelState.AddModelError("SafetyPointCreateInfo",
                                             "nie wypelnione wszystkie pola, nie dziala required na ddlistach bo nie zaznaczone zwracaja inta = 0");
                }
                else if (SafetyPointsLogic.CreateSafetyPoint(a_newSafetyPointDto))
                {
                    ModelState.AddModelError("SafetyPointCreateInfo", Resource.newSafetyPointCreated);
                    a_newSafetyPointDto = new AddNewSafetyPointDto();
                }
                else
                {
                    ModelState.AddModelError("SafetyPointCreateInfo", Resource.createdError);
                }

                return View(a_newSafetyPointDto);
            }

            return View(a_newSafetyPointDto);
        }

        [HttpPost]
        public ActionResult TypeOfSafetyPointChanged(int a_idTypeOfSafetyPoint = 0)
        {
            var str = new List<SelectListItem>();
            if(a_idTypeOfSafetyPoint !=0)
                str = SafetyPointsLogic.GetRecordsForType(a_idTypeOfSafetyPoint);

            return Json(str.ToArray());
        }

        public ActionResult DeleteSafetyPoint(int a_idSafetyPoint)//TODO
        {
            return View();
        }

        public ActionResult EditSafetyPoint(int a_idSafetyPoint)//TODO
        {
           return View();
        }


        public ActionResult SafetyPointGroups()
        {
            var groups = SafetyPointsLogic.GetAllDisplaySafetyPointsGroups();

            return View(groups);
        }

        public ActionResult AddSafetyPointGroup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSafetyPointGroup(AddSafetyPointGroup a_addSafetyPointGroup)
        {
            var idOfInsertedRow = 0;
            if (false == ModelState.IsValid || 0 == (idOfInsertedRow =SafetyPointsLogic.CreateSafetyPointGroup(a_addSafetyPointGroup)))
            {
                return View();
            }

            return RedirectToAction("EditSafetyPointGroup", new { a_idSafetyPointGroup = idOfInsertedRow });
            
        }


        public ActionResult DeleteSafetyPointGroup(int a_idSafetyPointGroup)
        {
            var viewModel = new RemoveSafetyPointGroupModel {IdSafetyPointGroup = a_idSafetyPointGroup};

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult DeleteSafetyPointGroup(RemoveSafetyPointGroupModel a_viewModel)
        {
            if(! ModelState.IsValid){
                ModelState.AddModelError("SafetyPointGroupRemoveInfo", Resource.removedError);
            }

            if (SafetyPointsLogic.RemoveSafetyPointGroup(a_viewModel.IdSafetyPointGroup))
            {
                return RedirectToAction("SafetyPointGroups", "Admin");
            }

            return View();
        }

        public ActionResult EditSafetyPointGroup(int a_idSafetyPointGroup)
        {
            var model = SafetyPointsLogic.GetEditSafetyPointGroupDto(a_idSafetyPointGroup);
            
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSafetyPointGroup(EditSafetyPointGroupDto a_editSafetyPointGroup)
        {
            if (ModelState.IsValid)
            {
                var model = SafetyPointsLogic.GetEditSafetyPointGroupDto(a_editSafetyPointGroup.IdSafetyPointGroup);

                if (a_editSafetyPointGroup.NameOfsafetyPointGroup != model.NameOfsafetyPointGroup)
                {
                    SafetyPointsLogic.RenameSafetyPointGroup(a_editSafetyPointGroup.IdSafetyPointGroup,
                                                              a_editSafetyPointGroup.NameOfsafetyPointGroup);
                }
                if (a_editSafetyPointGroup.IdNewAddSafetyPoint != 0)
                {
                    SafetyPointsLogic.AddSafetyPointToGroup(a_editSafetyPointGroup.IdSafetyPointGroup,
                                                            a_editSafetyPointGroup.IdNewAddSafetyPoint);
                    a_editSafetyPointGroup =
                            SafetyPointsLogic.GetEditSafetyPointGroupDto(a_editSafetyPointGroup.IdSafetyPointGroup);
                }

            }
            else
            {
                a_editSafetyPointGroup =
                        SafetyPointsLogic.GetEditSafetyPointGroupDto(a_editSafetyPointGroup.IdSafetyPointGroup);
            }

            return View(a_editSafetyPointGroup);
        }

        public ActionResult RemoveSafetyPointFromGroup(int a_idSafetyPointGroup, int a_idSafetyPoint)
        {
            var model = new RemoveSafetyPointFromGroup()
                {
                        IdSafetyPointGroup = a_idSafetyPointGroup,
                        IdSafetyPoint = a_idSafetyPoint
                };

            return View(model);
        }

        [HttpPost]
        public ActionResult RemoveSafetyPointFromGroup(RemoveSafetyPointFromGroup a_removeSafetyPointFromGroup)
        {
            if (ModelState.IsValid && SafetyPointsLogic.RemoveSafetyPointFromGroup(a_removeSafetyPointFromGroup))
            {
                return RedirectToAction("EditSafetyPointGroup", "Admin",
                                        new {a_idSafetyPointGroup = a_removeSafetyPointFromGroup.IdSafetyPointGroup});
            }
            ModelState.AddModelError("RemoveSafetyPointFromGroupInfo", Resource.removedError);

            return View(a_removeSafetyPointFromGroup);
        }

        public ActionResult Users()
        {
            var users = UserManagementAdminLogic.GetAllUsersForDiplay();

            return View(users);
        }

        public ActionResult ManageUserSafetyPointGroups(int a_idUser, string a_error)
        {
            var model = UserManagementAdminLogic.GetUserForManageSafetyPointGroups(a_idUser);

            if (null != a_error)
            {
                ModelState.AddModelError("ManageUserSafetyPointGroups", a_error);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ManageUserSafetyPointGroups(UserForManageSafetyPointGroups a_manageUser)
        {
            if (ModelState.IsValid)
            {
                if (0 != a_manageUser.IdAddToGroup)
                {
                    UserManagementAdminLogic.AddUserToSafetyPointGroup(a_manageUser.IdUser, a_manageUser.IdAddToGroup);

                    a_manageUser = UserManagementAdminLogic.GetUserForManageSafetyPointGroups(a_manageUser.IdUser);
                }
            }

            a_manageUser = UserManagementAdminLogic.GetUserForManageSafetyPointGroups(a_manageUser.IdUser);

            return View(a_manageUser);
        }

        public ActionResult RemoveUserFromSafetyPointGroup(int a_iduser, int a_idsafetypointgroup)
        {
            if (false == UserManagementAdminLogic.RemoveUserFromSafetyPointGroup(a_iduser, a_idsafetypointgroup))
            {
                return RedirectToAction("ManageUserSafetyPointGroups","Admin", new { a_idUser = a_iduser, a_error = Resource.removedError });
            }

            return RedirectToAction("ManageUserSafetyPointGroups", new { a_idUser = a_iduser });
        }
    }

   
}
