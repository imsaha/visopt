using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using visopt.Models;

namespace visopt.DataAccess
{
    public class AppointmentRepo
    {
        private readonly VisoContext _context;

        public AppointmentRepo(VisoContext context)
        {
            _context = context;
        }

        public AppointmentRepo()
        {
            _context = new VisoContext();
        }

        public async Task<Appointment> Find(int id)
        {
            return await _context.Appointments
                .Include(d => d.Doctor).Include(d => d.Client)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Appointment>> All()
        {
            return await _context.Appointments.Include(d=>d.Doctor).Include(d => d.Client)?.ToListAsync();
        }

        public async Task<List<Appointment>> Find(Expression<Func<Appointment, bool>> predicate)
        {
            return await _context.Appointments.Where(predicate)?.ToListAsync();
        }

        /// <summary>
        /// Returns all the Appointment related to the doctor 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<List<Appointment>> FindbyDoctorId(int doctorId)
        {
            return await _context.Appointments
                 .Include(d => d.Doctor)
                .Include(d => d.Client)
                .Where(x => x.DoctorId == doctorId)?.ToListAsync();
        }
        
        /// <summary>
        /// Returns all the Appointment related to the client 
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public async Task<List<Appointment>> FindbyClientId(int clientId)
        {
            return await _context.Appointments
                .Include(d=>d.Doctor)
                .Include(d=>d.Client)
                .Where(x => x.ClientId == clientId)?.ToListAsync();
        }


        public async Task<int> AddOrUpdate(Appointment appointment)
        {
            appointment.Doctor= await _context.Doctors.FirstOrDefaultAsync(x => x.Id == appointment.DoctorId);
            if (appointment.Validate())
            {
                Appointment findAppointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == appointment.Id);
                if (findAppointment != null)
                {
                    findAppointment.ClientId = appointment.ClientId;
                    findAppointment.DoctorId = appointment.DoctorId;
                    findAppointment.StartDateTime = appointment.StartDateTime;
                    findAppointment.EndsDateTime = appointment.EndsDateTime;
                }
                else
                {
                    _context.Appointments.Add(appointment);
                }
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Current appointment is not in correct timespan");
            }
        }

        public async Task<int> Remove(int appointmentId)
        {
            Appointment findAppointment = await _context.Appointments.FirstOrDefaultAsync(x => x.Id == appointmentId);
            if (findAppointment != null)
            {
                _context.Appointments.Remove(findAppointment);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Appointment with current id not found.");
            }
        }
    }
}