using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using uchet1.DAO;
using uchet1.Models;
using Microsoft.AspNet.Identity;

namespace uchet1.Controllers
{   [Authorize]
    public class HomeController : Controller
    {
        RecordsDAO recordsDAO = new RecordsDAO();
        List<Statement> stm_list;

        // GET: Home (for neizv)
        public ActionResult Index()
        {
            if (User.IsInRole("dek"))
            {
                return View("ViewStatements", recordsDAO.dekGETALLSTATEMENTS());
            }
            else if (User.IsInRole("prep"))
            {
                string prepid = User.Identity.GetUserId();
                return View("ViewStatements", recordsDAO.getStatementsForPrep(prepid));
            }
            else if (User.IsInRole("stud")) {
                string studid = User.Identity.GetUserId();
                ViewData["this_view_title"] = "Просмотр всех оценок студента";
                return View("studViewMarks", recordsDAO.studGetMarks(studid, "all"));
            } else {
                return View("neizv-user");
            }
        }

        public ActionResult BadMarksOnly() {
            string studid = User.Identity.GetUserId();
            ViewData["this_view_title"] = "Просмотр неудовлетворительных оценок студента";
            return View("studViewMarks", recordsDAO.studGetMarks(studid, "badmarksonly"));
        }

        public ActionResult MarkDetails(int markid) {
            return View("MarkDetails", recordsDAO.getMarkDetails(markid));
        }



        public ActionResult SearchByStatements(string sq) {
            if (!String.IsNullOrEmpty(sq))
            {
                return View("SearchByStatements", recordsDAO.SearchByStatements(sq));
            }
            else {
return View("SearchByStatements", recordsDAO.dekGETALLSTATEMENTS());
            }
            
        }

        public ActionResult StatementDetails(int id) {
            stm_list = recordsDAO.getStatementById(id);
            foreach (var item in stm_list)
            {
                ViewData["statement_title"] = item.title;
                ViewData["statement_subject"] = item.subjtitle;
                ViewData["statement_status"] = item.status;

            }
            foreach (var it in recordsDAO.GetStudGroupByStatementID(id))
            {
                ViewData["idstudgr"] = it.idstudgroup;
                ViewData["studgrname"] = it.studgroupname;
            }
            ViewData["statement_id"] = id;
            if (User.IsInRole("dek"))
            {
                return View("dekStatementDetails", recordsDAO.getMarksForStatement(id));
            }
            else
            if (User.IsInRole("prep"))
            {
                ViewData["mymodel"] = (IEnumerable<uchet1.Models.Mark>)recordsDAO.getMarksForStatement(id);
                return View("StatementDetails", new Mark { id_statement = id });
            }
            else { return View("Index"); }
            

        }
        [Authorize(Roles ="prep")]
        // POST: Home/StatementDetails
        [HttpPost]
        public ActionResult StatementDetails(Mark mark)
        {
            ModelState.Remove("id_mark");
            try
            {

                if (recordsDAO.AddMark(mark))
                {
                    return RedirectToAction("StatementDetails", recordsDAO.getMarksForStatement(mark.id_statement));
                }
                else
                {
                    return View("StatementDetails");
                }
            }
            catch
            {
                return View("StatementDetails");
            }

        }









        [Authorize(Roles ="dek")]
        public ActionResult ReadyStatements() {
            return View("ViewReadyStatements", recordsDAO.ShowReadyStatements());
        }


        [Authorize(Roles = "prep")]
        public ActionResult RemoveMarkFromStatement(int removemarkid, int statementid) {
            int id = statementid;
            try
            {
                recordsDAO.RemoveMarkFromStatement(removemarkid);
                return RedirectToAction("StatementDetails", new { id=statementid});
            }
            catch
            {
                return RedirectToAction("StatementDetails", recordsDAO.getMarksForStatement(statementid));
            }
        }


        [Authorize(Roles = "dek,prep")]
        public ActionResult StatementSetStatus(int id, string status) {
            recordsDAO.StatementSetStatus(id, status);
            return RedirectToAction("Index");
        }
        




        [Authorize(Roles ="dek")]
        // GET: Home/CreateStatement
        public ActionResult CreateStatement()
        {
            return View("dekCreateStatement");
        }




        [Authorize(Roles ="dek")]
        // POST: Home/CreateStatement
        [HttpPost]
        public ActionResult CreateStatement(Statement statement)
        {
            ModelState.Remove("id_statement");
            ModelState.Remove("status");
            try
            {
                if (recordsDAO.AddStatement(statement))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("dekCreateStatement");
                }
            }
            catch
            {
                return View("dekCreateStatement");
            }

        }
        
    }
}
