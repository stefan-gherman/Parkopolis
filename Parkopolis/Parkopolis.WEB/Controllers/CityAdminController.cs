using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parkopolis.WEB.Controllers
{
    public class CityAdminController : Controller
    {
        // GET: CityAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CityAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CityAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityAdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CityAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CityAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CityAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
