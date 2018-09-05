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
    public class DoctorController : Controller
    {
        private readonly DoctorRepo _doctor;

        public DoctorController()
        {
            _doctor = new DoctorRepo();
        }
        // GET: Viso
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _doctor.All());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(doctor);

                await _doctor.AddOrUpdate(doctor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(doctor);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _doctor.Find(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _doctor.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Doctor doctor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(doctor);

                await _doctor.AddOrUpdate(doctor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(doctor);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _doctor.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Doctor doctor)
        {
            await _doctor.Remove(doctor.Id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Search(int type, string search)
        {
            List<Doctor> doctors = null;
            switch (type)
            {
                case 1:
                    doctors = await _doctor.Find(x => x.FirstName == search);
                    break;
                case 2:
                    doctors = await _doctor.Find(x => x.LastName == search);
                    break;
                case 3:

                    DateTime start;
                    bool startParsed = DateTime.TryParse(search, out start);
                    doctors = await _doctor.Find(x => x.StartWorkingHour ==(startParsed ? start : x.StartWorkingHour));
                    break;
                case 4:
                    DateTime end;
                    bool endParsed = DateTime.TryParse(search, out end);
                    doctors = await _doctor.Find(x => x.EndWorkingHour == ((endParsed ? end : x.StartWorkingHour)));
                    break;
                default:
                    break;
            }


            return View(doctors);
        }


        public async Task<ActionResult> Appointments(int id)
        {
            AppointmentRepo _repo = new AppointmentRepo();
            List<Appointment> data = await _repo.FindbyDoctorId(id);
            return View(data.OrderByDescending(d => d.StartDateTime)?.ToList());
        }
    }
}
