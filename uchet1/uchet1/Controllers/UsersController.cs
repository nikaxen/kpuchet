using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uchet1.DAO;
using uchet1.Models;

namespace uchet1.Controllers
{
    public class UsersController : Controller
    {
        RecordsDAO recordsDAO = new RecordsDAO();
        [Authorize(Roles = "dek")]
        public ActionResult Students() {
            foreach (var item in recordsDAO.dekGetAllStuds())
            {
                    ViewData[item.Id] = recordsDAO.dekGetStudGroup(item.Id);
            
            }
            return View("Students", recordsDAO.dekGetAllStuds());
        }


        [Authorize(Roles = "dek")]
        public ActionResult SetGroupForStudent(string newgroupfor) {
            Users users = new Users();
            foreach (var item in recordsDAO.getUserById(newgroupfor)) {
                users.Id = item.Id;
                users.fio = item.fio;
                    }
            return View("SetGroupForStudent", users);
        }


        [Authorize(Roles = "dek")]
        [HttpPost]
        public ActionResult SetGroupForStudent(Users users) {
            try
            {
                if (recordsDAO.AddGroupForStudent(users.Id, users.studgroupid))
                {
                    return RedirectToAction("Students");
                }
                else
                {
                    return View("SetGroupForStudent", users);
                }
            }
            catch
            {
                return View("Students");
            }
        }

        // GET: Users
        [Authorize(Roles ="dek")]
        public ActionResult Index()
        {
           
            return View(recordsDAO.dekGETALLUSERS());
        }
        // GET: Users/UserRoles
        [Authorize(Roles = "dek")]
        public ActionResult UserRoles() {
            return View("UserRoles", recordsDAO.ShowUserRoles());
        }


        // GET: Users/SetUserRole/userid?roleid=roleid
        [Authorize(Roles = "dek")]
        public ActionResult SetUserRole(string userid, string roleid) {
            
            recordsDAO.SetUserRole(userid, roleid);
            return RedirectToAction("UserRoles");
        }


        // GET Users/Edit
        [Authorize(Roles ="dek")]
        public ActionResult Edit(string id)
        {
            return View();
        }



        // POST: Users/Edit/5
        [Authorize(Roles = "dek")]
        [HttpPost]
        public ActionResult Edit(string id, int role_id)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
    }
}
