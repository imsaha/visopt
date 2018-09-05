using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using visopt.Models;

namespace visopt.DataAccess
{
    public class DoctorRepo
    {
        private readonly VisoContext _context;

        public DoctorRepo(VisoContext context)
        {
            _context = context;
        }

        public DoctorRepo()
        {
            _context = new VisoContext();
        }

        public async Task<Doctor> Find(int id)
        {
            return await _context.Doctors.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Doctor>> Find(Expression<Func<Doctor,bool>> predicate)
        {
            return await _context.Doctors.Where(predicate)?.ToListAsync();
        }

        public async Task<List<Doctor>> All()
        {
            return await _context.Doctors?.ToListAsync();
        }

        public async Task<int> AddOrUpdate(Doctor doctor)
        {
            var findDoc =await _context.Doctors.FirstOrDefaultAsync(x => x.Id == doctor.Id);
            if (findDoc !=null)
            {
                findDoc.FirstName = doctor.FirstName;
                findDoc.LastName = doctor.LastName;
                findDoc.StartWorkingHour = doctor.StartWorkingHour;
                findDoc.EndWorkingHour = doctor.EndWorkingHour;
            }
            else
            {
                _context.Doctors.Add(doctor);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Remove(int doctorId)
        {
            var findDoc = await _context.Doctors.FirstOrDefaultAsync(x => x.Id == doctorId);
            if (findDoc != null)
            {
                _context.Doctors.Remove(findDoc);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Doctor not found with provided id.");
            }
        }
    }
}