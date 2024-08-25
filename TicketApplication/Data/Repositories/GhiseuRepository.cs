using Dapper;
using Microsoft.Data.SqlClient;
using TicketApplication.Data.Entities;


namespace TicketApplication.Data.Repositories
{
    public class GhiseuRepository : IGhiseuRepository
    {
        private readonly string _connectionString;

        public GhiseuRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }

        public async Task<Ghiseu?> GetGhiseuById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM bon.Ghiseu WHERE Id = @Id";
                return await con.QuerySingleOrDefaultAsync<Ghiseu>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Ghiseu>> GetAllGhiseu()
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM bon.Ghiseu";
                return await con.QueryAsync<Ghiseu>(sql);
            }
        }
        public async Task<Ghiseu> AddGhiseu(Ghiseu ghiseu)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            INSERT INTO bon.Ghiseu (Cod, Denumire, Descriere, Icon, Activ)
            VALUES (@Cod, @Denumire, @Descriere, @Icon, @Activ);
            SELECT CAST(SCOPE_IDENTITY() as int)";

                await con.ExecuteAsync(sql, param: ghiseu);
                return ghiseu;
            }
        }

        public async Task EditGhiseu(Ghiseu ghiseu)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = @"
            UPDATE bon.Ghiseu SET 
                Cod = @Cod, 
                Denumire = @Denumire, 
                Descriere = @Descriere, 
                Icon = @Icon, 
                Activ = @Activ 
            WHERE Id = @Id";

                await con.ExecuteAsync(
                    sql,
                    param: new { ghiseu.Cod, ghiseu.Denumire, ghiseu.Descriere, ghiseu.Icon, ghiseu.Activ, ghiseu.Id }
                    );
            }
        }

        public async Task MarkAsActive(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE bon.Ghiseu SET Activ = 1 WHERE Id = @Id";
                await con.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task MarkAsInactive(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE bon.Ghiseu SET Activ = 0 WHERE Id = @Id";
                await con.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task DeleteGhiseu(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM bon.Ghiseu WHERE Id = @Id";
                await con.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}
