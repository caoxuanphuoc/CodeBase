using CodeBase.Service.Handlers.V1.Example;
using CodeBase.Service.Handlers.V1.Example.Dto;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CodeBase.Test.Services
{
    public class ExampleSerivceTest
    {
        private readonly Mock<IExampleAppService> _mockExampleAppService;

        public ExampleSerivceTest()
        {
            _mockExampleAppService = new Mock<IExampleAppService>();
        }

        [Fact]
        public async Task CreateExample_ShouldReturnCreatedExample()
        {
            // Arrange
            var createDto = new CreateExampleDto { Name = "Test", Description = "Test Description" };
            var expectedDto = new ExampleDto { Id = 1, Name = "Test", Description = "Test Description" };

            _mockExampleAppService
                .Setup(service => service.CreateExample(createDto))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _mockExampleAppService.Object.CreateExample(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
            Assert.Equal(expectedDto.Description, result.Description);
        }

        [Fact]
        public async Task GetById_ShouldReturnExampleById()
        {
            // Arrange
            var id = 1;
            var expectedDto = new ExampleDto { Id = id, Name = "Test", Description = "Test Description" };

            _mockExampleAppService
                .Setup(service => service.GetById(id))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _mockExampleAppService.Object.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
            Assert.Equal(expectedDto.Description, result.Description);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfExamples()
        {
            // Arrange
            var expectedList = new List<ExampleDto>
            {
                new ExampleDto { Id = 1, Name = "Test1", Description = "Description1" },
                new ExampleDto { Id = 2, Name = "Test2", Description = "Description2" }
            };

            _mockExampleAppService
                .Setup(service => service.GetAll())
                .ReturnsAsync(expectedList);

            // Act
            var result = await _mockExampleAppService.Object.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedList.Count, result.Count);
            Assert.Equal(expectedList[0].Id, result[0].Id);
            Assert.Equal(expectedList[1].Id, result[1].Id);
        }

        [Fact]
        public async Task Update_ShouldReturnUpdatedExample()
        {
            // Arrange
            var updateDto = new UpdateExampleDto { Id = 1, Name = "Updated", Description = "Updated Description" };
            var expectedDto = new ExampleDto { Id = 1, Name = "Updated", Description = "Updated Description" };

            _mockExampleAppService
                .Setup(service => service.Update(updateDto))
                .ReturnsAsync(expectedDto);

            // Act
            var result = await _mockExampleAppService.Object.Update(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDto.Id, result.Id);
            Assert.Equal(expectedDto.Name, result.Name);
            Assert.Equal(expectedDto.Description, result.Description);
        }
    }
}
