using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeWeb.Models;

namespace DenRozeWeb.Services
{
    public class BillItemService
    {
        public void DeleteBillItemByOrderId(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public async Task DeleteBillItemByOrderIdAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public void DeleteBillItemByBillId(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public async Task DeleteBillItemByBillIdAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public void DeleteBillItem(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.BillItem WHERE biid = @ID");
        }
        public async Task DeleteBillItemAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.BillItem WHERE biid = @ID");
        }
        public void UpdateBillItem(int id, int count, int iid, int? bid, int? oid)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", (object)bid ?? DBNull.Value)
                .AddParamaeter("@Oid", (object)oid ?? DBNull.Value)
                .ExecuteNonQuery("Update DenRoze.BillItem SET biid=@ID, count=@Count, iid=@Iid, bid=@Bid, bid=@Bid, oid=@Oid WHERE iid=@ID");
        }
        public async Task UpdateBillItemAsync(int id, int count, int iid, int? bid, int? oid)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", (object)bid ?? DBNull.Value)
                .AddParamaeter("@Oid", (object)oid ?? DBNull.Value)
                .ExecuteNonQueryAsync("Update DenRoze.BillItem SET biid=@ID, count=@Count, iid=@Iid, bid=@Bid, oid=@Oid WHERE iid=@ID");
        }
        public ObservableCollection<BillItemModel> GetBillItemByBill(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<BillItemModel>("SELECT * FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public async Task<ObservableCollection<BillItemModel>> GetBillItemByBillAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<BillItemModel>("SELECT * FROM DenRoze.BillItem WHERE bid = @ID");
        }
        public ObservableCollection<BillItemModel> GetBillItemByOrder(int id)
        {
            return new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQuery<BillItemModel>("SELECT * FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public async Task<ObservableCollection<BillItemModel>> GetBillItemByOrderAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteQueryAsync<BillItemModel>("SELECT * FROM DenRoze.BillItem WHERE oid = @ID");
        }
        public ObservableCollection<BillItemModel> GetAllBillItems()
        {
            return new Database().ExecuteQuery<BillItemModel>("SELECT * FROM DenRoze.BillItem");
        }

        public async Task<ObservableCollection<BillItemModel>> GetAllBillItemsAsync()
        {
            return await new Database().ExecuteQueryAsync<BillItemModel>("SELECT * FROM DenRoze.BillItem");
        }

        public decimal InsertBillItem(int count, int iid, int? bid, int? oid)
        {
            return new Database()
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", (object)bid ?? DBNull.Value)
                .AddParamaeter("@Oid", (object)oid ?? DBNull.Value)
                .ExecuteScalar<decimal>(@"INSERT INTO DenRoze.BillItem VALUES (@Count, @Iid, @bID, @Oid); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertBillItemAsync(int count, int iid, int? bid, int? oid)
        {
            return await new Database()
                .AddParamaeter("@Count", count)
                .AddParamaeter("@Iid", iid)
                .AddParamaeter("@Bid", (object)bid ?? DBNull.Value)
                .AddParamaeter("@Oid", (object)oid ?? DBNull.Value)
                .ExecuteScalarAsync<decimal>(@"INSERT INTO DenRoze.BillItem VALUES (@Count, @Iid, @bID, @Oid); SELECT SCOPE_IDENTITY()");
        }
    }
}
