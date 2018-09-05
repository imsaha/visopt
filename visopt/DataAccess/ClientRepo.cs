using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using visopt.Models;

namespace visopt.DataAccess
{
    public class ClientRepo
    {
        private readonly VisoContext _context;

        public ClientRepo(VisoContext context)
        {
            _context = context;
        }
        public ClientRepo()
        {
            _context = new VisoContext();
        }

        public async Task<bool> ValidateClientMobile(string MobileNo)
        {
            var find = await _context.Clients.FirstOrDefaultAsync(x=>x.MobileNo==MobileNo);
            return find == null;
        }


        public async Task<Client> Find(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Client> Find(string mobile)
        {
            return await _context.Clients.FirstOrDefaultAsync(x => x.MobileNo == mobile);
        }
        public async Task<List<Client>> Find(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate)?.ToListAsync();
        }

        public async Task<List<Client>> All()
        {
            return await _context.Clients?.ToListAsync();
        }        

        public async Task<int> AddOrUpdate(Client client)
        {
            var findClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == client.Id);
            if (findClient != null)
            {
                findClient.FirstName = client.FirstName;
                findClient.LastName = client.LastName;
                findClient.MobileNo = client.MobileNo;
            }
            else
            {
                _context.Clients.Add(client);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Remove(int clientId)
        {
            var findClient = await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);
            if (findClient != null)
            {
                _context.Clients.Remove(findClient);
                return await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Doctor not found with provided id.");
            }
        }
    }
}