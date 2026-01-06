using System.Collections.Generic;
using System.Threading.Tasks;
using Barber_Service.DTOs.Barber;
using Barber_Service.Models.DTOs;

namespace Barber_Service.Service.Interfaces
{
    public interface IBarberService
    {
        // Lấy tất cả barbers
        Task<IEnumerable<BarberDto>> GetAllBarbersAsync();

        // Lấy barber theo ID
        Task<BarberDto?> GetBarberByIdAsync(int id);

        // Lấy barber chi tiết (bao gồm bookings và timeslots)
        Task<BarberDetailDto?> GetBarberDetailByIdAsync(int id);

        // Lấy barbers theo trạng thái
        Task<IEnumerable<BarberDto>> GetBarbersByStatusAsync(string status);

        // Tìm kiếm barbers theo tên
        Task<IEnumerable<BarberDto>> SearchBarbersByNameAsync(string name);

        // Tạo mới barber
        Task<BarberDto> CreateBarberAsync(CreateBarberDto createDto);

        // Cập nhật barber
        Task<BarberDto?> UpdateBarberAsync(int id, UpdateBarberDto updateDto);

        // Xóa barber
        Task<bool> DeleteBarberAsync(int id);
    }
}