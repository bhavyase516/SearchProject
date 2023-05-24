using Dapper;
using Microsoft.Extensions.Logging;
using Moq;
using SearchProject.BAL;
using SearchProject.DAL;
using SearchProject.DAL.Models;
using System.Data;

namespace SearchProject.Tests
{
    public class RectangleServiceTests
    {
        [Fact]
        public void SearchRectangles_ReturnsMatchingRectangles()
        {
            // Arrange
            var coordinates = new List<Coordinate>
            {
                new Coordinate { X = 1, Y = 1 },
                new Coordinate { X = 3, Y = 3 }
            };

            var rectangles = new List<Rectangle>
            {
                new Rectangle { X = 0, Y = 0, Width = 2, Height = 2 },
                new Rectangle { X = 2, Y = 2, Width = 2, Height = 2 },
                new Rectangle { X = 4, Y = 4, Width = 2, Height = 2 }
            };

            var mockRepository = new Mock<IRectangleRepository>();
            var logger = new Mock<ILogger<RectangleService>>();
            mockRepository.Setup(r => r.GetRectangles()).Returns(Task.FromResult(rectangles));

            var rectangleService = new RectangleService(mockRepository.Object, logger.Object);

            // Act
            var result = rectangleService.SearchRectangles(coordinates).Result;

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}