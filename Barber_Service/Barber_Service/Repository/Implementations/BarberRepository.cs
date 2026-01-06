using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Barber_Service.Models;
using Barber_Service.Repository.Interfaces;

namespace Barber_Service.Repository.Implementations
{
    public class BarberRepository : IBarberRepository
    {
        private readonly BarberBookingDbContext _db;

        public BarberRepository(BarberBookingDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Barber>> GetAllAsync()
        {
            return await _db.Barbers
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Barber?> GetByIdAsync(int id)
        {
            return await _db.Barbers
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Barber?> GetByIdWithDetailsAsync(int id)
        {
            return await _db.Barbers
                .Include(b => b.Bookings)
                .Include(b => b.TimeSlots)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Barber>> GetByStatusAsync(string status)
        {
            return await _db.Barbers
                .Where(b => b.Status == status)
                .OrderByDescending(b => b.RatingAvg)
                .ToListAsync();
        }

        public async Task<IEnumerable<Barber>> SearchByNameAsync(string name)
        {
            return await _db.Barbers
                .Where(b => b.FullName.Contains(name))
                .OrderBy(b => b.FullName)
                .ToListAsync();
        }

        public async Task<Barber> CreateAsync(Barber barber)
        {
            await _db.Barbers.AddAsync(barber);
            await _db.SaveChangesAsync();
            return barber;
        }

        public async Task<Barber?> UpdateAsync(int id, Barber barber)
        {
            var existingBarber = await _db.Barbers.FindAsync(id);
            if (existingBarber == null)
                return null;

            // Update properties
            _db.Entry(existingBarber).CurrentValues.SetValues(barber);
            await _db.SaveChangesAsync();
            return existingBarber;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var barber = await _db.Barbers.FindAsync(id);
            if (barber == null)
                return false;

            _db.Barbers.Remove(barber);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Barbers.AnyAsync(b => b.Id == id);
        }

        public async Task<bool> PhoneExistsAsync(string phone, int? excludeId = null)
        {
            var query = _db.Barbers.Where(b => b.Phone == phone);

            if (excludeId.HasValue)
                query = query.Where(b => b.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}