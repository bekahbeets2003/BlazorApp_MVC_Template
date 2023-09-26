using Dapper;
using BlazorApp_MVC.Models;
using BlazorApp_MVC.Utilities;
using System.Data.SqlClient;
using System.Data;
using BlazorApp_MVC.Interfaces;

namespace BlazorApp_MVC.DataRepo
{
    public class DbDapper : IDbDapper
    {
        private ILogger<DbDapper> _logger;
        private Utility _utility;
        private string _connectionString;
        SemaphoreSlim _semaphoregate = new SemaphoreSlim(1); //.net 6 async 'lock'

        public DbDapper(ILogger<DbDapper> logger, Utility utility, string connectionString)
        {
            _logger = logger;
            _utility = utility;
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets an IEnumerable of athings from db via dapper call. Params empty. Returns list of athings
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<athing>> GetAthingsAsync()
        {
            await _semaphoregate.WaitAsync();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string sQuery = "exec usp_csv_get_files";
                var result = await conn.QueryAsync<athing>(sQuery);

                conn.Close();
            }
            _semaphoregate.Release();

            return new List<athing>();
        }

        /// <summary>
        /// Get a thing by id from db via dapper call. Param id, Returns a thing
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<athing> GetAthingAsync(int id)
        {
            await _semaphoregate.WaitAsync();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                DynamicParameters param = new DynamicParameters();
                param.Add("@p_book_id", id);

                string sql = "exec GetBookById";
                var result = await conn.QueryFirstOrDefaultAsync<athing>(sql, param, commandType: CommandType.StoredProcedure);

                _semaphoregate.Release();

                return result;

            }
        }


    }
}
