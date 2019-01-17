using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class SeedController : Controller
    {
        private DataContext _context;

        public SeedController(DataContext ctx) => _context = ctx;

        public IActionResult Index()
        {
            ViewBag.Count = _context.Products.Count();
            return View(_context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Take(20));
        }

        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                _context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
                _context.Database
                .ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                _context.Database.ExecuteSqlCommand($@"
                    CREATE PROCEDURE CreateSeedData
                        @RowCount decimal
                    AS
                        BEGIN
                        SET NOCOUNT ON
                    DECLARE @i INT = 1;
                        DECLARE @catId BIGINT;
                        DECLARE @CatCount INT = @RowCount / 10;
                        DECLARE @pprice DECIMAL(5,2);
                        DECLARE @rprice DECIMAL(5,2);
                        BEGIN TRANSACTION
                            WHILE @i <= @CatCount
                                BEGIN
                                    INSERT INTO Categories (Name, Description)
                                    VALUES (CONCAT('Category-', @i),
                                    'Test Data Category');
                                    SET @catId = SCOPE_IDENTITY();
                                    DECLARE @j INT = 1;
                                    WHILE @j <= 10
                                        BEGIN
                                            SET @pprice = RAND()*(500-5+1);
                                            SET @rprice = (RAND() * @pprice)
                                            + @pprice;
                                            INSERT INTO Products (Name, CategoryId,
                                            PurchasePrice, RetailPrice)
                                            VALUES (CONCAT('Product', @i, '-', @j),
                                            @catId, @pprice, @rprice)
                                            SET @j = @j + 1
                                            END
                            SET @i = @i + 1
                            END
                            COMMIT
                    END");
                _context.Database.BeginTransaction();
                _context.Database
                .ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
                _context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            _context.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlCommand("DELETE FROM Orders");
            _context.Database.ExecuteSqlCommand("DELETE FROM Categories");
            _context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
    }
}