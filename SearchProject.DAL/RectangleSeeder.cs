using SearchProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchProject.DAL
{
    public class RectangleSeeder
    {
        private readonly IRectangleRepository _rectangleRepository;

        public RectangleSeeder(IRectangleRepository rectangleRepository)
        {
            _rectangleRepository = rectangleRepository;
        }
        public async Task SeedAsync(int count)
        {
            var rectangles = GenerateRandomRectangles(count);
            _rectangleRepository.SeedRectangles(rectangles);
        }

        private List<Rectangle> GenerateRandomRectangles(int count)
        {
            var rectangles = new List<Rectangle>();

            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(0, 100);
                int y = random.Next(0, 100);
                int width = random.Next(10, 50);
                int height = random.Next(10, 50);

                rectangles.Add(new Rectangle { X = x, Y = y, Width = width, Height = height });
            }

            return rectangles;
        }
    }

}
