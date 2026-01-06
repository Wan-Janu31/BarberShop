using System.Collections.Generic;
using System.Threading.Tasks;
using Barber_Service.Models;

namespace Barber_Service.Repository.Interfaces
{
    public interface IBarberRepository
    {
        // Lấy danh sách
        Task<IEnumerable<Barber>> GetAllAsync();

        // Lấy theo ID
        Task<Barber?> GetByIdAsync(int id);

        // Lấy theo ID kèm chi tiết (Bookings, TimeSlots)
        Task<Barber?> GetByIdWithDetailsAsync(int id);

        // Lọc theo trạng thái
        Task<IEnumerable<Barber>> GetByStatusAsync(string status);

        // Tìm kiếm theo tên
        Task<IEnumerable<Barber>> SearchByNameAsync(string name);

        // Tạo mới
        Task<Barber> CreateAsync(Barber barber);

        // Cập nhật
        Task<Barber?> UpdateAsync(int id, Barber barber);

        // Xóa
        Task<bool> DeleteAsync(int id);

        // Kiểm tra tồn tại
        Task<bool> ExistsAsync(int id);

        // Kiểm tra số điện thoại đã tồn tại (có thể loại trừ một ID)
        Task<bool> PhoneExistsAsync(string phone, int? excludeId = null);
    }
}