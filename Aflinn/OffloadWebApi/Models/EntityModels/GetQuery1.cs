using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using OffloadWebApi.Repository;

namespace OffloadWebApi.Models.EntityModels
{
    public class GetQuery1
    {
        public GetQuery1(string textFirst) 
        {
            this.textFirst = textFirst;
        }

        public string textFirst { get; set; }

        internal AppDb Db { get; set; }

        public GetQuery1()
        {
        }

        internal GetQuery1(AppDb db)
        {
            this.Db = db;
        }

        public async Task<List<GetQuery1>> GetAsync()
        {
            using var cmd = this.Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT Art FROM fangstdata_2019 LIMIT 1;";
            return await this.ReadAllAsync(await cmd.ExecuteReaderAsync()); // listi (1 stak)
        }

        private async Task<List<GetQuery1>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<GetQuery1>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new GetQuery1(this.Db)
                    {
                        textFirst = reader.GetString(0),
                    };
                    posts.Add(post);
                }
            }

            return posts;
        }

        internal Task LatestGetAsync() 
        {
            throw new NotImplementedException();
        }
    }
}