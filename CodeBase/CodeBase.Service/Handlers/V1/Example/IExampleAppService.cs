using CodeBase.Core.Entities;
using CodeBase.Service.Handlers.V1.Example.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBase.Service.Handlers.V1.Example
{
    public interface IExampleAppService
    {
        Task<ExampleDto> CreateExample(CreateExampleDto exampleDto);
        Task<ExampleDto> GetById(int id);

        Task<List<ExampleDto>> GetAll();

        Task<ExampleDto> Update( UpdateExampleDto input);

    }
}
