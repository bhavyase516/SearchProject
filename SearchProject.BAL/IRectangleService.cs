using SearchProject.DAL.Models;

namespace SearchProject.BAL
{
    public interface IRectangleService
    {
        Task<List<Rectangle>> SearchRectangles(List<Coordinate> coordinates);
    }
}
