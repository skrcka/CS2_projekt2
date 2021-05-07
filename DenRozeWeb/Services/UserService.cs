using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DenRozeWeb.Models;

namespace DenRozeWeb.Services
{
    public class UserService
    {
        public void DeleteUser(int id)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQuery("DELETE FROM DenRoze.\"User\" WHERE uid = @ID");
        }
        public async Task DeleteUserAsync(int id)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .ExecuteNonQueryAsync("DELETE FROM DenRoze.\"User\" WHERE uid = @ID");
        }
        public void UpdateUser(int id, string password, string full_name, string phone, string email, string address)
        {
            new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@FullName", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteNonQuery("Update DenRoze.\"User\" SET password=@Password, full_name=@FullName, phone=@Phone, email=@Email, address=@Address WHERE uid = @ID");
        }
        public async Task UpdateUserAsync(int id, string full_name, string password, string phone, string email, string address)
        {
            await new Database()
                .AddParamaeter("@ID", id)
                .AddParamaeter("@FullName", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteNonQueryAsync("Update DenRoze.\"User\" SET password=@Password, full_name=@FullName, phone=@Phone, email=@Email, address=@Address WHERE uid = @ID");
        }
        public ObservableCollection<UserModel> GetUserById(int id)
        {
            return new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQuery<UserModel>("SELECT * FROM DenRoze.\"User\" WHERE uid = @Id");
        }
        public async Task<ObservableCollection<UserModel>> GetUserByIdAsync(int id)
        {
            return await new Database()
                .AddParamaeter("@Id", id)
                .ExecuteQueryAsync<UserModel>("SELECT * FROM DenRoze.\"User\" WHERE uid = @Id");
        }
        public ObservableCollection<UserModel> GetUserByEmail(string email)
        {
            return new Database()
                .AddParamaeter("@Email", email)
                .ExecuteQuery<UserModel>("SELECT * FROM DenRoze.\"User\" WHERE email = @Email");
        }
        public async Task<ObservableCollection<UserModel>> GetUserByEmailAsync(string email)
        {
            return await new Database()
                .AddParamaeter("@Email", email)
                .ExecuteQueryAsync<UserModel>("SELECT * FROM DenRoze.\"User\" WHERE email = @Email");
        }

        public ObservableCollection<UserModel> GetAllUsers()
        {
            return new Database().ExecuteQuery<UserModel>("SELECT * FROM DenRoze.\"User\"");
        }

        public async Task<ObservableCollection<UserModel>> GetAllUsersAsync()
        {
            return await new Database().ExecuteQueryAsync<UserModel>("SELECT * FROM DenRoze.\"User\"");
        }

        public decimal InsertUser(string login, string full_name, string password, string phone, string email, string address)
        {
            return new Database().AddParamaeter("@Login", login)
                .AddParamaeter("@Full_name", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteScalar<decimal>("INSERT INTO DenRoze.\"User\" VALUES (@Login, @Full_name, @Password, @Phone, @Email, @Address); SELECT SCOPE_IDENTITY()");
        }

        public async Task<decimal> InsertUserAsync(string login, string full_name, string password, string phone, string email, string address)
        {
            return await new Database().AddParamaeter("@Login", login)
                .AddParamaeter("@Full_name", full_name)
                .AddParamaeter("@Password", password)
                .AddParamaeter("@Phone", phone)
                .AddParamaeter("@Email", email)
                .AddParamaeter("@Address", address)
                .ExecuteScalarAsync<decimal>("INSERT INTO DenRoze.\"User\" VALUES (@Login, @Full_name, @Password, @Phone, @Email, @Address); SELECT SCOPE_IDENTITY()");
        }
    }
}
