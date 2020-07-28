using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Parkopolis.WEB.Controllers
{
    public class ParkingAdminController : Controller
    {
        // GET: ParkingAdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ParkingAdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParkingAdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkingAdminController/Create
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

        // GET: ParkingAdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParkingAdminController/Edit/5
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

        // GET: ParkingAdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParkingAdminController/Delete/5
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
