using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Barber_Service.Models.DTOs;
using Barber_Service.Service.Interfaces;
using Barber_Service.DTOs.Barber;

namespace Barber_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly IBarberService _barberService;

        public BarberController(IBarberService barberService)
        {
            _barberService = barberService;
        }

        /// <summary>
        /// Lấy danh sách tất cả thợ cắt tóc
        /// </summary>
        /// <returns>Danh sách thợ cắt tóc</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BarberDto>>> GetAllBarbers()
        {
            var barbers = await _barberService.GetAllBarbersAsync();
            return Ok(barbers);
        }

        /// <summary>
        /// Lấy thông tin thợ cắt tóc theo ID
        /// </summary>
        /// <param name="id">ID của thợ cắt tóc</param>
        /// <returns>Thông tin thợ cắt tóc</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BarberDto>> GetBarberById(int id)
        {
            var barber = await _barberService.GetBarberByIdAsync(id);

            if (barber == null)
                return NotFound(new { message = "Không tìm thấy thợ cắt tóc" });

            return Ok(barber);
        }

        /// <summary>
        /// Lấy thông tin chi tiết thợ cắt tóc bao gồm bookings và timeslots
        /// </summary>
        /// <param name="id">ID của thợ cắt tóc</param>
        /// <returns>Thông tin chi tiết thợ cắt tóc</returns>
        [HttpGet("{id}/detail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BarberDetailDto>> GetBarberDetail(int id)
        {
            var barber = await _barberService.GetBarberDetailByIdAsync(id);

            if (barber == null)
                return NotFound(new { message = "Không tìm thấy thợ cắt tóc" });

            return Ok(barber);
        }

        /// <summary>
        /// Lấy danh sách thợ cắt tóc theo trạng thái
        /// </summary>
        /// <param name="status">Trạng thái (Active, Inactive, OnLeave)</param>
        /// <returns>Danh sách thợ cắt tóc</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BarberDto>>> GetBarbersByStatus(string status)
        {
            if (!IsValidStatus(status))
                return BadRequest(new { message = "Trạng thái không hợp lệ. Chỉ chấp nhận: Active, Inactive, OnLeave" });

            var barbers = await _barberService.GetBarbersByStatusAsync(status);
            return Ok(barbers);
        }

        /// <summary>
        /// Tìm kiếm thợ cắt tóc theo tên
        /// </summary>
        /// <param name="name">Tên cần tìm</param>
        /// <returns>Danh sách thợ cắt tóc</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BarberDto>>> SearchBarbers([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "Tên tìm kiếm không được để trống" });

            var barbers = await _barberService.SearchBarbersByNameAsync(name);
            return Ok(barbers);
        }

        /// <summary>
        /// Tạo mới thợ cắt tóc
        /// </summary>
        /// <param name="createDto">Thông tin thợ cắt tóc mới</param>
        /// <returns>Thông tin thợ cắt tóc đã tạo</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BarberDto>> CreateBarber([FromBody] CreateBarberDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var barber = await _barberService.CreateBarberAsync(createDto);
                return CreatedAtAction(nameof(GetBarberById), new { id = barber.Id }, barber);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật thông tin thợ cắt tóc
        /// </summary>
        /// <param name="id">ID của thợ cắt tóc</param>
        /// <param name="updateDto">Thông tin cần cập nhật</param>
        /// <returns>Thông tin thợ cắt tóc đã cập nhật</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BarberDto>> UpdateBarber(int id, [FromBody] UpdateBarberDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var barber = await _barberService.UpdateBarberAsync(id, updateDto);

                if (barber == null)
                    return NotFound(new { message = "Không tìm thấy thợ cắt tóc" });

                return Ok(barber);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa thợ cắt tóc
        /// </summary>
        /// <param name="id">ID của thợ cắt tóc</param>
        /// <returns>Kết quả xóa</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteBarber(int id)
        {
            var result = await _barberService.DeleteBarberAsync(id);

            if (!result)
                return NotFound(new { message = "Không tìm thấy thợ cắt tóc" });

            return Ok(new { message = "Xóa thợ cắt tóc thành công" });
        }

        // Helper method để validate status
        private bool IsValidStatus(string status)
        {
            return status == "Active" || status == "Inactive" || status == "OnLeave";
        }
    }
}