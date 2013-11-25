using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingCamp.Web.Controllers
{
    public class CampMemberController : Controller
    {
        //
        // GET: /CampMember/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CampMember/Details/5

        public ActionResult Details(string cid)
        {
            return View();
        }

        //
        // GET: /CampMember/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /CampMember/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /CampMember/Edit/5

        public ActionResult Edit(string cid)
        {
            return View();
        }

        //
        // POST: /CampMember/Edit/5

        [HttpPost]
        public ActionResult Edit(string cid, FormCollection collection)
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

        //
        // GET: /CampMember/Delete/5

        public ActionResult Delete(string cid)
        {
            return View();
        }

        //
        // POST: /CampMember/Delete/5

        [HttpPost]
        public ActionResult Delete(string cid, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
