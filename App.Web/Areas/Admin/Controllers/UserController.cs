using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace App.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        #region DB
        private Data.orm.UnitOfWork _unitOfWork;
        public Data.orm.UnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork = new Data.orm.UnitOfWork();
                return _unitOfWork;
            }
            set { _unitOfWork = value; }
        }
        #endregion

        public ActionResult Index()
        {
            var users = this.UnitOfWork.UserRepository.Get();
            return View(users);
        }

        public ActionResult Create()
        {
            var model = new App.Web.Areas.Admin.Models.MemberEditViewModel()
            {
                Roles = new MultiSelectList(UnitOfWork.RoleRepository.Get(), "Id", "Name"),
                Member = new global::Models.Member()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(App.Web.Areas.Admin.Models.MemberEditViewModel memberViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    memberViewModel.Roles = new MultiSelectList(UnitOfWork.RoleRepository.Get(), "Id", "Name");
                    return View(memberViewModel);
                }

                var member = memberViewModel.Member;
                var ids = memberViewModel.SelectedRolesIds;                

                var userInfo = new { Email = member.Email, FirstName = member.Person.FirstName, Surname = member.Person.SurName, PersonType = "PERSON", Roles = ids };
                WebSecurity.CreateUserAndAccount(member.UserName, member.Password, userInfo, false);

                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                memberViewModel.Roles = new MultiSelectList(UnitOfWork.RoleRepository.Get(), "Id", "Name");
                return View(memberViewModel);
            }
        }

        public ActionResult Edit(Int32 id)
        {
            var member = this.UnitOfWork.UserRepository.GetByID(id);
            if (member == null)
                return RedirectToAction("Index", "User", new { area = "Admin" });

            int[] ids = null;
            if (member.Roles != null && member.Roles.Count > 0)
            {
                ids = new int[member.Roles.Count];
                int i = 0;
                foreach (var role in member.Roles)
                {
                    ids[i] = role.Id;
                    i++;
                }
            }
            var model = new App.Web.Areas.Admin.Models.MemberEditViewModel()
            {
                SelectedRolesIds = ids,
                Roles = new MultiSelectList(this.UnitOfWork.RoleRepository.Get(), "Id", "Name"),
                Member = (global::Models.Member)member
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(App.Web.Areas.Admin.Models.MemberEditViewModel memberViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(new App.Web.Areas.Admin.Models.MemberEditViewModel()
                    {
                        SelectedRolesIds = memberViewModel.SelectedRolesIds,
                        Roles = new MultiSelectList(this.UnitOfWork.RoleRepository.Get(), "Id", "Name"),
                        Member = memberViewModel.Member
                    });

                //GET ORGINAL MODEL
                var modelOrginal = (global::Models.Member)this.UnitOfWork.UserRepository.GetByID(memberViewModel.Member.Id);
                //ADD NEW VALUES
                modelOrginal.ModifiedDate = DateTime.UtcNow;
                modelOrginal.UserName = memberViewModel.Member.UserName;
                modelOrginal.Email = memberViewModel.Member.UserName;
                modelOrginal.Password = memberViewModel.Member.Password;
                modelOrginal.ConfirmPassword = memberViewModel.Member.ConfirmPassword;
                modelOrginal.Person.FirstName = memberViewModel.Member.Person.FirstName;
                modelOrginal.Person.SurName = memberViewModel.Member.Person.SurName;
                var ids = memberViewModel.SelectedRolesIds;
                if (ids != null && ids.Length > 0)
                {
                    if (modelOrginal.Roles != null)
                    {
                        modelOrginal.Roles.Clear();
                    }
                    var roles = new List<global::Models.Role>();
                    foreach (var id in ids)
                    {
                        var role = this.UnitOfWork.RoleRepository.GetByID(Convert.ToInt16(id));
                        roles.Add(role);
                    }
                    modelOrginal.Roles = roles;
                }
                this.UnitOfWork.UserRepository.Update(modelOrginal);
                this.UnitOfWork.Save();

                return RedirectToAction("Index", "User", new { area = "Admin" });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(new App.Web.Areas.Admin.Models.MemberEditViewModel()
                {
                    SelectedRolesIds = memberViewModel.SelectedRolesIds,
                    Roles = new MultiSelectList(this.UnitOfWork.RoleRepository.Get(), "Id", "Name"),
                    Member = memberViewModel.Member
                });
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete(Int32 id)
        {
            string message = "";
            bool state = false;
            try
            {
                var member = this.UnitOfWork.UserRepository.GetByID(id);
                if (member != null)
                {
                    this.UnitOfWork.UserRepository.Delete(member);
                    this.UnitOfWork.Save();

                    message = string.Format("Deleted Id '{0}' from the database!", id);
                    state = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Json(new { id = id, state = state, message = message });
        }
    }
}
