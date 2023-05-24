using SearchProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchProject.DAL
{
    public interface IRectangleRepository
    {
        Task<List<Rectangle>> GetRectangles();
        Task SeedRectangles(List<Rectangle> rectangles);
    }
}
