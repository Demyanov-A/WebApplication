using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication.DAL.Context;
using WebApplication.Data;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class DbInitializer : IDbInitializer
    {
        private readonly WebApplicationDB _db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(WebApplicationDB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }
        public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
        {
            _Logger.LogInformation("Инициализация БД....");

            if (RemoveBefore)
                await RemoveAsync(Cancel).ConfigureAwait(false);

            var pending_migrations = await _db.Database.GetPendingMigrationsAsync(Cancel);

            if (pending_migrations.Any())
            {
                _Logger.LogInformation("Выполнение миграции БД....");

                await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);

                _Logger.LogInformation("Выполнение миграции БД выполнено успешно!");
            }

            await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);

            await InitializeProductAsync(Cancel).ConfigureAwait(false);

            await InitializeEmployeesAsync(Cancel).ConfigureAwait(false);

            _Logger.LogInformation("Инициализация БД выполнена успешно!");
        }

        public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
        {
            _Logger.LogInformation("Удаление БД.....");

            var result = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);

            if(result)
                _Logger.LogInformation("БД удалена!");
            else
                _Logger.LogInformation("БД не удалена!");
            
            return result;
        }

        private async Task InitializeProductAsync(CancellationToken Cancel)
        {

            if (_db.Sections.Any())
            {
                _Logger.LogInformation("Инициализация тестовых данных БД не требуется");

                return;
            }

            _Logger.LogInformation("Инициализация тестовых данных БД");

            _Logger.LogInformation("Добавление секций в БД");

            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Sections] ON", Cancel);

                await _db.SaveChangesAsync(Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Sections] OFF", Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _Logger.LogInformation("Добавление брендов в БД");

            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Brands] ON", Cancel);

                await _db.SaveChangesAsync(Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Brands] OFF", Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _Logger.LogInformation("Добавление товаров в БД");

            await using (await _db.Database.BeginTransactionAsync(Cancel))
            {
                await _db.Products.AddRangeAsync(TestData.Products, Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Products] ON", Cancel);

                await _db.SaveChangesAsync(Cancel);

                await _db.Database.ExecuteSqlRawAsync("Set IDENTITY_INSERT [dbo].[Products] OFF", Cancel);

                await _db.Database.CommitTransactionAsync(Cancel);
            }

            _Logger.LogInformation("Инициализация тестовых данных БД выполнена успешно!");
        }

        private async Task InitializeEmployeesAsync(CancellationToken Cancel)
        {
            if (await _db.Employees.AnyAsync(Cancel))
            {
                _Logger.LogInformation("Инициализация сотрудников не требуется");
                return;
            }

            _Logger.LogInformation("Инициализация сотрудников...");
            await using var transaction = await _db.Database.BeginTransactionAsync(Cancel);

            await _db.Employees.AddRangeAsync(TestData.Employees, Cancel);
            await _db.SaveChangesAsync(Cancel);

            await transaction.CommitAsync(Cancel);
            _Logger.LogInformation("Инициализация сотрудников выполнена успешно");
        }
    }
}
