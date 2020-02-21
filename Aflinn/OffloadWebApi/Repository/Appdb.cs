using System;
using MySql.Data.MySqlClient;
using OffloadWebApi.Models.EntityModels;

namespace OffloadWebApi.Repository
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            this.Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => this.Connection.Dispose();
    }
}