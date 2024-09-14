using Dapper;
using Microsoft.Data.SqlClient;
using TicketApplication.Common;
using TicketApplication.Data.Entities;

namespace TicketApplication.Data.Repositories
{
    public class BonRepository : IBonRepository
    {
        private readonly string _connectionString;

        public BonRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<Bon?> GetBonById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM bon.Bon WHERE Id = @Id";
                return await con.QuerySingleOrDefaultAsync<Bon>(sql, new { Id = id });
            }
        }
        public async Task<IEnumerable<Bon>> GetAllBonByGhiseuId(int ghiseuId)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT * FROM bon.Bon
            WHERE IdGhiseu = @GhiseuId
            ORDER BY Stare ASC, CreatedAt ASC";
                return await con.QueryAsync<Bon>(sql, param: new { GhiseuId = ghiseuId });
            }
        }

        public async Task<IEnumerable<Bon>> GetAllBon()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT * FROM bon.Bon
            ORDER BY Stare ASC, CreatedAt ASC";
                return await con.QueryAsync<Bon>(sql);
            }
        }

        public async Task<Bon> AddBon(Bon bon)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            INSERT INTO bon.Bon (IdGhiseu, Stare, CreatedAt, ModifiedAt)
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
            UPDATE bon.Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql, param: new { Stare = StareEnum.InCursDePreluare, ModifiedAt = DateTime.Now, Id = id });
            }
        }

        public async Task MarkAsReceived(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE bon.Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql, param: new { Stare = StareEnum.Preluat, ModifiedAt = DateTime.Now, Id = id });
            }
        }

        public async Task MarkAsClose(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE bon.Bon SET 
                Stare = @Stare,
                ModifiedAt = @ModifiedAt 
            WHERE Id = @Id";

                await con.ExecuteAsync(sql, param: new { Stare = StareEnum.Inchis, ModifiedAt = DateTime.Now, Id = id });
            }
        }
    }
}
