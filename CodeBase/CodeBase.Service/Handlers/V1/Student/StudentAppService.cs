using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CodeBase.Core.Entities;
using CodeBase.EntityFrameworkCore.Repositories;
using CodeBase.EntityFrameworkCore.Repositories.UnitOfWork;
using CodeBase.Service.Handlers.V1.Student.Dto;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace CodeBase.Service.Handlers.V1.Student
{
    public class StudentAppService : IStudentAppService
    {
        private readonly IBaseRepository<Core.Entities.Student, int> _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CacheKeyPrefix = "Student_";

        public StudentAppService(
            IBaseRepository<Core.Entities.Student, int> studentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMemoryCache cache)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<StudentDto> CreateStudent(CreateStudentDto input)
        {
            var student = _mapper.Map<Core.Entities.Student>(input);
            await _studentRepository.AddAsync(student);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> GetStudentById(int id)
        {
            var cacheKey = $"{CacheKeyPrefix}{id}";
            if (_cache.TryGetValue(cacheKey, out StudentDto cachedStudent))
            {
                return cachedStudent;
            }

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new Exception($"Không tìm thấy sinh viên với ID: {id}");
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            _cache.Set(cacheKey, studentDto, TimeSpan.FromMinutes(30));
            return studentDto;
        }

        public async Task<List<StudentDto>> GetAllStudents()
        {
            var cacheKey = $"{CacheKeyPrefix}All";
            if (_cache.TryGetValue(cacheKey, out List<StudentDto> cachedStudents))
            {
                return cachedStudents;
            }

            var students = await _studentRepository.GetAllAsync().ToListAsync();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);
            _cache.Set(cacheKey, studentDtos, TimeSpan.FromMinutes(30));
            return studentDtos;
        }

        public async Task<StudentDto> UpdateStudent(int id, UpdateStudentDto input)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new Exception($"Không tìm thấy sinh viên với ID: {id}");
            }

            _mapper.Map(input, student);
            student.UpdatedAt = DateTime.UtcNow;
            _studentRepository.UpdateAsync(student);
            await _unitOfWork.SaveChangesAsync();

            _cache.Remove($"{CacheKeyPrefix}{id}");
            _cache.Remove($"{CacheKeyPrefix}All");

            return _mapper.Map<StudentDto>(student);
        }

        public async Task DeleteStudent(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                throw new Exception($"Không tìm thấy sinh viên với ID: {id}");
            }

            _studentRepository.DeleteAsync(student);
            await _unitOfWork.SaveChangesAsync();

            _cache.Remove($"{CacheKeyPrefix}{id}");
            _cache.Remove($"{CacheKeyPrefix}All");
        }
    }
}