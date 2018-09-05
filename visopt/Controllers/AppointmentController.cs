using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using visopt.DataAccess;
using visopt.Models;

namespace visopt.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentRepo _appointment;

        public AppointmentController()
        {
            _appointment = new AppointmentRepo();
        }
        // GET: Viso
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _appointment.All());
        }

        [HttpGet]
        public ActionResult Create()
        {
            Appointment model = new Appointment();
            model.LoadAdditionalData();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Appointment appointment)
        {
            try
            {
                appointment.LoadAdditionalData();
                if (!ModelState.IsValid)
                    return View(appointment);

                await this._appointment.AddOrUpdate(appointment);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(appointment);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var model = await _appointment.Find(id);
            model.LoadAdditionalData();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _appointment.Find(id);
            model.LoadAdditionalData();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Appointment appointment)
        {
            try
            {
                appointment.LoadAdditionalData();
                if (!ModelState.IsValid)
                    return View(appointment);

                await _appointment.AddOrUpdate(appointment);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(appointment);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _appointment.Find(id);
            model.LoadAdditionalData();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Appointment appointment)
        {
            await _appointment.Remove(appointment.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
