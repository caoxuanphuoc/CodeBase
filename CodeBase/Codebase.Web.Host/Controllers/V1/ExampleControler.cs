using Codebase.Web.Host.Controllers.Models;
using CodeBase.Service.Handlers.V1.Example;
using CodeBase.Service.Handlers.V1.Example.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Codebase.Web.Host.Controllers.V1
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleControler : ControllerBase
    {
        private readonly IExampleAppService _exampleAppService;
        public ExampleControler(IExampleAppService exampleAppService)
        {
            _exampleAppService = exampleAppService;
        }

        [HttpPost(Name = "CreateExample")]
        public async Task<ExampleDto> CreateExample(CreateExampleModels exampleModel)
        {
            CreateExampleDto data = new CreateExampleDto
            {
                Description = exampleModel.Description,
                Name = exampleModel.Name
            };
            return await _exampleAppService.CreateExample(data);
        }
        [HttpGet(Name = "GetAllExamples")]
        public async Task<ActionResult<List<ExampleDto>>> GetAll()
        {
            var examples = await _exampleAppService.GetAll();
            return Ok(examples);
        }

        [HttpGet("{id}", Name = "GetExampleById")]
        public async Task<ActionResult<ExampleDto>> GetById(int id)
        {
            var example = await _exampleAppService.GetById(id);
            if (example == null)
            {
                return NotFound();
            }
            return Ok(example);
        }

        [HttpPut( Name = "UpdateExample")]
        public async Task<ActionResult<ExampleDto>> Update( UpdateExampleModel updateModel)
        {
            var data = new UpdateExampleDto
            {
                Id = updateModel.Id,
                Description = updateModel.Description,
                Name = updateModel.Name
            };
            var updatedExample = await _exampleAppService.Update(data);
            if (updatedExample == null)
            {
                return NotFound();
            }
            return Ok(updatedExample);
        }
    }
}
