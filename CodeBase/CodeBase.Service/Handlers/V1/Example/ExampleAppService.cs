using AutoMapper;
using CodeBase.Core.Entities;
using CodeBase.EntityFrameworkCore.Repositories;
using CodeBase.EntityFrameworkCore.Repositories.UnitOfWork;
using CodeBase.Service.Handlers.V1.Example.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Handlers.V1.Example
{
    public class ExampleAppService : IExampleAppService
    {
        private readonly IBaseRepository<ExampleEntity, int> _exampleRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ExampleAppService(IBaseRepository<ExampleEntity, int> exampleRepository, IMapper mapper, IUnitOfWork unitOfWork )
        {
            _exampleRepository = exampleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ExampleDto> CreateExample(CreateExampleDto exampleDto)
        {

            var exampleEntity = _mapper.Map<ExampleEntity>(exampleDto);

            await _exampleRepository.AddAsync(exampleEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ExampleDto>(exampleEntity);

        }

        public async Task<List<ExampleDto>> GetAll()
        {
            var examples = await _exampleRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<List<ExampleDto>>(examples);
        }

        public async Task<ExampleDto> GetById(int id)
        {
            var example = await _exampleRepository.GetByIdAsync(id);
            return _mapper.Map<ExampleDto>(example);
        }

        public async Task<ExampleDto> Update(UpdateExampleDto input)
        {
            var exampleEntity = await _exampleRepository.GetByIdAsync(input.Id);
            if (exampleEntity == null)
            {
                // Handle entity not found, e.g., throw an exception or return null
                return null;
            }

            _mapper.Map(input, exampleEntity);
            _exampleRepository.UpdateAsync(exampleEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ExampleDto>(exampleEntity);
        }
    }
}
