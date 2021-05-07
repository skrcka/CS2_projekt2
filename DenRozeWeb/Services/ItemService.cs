using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeWeb.Models;

namespace DenRozeWeb.Services
{
    public class ItemService
    {
        public void DeleteItem(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.Item WHERE iid = @ID");
        }
        public async Task DeleteItemAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.Item WHERE iid = @ID");
        }
        public void UpdateItem(int id, string name, string? code, decimal price, decimal? dph, int? count, int? mincount, int? weight)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Name", name)
                .AddParamaeter("@Code", (object)code ?? DBNull.Value)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", (object)dph ?? DBNull.Value)
                .AddParamaeter("@Count", (object)count ?? DBNull.Value)
                .AddParamaeter("@Mincount", (object)mincount ?? DBNull.Value)
                .AddParamaeter("@Weight", (object)weight ?? DBNull.Value)
                .ExecuteNonQuery("Update DenRoze.Item SET name=@Name, code=@Code, price=@Price, dph=@DPH, count=@Count, mincount=@Mincount, weight=@Weight WHERE iid = @ID");
        }
        public async Task UpdateItemAsync(int id, string name, string? code, decimal price, decimal? dph, int? count, int? mincount, int? weight)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Name", name)
                .AddParamaeter("@Code", (object)code ?? DBNull.Value)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", (object)dph ?? DBNull.Value)
                .AddParamaeter("@Count", (object)count ?? DBNull.Value)
                .AddParamaeter("@Mincount", (object)mincount ?? DBNull.Value)
                .AddParamaeter("@Weight", (object)weight ?? DBNull.Value)
                .ExecuteNonQueryAsync("Update DenRoze.Item SET name=@Name, code=@Code, price=@Price, dph=@DPH, count=@Count, mincount=@Mincount, weight=@Weight WHERE iid = @ID");
        }
        public ObservableCollection<ItemModel> GetItemById(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<ItemModel>("SELECT * FROM DenRoze.Item WHERE iid = @ID");
        }
        public async Task<ObservableCollection<ItemModel>> GetItemByIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<ItemModel>("SELECT * FROM DenRoze.Item WHERE iid = @ID");
        }
        public ObservableCollection<ItemModel> GetItemByCode(string code)
        {
            return new Database()
                .AddParamaeter("@Code", code)
                .ExecuteQuery<ItemModel>("SELECT * FROM DenRoze.Item WHERE Code = @Code");
        }
        public async Task<ObservableCollection<ItemModel>> GetItemByCodeAsync(string code)
        {
            return await new Database()
                .AddParamaeter("@Code", code)
                .ExecuteQueryAsync<ItemModel>("SELECT * FROM DenRoze.Item WHERE Code = @Code");
        }

        public ObservableCollection<ItemModel> GetAllItems()
        {
            return new Database().ExecuteQuery<ItemModel>("SELECT * FROM DenRoze.Item");
        }

        public async Task<ObservableCollection<ItemModel>> GetAllItemsAsync()
        {
            return await new Database().ExecuteQueryAsync<ItemModel>("SELECT * FROM DenRoze.Item");
        }

        public decimal InsertItem(string name, string? code, decimal price, decimal? dph, int? count, int? mincount, int? weight)
        {
            return new Database().AddParamaeter("@Name", name)
                .AddParamaeter("@Code", (object)code ?? DBNull.Value)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", (object)dph ?? DBNull.Value)
                .AddParamaeter("@Count", (object)count ?? DBNull.Value)
                .AddParamaeter("@Mincount", (object)mincount ?? DBNull.Value)
                .AddParamaeter("@Weight", (object)weight ?? DBNull.Value)
                .ExecuteScalar<decimal>(@"INSERT INTO DenRoze.Item VALUES (@Name, @Code, @Price, @DPH, @Count, @MinCount, @Weight); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertItemAsync(string name, string? code, decimal price, decimal? dph, int? count, int? mincount, int? weight)
        {
            return await new Database().AddParamaeter("@Name", name)
                .AddParamaeter("@Code", (object)code ?? DBNull.Value)
                .AddParamaeter("@Price", price)
                .AddParamaeter("@DPH", (object)dph ?? DBNull.Value)
                .AddParamaeter("@Count", (object)count ?? DBNull.Value)
                .AddParamaeter("@Mincount", (object)mincount ?? DBNull.Value)
                .AddParamaeter("@Weight", (object)weight ?? DBNull.Value)
                .ExecuteScalarAsync<decimal>(@"INSERT INTO DenRoze.Item VALUES (@Name, @Code, @Price, @DPH, @Count, @MinCount, @Weight); SELECT SCOPE_IDENTITY()");
        }
    }
}
