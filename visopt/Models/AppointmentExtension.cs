using System;
using System.Linq;

namespace visopt.Models
{
    public static class AppointmentExtension
    {
        /// <summary>
        /// It's required to load doctor in order to validate
        /// </summary>
        /// <returns></returns>
        public static bool Validate(this Appointment appointment)
        {
            if(appointment.StartDateTime.TimeOfDay >= appointment.Doctor.StartWorkingHour.TimeOfDay &&
                appointment.EndsDateTime.TimeOfDay <= appointment.Doctor.EndWorkingHour.TimeOfDay &&
                appointment.StartDateTime > DateTime.Now &&
                appointment.EndsDateTime > DateTime.Now)
            {
                using (VisoContext context = new VisoContext())
                {
                    return context.Appointments
                           .Count(x => x.ClientId == appointment.ClientId &&
                                  x.StartDateTime.Day == appointment.StartDateTime.Day) == 0;

                }
            }
            else
            {
                return false;
            }
        }
               
        public static void LoadAdditionalData(this Appointment appointment)
        {
            VisoContext context = new VisoContext();
            appointment.Doctors = context.Doctors?.ToList();
            appointment.Clients = context.Clients?.ToList();
        }
    }
}