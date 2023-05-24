using Dapper;
using SearchProject.DAL.Models;
using System.Data;

namespace SearchProject.DAL
{
    public class RectangleRepository : IRectangleRepository
    {
        private readonly IDbConnection _dbConnection;

        public RectangleRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<List<Rectangle>> GetRectangles()
        {
            await EnsureTablesCreatedAsync();
            return _dbConnection.Query<Rectangle>("SELECT * FROM Rectangles").ToList();
        }
        private async Task EnsureTablesCreatedAsync()
        {
            // Check if the tables exist
            bool rectanglesTableExists = _dbConnection.ExecuteScalarAsync<bool>("SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Rectangles'").Result;

            if (!rectanglesTableExists)
            {
                // Create the Rectangles table
                _dbConnection.Execute(@"
                    CREATE TABLE Rectangles (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Name VARCHAR(50) NOT NULL,
                        X INT NOT NULL,
                        Y INT NOT NULL,
                        Width INT NOT NULL,
                        Height INT NOT NULL
                    )
                ");
            }
        }

        public async Task SeedRectangles(List<Rectangle> rectangles)
        {
            await EnsureTablesCreatedAsync();
            string query = @"INSERT INTO Rectangles (X, Y, Width, Height) VALUES (@X, @Y, @Width, @Height)";

            _dbConnection.Execute(query, rectangles);
        }
    }

}