using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using visopt.DataAccess;
using visopt.Models;

namespace visopt.Controllers.api
{
    public class DoctorController : ApiController
    {
        private readonly DoctorRepo _doctor;

        public DoctorController()
        {
            _doctor = new DoctorRepo();
        }

        [HttpGet]
        public async Task<ICollection<Doctor>> All()
        {
            return await _doctor.All();
        }

        [HttpGet]
        public async Task<Doctor> Find(int id)
        {
            return await _doctor.Find(id);
        }

        [HttpDelete]
        public async Task<int> Remove(int id)
        {
            return await _doctor.Remove(id);
        }

        [HttpPost]
        public async Task<int> Add(Doctor doctor)
        {
            return await _doctor.AddOrUpdate(doctor);
        }
    }
}
