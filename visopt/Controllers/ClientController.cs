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
    public class ClientController : Controller
    {
        private readonly ClientRepo _client;

        public ClientController()
        {
            _client = new ClientRepo();
        }
        // GET: Viso
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(await _client.All());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Client client)
        {
            try
            {
                if(!await _client.ValidateClientMobile(client.MobileNo))
                    ModelState.AddModelError(nameof(client.MobileNo),"Mobile number is all ready registered.");

                if (!ModelState.IsValid)
                    return View(client);

                await _client.AddOrUpdate(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(client);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _client.Find(id));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            return View(await _client.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Client client)
        {
            try
            {
                if (!await _client.ValidateClientMobile(client.MobileNo))
                    ModelState.AddModelError(nameof(client.MobileNo), "Mobile number is all ready registered.");

                if (!ModelState.IsValid)
                    return View(client);

                await _client.AddOrUpdate(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(client);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            return View(await _client.Find(id));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Client client)
        {
            await _client.Remove(client.Id);
            return RedirectToAction(nameof(Index));
        }

    }
}