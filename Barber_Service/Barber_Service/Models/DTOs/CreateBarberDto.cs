using System;
using System.ComponentModel.DataAnnotations;

namespace Barber_Service.DTOs.Barber
{
    // DTO cho việc tạo mới Barber
    public class CreateBarberDto
    {
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Số năm kinh nghiệm là bắt buộc")]
        [Range(0, 50, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 50")]
        public int ExperienceYears { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^(\+84|0)[0-9]{9,10}$", ErrorMessage = "Số điện thoại phải đúng định dạng Việt Nam")]
        public string Phone { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc")]
        [RegularExpression("^(Active|Inactive|OnLeave)$", ErrorMessage = "Trạng thái phải là Active, Inactive hoặc OnLeave")]
        public string Status { get; set; } = "Active";
    }

    // DTO cho việc cập nhật Barber
    public class UpdateBarberDto
    {
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        public string? FullName { get; set; }

        public string? ImageUrl { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        [Range(0, 50, ErrorMessage = "Số năm kinh nghiệm phải từ 0 đến 50")]
        public int? ExperienceYears { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [RegularExpression(@"^(\+84|0)[0-9]{9,10}$", ErrorMessage = "Số điện thoại phải đúng định dạng Việt Nam")]
        public string? Phone { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả không được vượt quá 500 ký tự")]
        public string? Description { get; set; }

        [RegularExpression("^(Active|Inactive|OnLeave)$", ErrorMessage = "Trạng thái phải là Active, Inactive hoặc OnLeave")]
        public string? Status { get; set; }
    }

    // DTO cho response
    public class BarberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int ExperienceYears { get; set; }
        public string Phone { get; set; } = null!;
        public string? Description { get; set; }
        public double RatingAvg { get; set; }
        public int RatingCount { get; set; }
        public string Status { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }

    // DTO chi tiết bao gồm các booking và timeslots
    public class BarberDetailDto : BarberDto
    {
        public int TotalBookings { get; set; }
        public int TotalTimeSlots { get; set; }
    }
}