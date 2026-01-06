using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Barber_Service.DTOs.Barber;
using Barber_Service.Models;
using Barber_Service.Models.DTOs;
using Barber_Service.Repository.Interfaces;
using Barber_Service.Service.Interfaces;

namespace Barber_Service.Service.Implementations
{
    public class BarberService : IBarberService
    {
        private readonly IBarberRepository _repository;
        private readonly IMapper _mapper;

        public BarberService(IBarberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BarberDto>> GetAllBarbersAsync()
        {
            var barbers = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BarberDto>>(barbers);
        }

        public async Task<BarberDto?> GetBarberByIdAsync(int id)
        {
            var barber = await _repository.GetByIdAsync(id);
            return barber == null ? null : _mapper.Map<BarberDto>(barber);
        }

        public async Task<BarberDetailDto?> GetBarberDetailByIdAsync(int id)
        {
            var barber = await _repository.GetByIdWithDetailsAsync(id);
            return barber == null ? null : _mapper.Map<BarberDetailDto>(barber);
        }

        public async Task<IEnumerable<BarberDto>> GetBarbersByStatusAsync(string status)
        {
            var barbers = await _repository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<BarberDto>>(barbers);
        }

        public async Task<IEnumerable<BarberDto>> SearchBarbersByNameAsync(string name)
        {
            var barbers = await _repository.SearchByNameAsync(name);
            return _mapper.Map<IEnumerable<BarberDto>>(barbers);
        }

        public async Task<BarberDto> CreateBarberAsync(CreateBarberDto createDto)
        {
            // Kiểm tra số điện thoại đã tồn tại chưa
            if (await _repository.PhoneExistsAsync(createDto.Phone))
            {
                throw new InvalidOperationException("Số điện thoại đã được sử dụng");
            }

            // Kiểm tra tuổi hợp lệ (ít nhất 18 tuổi)
            var age = DateTime.Today.Year - createDto.DateOfBirth.Year;
            if (createDto.DateOfBirth > DateOnly.FromDateTime(DateTime.Today.AddYears(-age))) age--;

            if (age < 18)
            {
                throw new InvalidOperationException("Thợ cắt tóc phải đủ 18 tuổi");
            }

            var barber = _mapper.Map<Barber>(createDto);
            var createdBarber = await _repository.CreateAsync(barber);
            return _mapper.Map<BarberDto>(createdBarber);
        }

        public async Task<BarberDto?> UpdateBarberAsync(int id, UpdateBarberDto updateDto)
        {
            var existingBarber = await _repository.GetByIdAsync(id);
            if (existingBarber == null)
                return null;

            // Kiểm tra số điện thoại nếu được cập nhật
            if (!string.IsNullOrEmpty(updateDto.Phone) &&
                updateDto.Phone != existingBarber.Phone &&
                await _repository.PhoneExistsAsync(updateDto.Phone, id))
            {
                throw new InvalidOperationException("Số điện thoại đã được sử dụng");
            }

            // Kiểm tra tuổi nếu ngày sinh được cập nhật
            if (updateDto.DateOfBirth.HasValue)
            {
                var age = DateTime.Today.Year - updateDto.DateOfBirth.Value.Year;
                if (updateDto.DateOfBirth > DateOnly.FromDateTime(DateTime.Today.AddYears(-age))) age--;

                if (age < 18)
                {
                    throw new InvalidOperationException("Thợ cắt tóc phải đủ 18 tuổi");
                }
            }

            _mapper.Map(updateDto, existingBarber);
            var updatedBarber = await _repository.UpdateAsync(id, existingBarber);
            return updatedBarber == null ? null : _mapper.Map<BarberDto>(updatedBarber);
        }

        public async Task<bool> DeleteBarberAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}