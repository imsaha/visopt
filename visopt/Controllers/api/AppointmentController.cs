using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using visopt.DataAccess;
using visopt.Models;

namespace visopt.Controllers.api
{
    public class AppointmentController : ApiController
    {
        private readonly AppointmentRepo _appontment;

        public AppointmentController()
        {
            _appontment = new AppointmentRepo();
        }

        [HttpGet]
        public async Task<Appointment> Find(int id)
        {
            return await _appontment.Find(id);
        }


        [HttpGet]
        public async Task<List<Appointment>> ByClient(int id)
        {
            var appointments = await _appontment.FindbyClientId(id);
            return appointments.OrderByDescending(x => x.StartDateTime)?.ToList();
        }

        [HttpGet]
        public async Task<List<Appointment>> ByDoctor(int id)
        {
            var appointments = await _appontment.FindbyDoctorId(id);
            return appointments.OrderByDescending(x => x.StartDateTime)?.ToList();
        }

        [HttpDelete]
        public async Task<int> Remove(int id)
        {
            return await _appontment.Remove(id);
        }

        [HttpPost]
        public async Task<int> Book(Appointment data)
        {
            return await _appontment.AddOrUpdate(data);
        }
    }
}
