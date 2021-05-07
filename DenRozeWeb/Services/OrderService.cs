using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeWeb.Models;

namespace DenRozeWeb.Services
{
    public class OrderService
    {
        public void DeleteOrder(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.\"Order\" WHERE oid = @ID");
        }
        public async Task DeleteOrderAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.\"Order\" WHERE oid = @ID");
        }
        public void UpdateOrder(int id, string? note, DateTime created_at, DateTime? edited_at, int uid)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Note", (object)note ?? DBNull.Value)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", (object)edited_at ?? DBNull.Value)
                .AddParamaeter("@Uid", uid)
                .ExecuteNonQuery("Update DenRoze.\"Order\" SET note=@Note, created_at=@Created_at, edited_at=@Edited_at, uid=@Uid WHERE oid = @ID");
        }
        public async Task UpdateOrderAsync(int id, string? note, DateTime created_at, DateTime? edited_at, int uid)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Note", (object)note ?? DBNull.Value)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", (object)edited_at ?? DBNull.Value)
                .AddParamaeter("@Uid", uid)
                .ExecuteNonQueryAsync("Update DenRoze.\"Order\" SET note=@Note, created_at=@Created_at, edited_at=@Edited_at, uid=@Uid WHERE oid = @ID");
        }
        public ObservableCollection<OrderModel> GetOrderByDate(DateTime date)
        {
            return new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQuery<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE CONVERT(date, created_at) = @Date");
        }
        public async Task<ObservableCollection<OrderModel>> GetOrderByDateAsync(DateTime date)
        {
            return await new Database()
                .AddParamaeter("@Date", date.Date)
                .ExecuteQueryAsync<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE CONVERT(date, created_at) = @Date");
        }

        public ObservableCollection<OrderModel> GetOrderByUserId(int id)
        {
            return new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQuery<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE uid = @Id");
        }
        public async Task<ObservableCollection<OrderModel>> GetOrderByUserIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQueryAsync<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE uid = @Id");
        }

        public ObservableCollection<OrderModel> GetOrderByOrderId(int id)
        {
            return new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQuery<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE oid = @Id");
        }
        public async Task<ObservableCollection<OrderModel>> GetOrderByOrderIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQueryAsync<OrderModel>("SELECT * FROM DenRoze.\"Order\" WHERE oid = @Id");
        }

        public ObservableCollection<OrderModel> GetAllOrders()
        {
            return new Database().ExecuteQuery<OrderModel>("SELECT * FROM DenRoze.\"Order\"");
        }

        public async Task<ObservableCollection<OrderModel>> GetAllOrdersAsync()
        {
            return await new Database().ExecuteQueryAsync<OrderModel>("SELECT * FROM DenRoze.\"Order\"");
        }

        public decimal InsertOrder(string? note, DateTime created_at, DateTime? edited_at, int uid)
        {
            return new Database()
                .AddParamaeter("@Note", (object)note ?? DBNull.Value)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", (object)edited_at ?? DBNull.Value)
                .AddParamaeter("@Uid", uid)
                .ExecuteScalar<decimal>("INSERT INTO DenRoze.\"Order\" VALUES (@Note, @Created_at, @Edited_at, @Uid); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertOrderAsync(string? note, DateTime created_at, DateTime? edited_at, int uid)
        {
            return await new Database()
                .AddParamaeter("@Note", (object)note ?? DBNull.Value)
                .AddParamaeter("@Created_at", created_at)
                .AddParamaeter("@Edited_at", (object)edited_at ?? DBNull.Value)
                .AddParamaeter("@Uid", uid)
                .ExecuteScalarAsync<decimal>("INSERT INTO DenRoze.\"Order\" VALUES (@Note, @Created_at, @Edited_at, @Uid); SELECT SCOPE_IDENTITY()");
        }
    }
}
