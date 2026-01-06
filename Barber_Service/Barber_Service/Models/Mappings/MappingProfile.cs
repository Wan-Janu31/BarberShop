using AutoMapper;
using Barber_Service.DTOs.Barber;
using Barber_Service.Models;
using Barber_Service.Models.DTOs;

namespace Barber_Service.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer mappings (existing)
            CreateMap<CustomerDTO, Customer>().ReverseMap();

            // Barber mappings
            // Map từ Model sang DTO
            CreateMap<Barber, BarberDto>();

            CreateMap<Barber, BarberDetailDto>()
                .ForMember(dest => dest.TotalBookings, opt => opt.MapFrom(src => src.Bookings.Count))
                .ForMember(dest => dest.TotalTimeSlots, opt => opt.MapFrom(src => src.TimeSlots.Count));

            // Map từ CreateDTO sang Model
            CreateMap<CreateBarberDto, Barber>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RatingAvg, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.RatingCount, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Bookings, opt => opt.Ignore())
                .ForMember(dest => dest.TimeSlots, opt => opt.Ignore());

            // Map từ UpdateDTO sang Model (chỉ update field không null)
            CreateMap<UpdateBarberDto, Barber>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}