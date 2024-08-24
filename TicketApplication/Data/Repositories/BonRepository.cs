﻿using Microsoft.Data.SqlClient;
using TicketApplication.Data.Entities;
using Dapper;

namespace TicketApplication.Data.Repositories
{
    public class BonRepository : IBonRepository
    {
        private readonly string _connectionString;

        public BonRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<Bon> AddBon(Bon bon)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            INSERT INTO Bon (IdGhiseu, Stare, CreatedAt, ModifiedAt)
            VALUES (@IdGhiseu, @Stare, @CreatedAt, @ModifiedAt);
            SELECT CAST(SCOPE_IDENTITY() as int)";

                await con.ExecuteAsync(sql, param: bon);
                return bon;
            }
        }

        public async Task MarkAsInProgress(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql,param: new { Stare = StareEnum.InCursDePreluare, ModifiedAt = DateTime.Now, Id = id });
            }
        }

        public async Task MarkAsReceived(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql,param: new { Stare = StareEnum.Preluat, ModifiedAt = DateTime.Now, Id = id });
            }
        }

        public async Task MarkAsClose(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql, param:new { Stare = StareEnum.Inchis, ModifiedAt = DateTime.Now, Id = id });
            }
        }
    }
}
