using Microsoft.Extensions.Logging;
using SearchProject.DAL;
using SearchProject.DAL.Models;

namespace SearchProject.BAL
{
    public class RectangleService : IRectangleService
    {
        private readonly IRectangleRepository _rectangleRepository;
        private readonly ILogger<RectangleService> _logger;

        public RectangleService(IRectangleRepository rectangleRepository, ILogger<RectangleService> logger)
        {
            _rectangleRepository = rectangleRepository;
            _logger = logger;
        }

        public async Task<List<Rectangle>> SearchRectangles(List<Coordinate> coordinates)
        {
            var rectangles = await  _rectangleRepository.GetRectangles();
            var result = new List<Rectangle>();
            foreach (var coordinate in coordinates)
            {
                var matchingRectangles = rectangles.Where(r =>
                    coordinate.X >= r.X && coordinate.X <= r.X + r.Width &&
                    coordinate.Y >= r.Y && coordinate.Y <= r.Y + r.Height);

                result.AddRange(matchingRectangles);
            }

            return result;
        }

        public async Task SeedRectangles(List<Rectangle> rectangles)
        {
            await _rectangleRepository.SeedRectangles(rectangles);
        }
    }
}