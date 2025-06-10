using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using desktop_AmzOpsApi.Models;
using Microsoft.Data.SqlClient;

namespace desktop_AmzOpsApi.DAL
{
    public class AmazonSiteRepo
    {
        private readonly string _connectionString;

        public AmazonSiteRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<SqlConnection> GetOpenConnectionAsync()
        {
            var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }

        public List<AmazonSite> GetAllSites()
        {
            var amazonSites = new List<AmazonSite>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"SELECT [Id]
                                        ,[SiteCode]
                                        ,[SiteType]
                                        ,[Size]
                                        ,[Population]
                                        ,[Notes]
                                        ,[Status]
                                        ,[Address1]
                                        ,[Address2]
                                        ,[City]
                                        ,[Region]
                                        ,[PostalCode]
                                        ,[Country] 
                                      FROM [AVIData].[dbo].[Amazon_Site]", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                amazonSites.Add(new AmazonSite
                {
                    Id = reader["id"] != DBNull.Value ? Convert.ToInt32(reader["id"]) : 0,
                    SiteCode = reader["SiteCode"] != DBNull.Value ? (string?)reader["SiteCode"].ToString() : string.Empty,
                    SiteType = reader["SiteType"] != DBNull.Value ? (string?)reader["SiteType"].ToString() : null, 
                    Status = reader["status"] != DBNull.Value ? (string?)reader["status"].ToString() : null, 
                    Address1 = reader["Address1"] != DBNull.Value ? (string?)reader["Address1"].ToString() : null,
                    Address2 = reader["Address2"] != DBNull.Value ? (string?)reader["Address2"].ToString() : null, 
                    City = reader["City"] != DBNull.Value ? (string?)reader["City"].ToString() : null, 
                    Region = reader["Region"] != DBNull.Value ? (string?)reader["Region"].ToString() : null,
                    PostalCode = reader["PostalCode"] != DBNull.Value ? (string?)reader["PostalCode"].ToString() : null,
                    Country = reader["Country"] != DBNull.Value ? (string?)reader["Country"].ToString() : null
                });
            }

            return amazonSites;
        }

        public void InsertSite(AmazonSite site)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var insertCmd = new SqlCommand(@"INSERT INTO AVIData..Amazon_Site (
                                                [SiteCode]
                                                ,[SiteType]
                                                ,[Size]
                                                ,[Population]
                                                ,[Notes]
                                                ,[Status]
                                                ,[Address1]
                                                ,[Address2]
                                                ,[City]
                                                ,[Region]
                                                ,[PostalCode]
                                                ,[Country]
                                                )
                                        VALUES (@SiteCode
                                                ,@SiteType
                                                ,@Size
                                                ,@Population
                                                ,@Notes
                                                ,@Status
                                                ,@Address1
                                                ,@Address2
                                                ,@City
                                                ,@Region
                                                ,@PostalCode
                                                ,@Country); SELECT SCOPE_IDENTITY();", conn);
            insertCmd.Parameters.AddWithValue("@SiteCode", site.SiteCode);
            insertCmd.Parameters.AddWithValue("@SiteType", DbUtils.ToDbValue(site.SiteType));
            insertCmd.Parameters.AddWithValue("@Size", DbUtils.ToDbValue(site.Size));
            insertCmd.Parameters.AddWithValue("@Population", DbUtils.ToDbValue(site.Population));
            insertCmd.Parameters.AddWithValue("@Notes", DbUtils.ToDbValue(site.Notes));
            insertCmd.Parameters.AddWithValue("@Status", DbUtils.ToDbValue(site.Status));
            insertCmd.Parameters.AddWithValue("@Address1", DbUtils.ToDbValue(site.Address1));
            insertCmd.Parameters.AddWithValue("@Address2", DbUtils.ToDbValue(site.Address2));
            insertCmd.Parameters.AddWithValue("@City", DbUtils.ToDbValue(site.City));
            insertCmd.Parameters.AddWithValue("@Region", DbUtils.ToDbValue(site.Region));
            insertCmd.Parameters.AddWithValue("@PostalCode", DbUtils.ToDbValue(site.PostalCode));
            insertCmd.Parameters.AddWithValue("@Country", DbUtils.ToDbValue(site.Country));
            var newId = Convert.ToInt32(insertCmd.ExecuteScalar());
            site.Id = newId;
        }

        public void UpdateSite(AmazonSite site)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var updateCmd = new SqlCommand(@"UPDATE AVIData..Amazon_Site SET
                                            [SiteCode] = @SiteCode
                                            ,[SiteType] = @SiteType
                                            ,[Size] = @Size
                                            ,[Population] = @Population
                                            ,[Notes] = @Notes
                                            ,[Status] = @Status
                                            ,[Address1] = @Address1
                                            ,[Address2] = @Address2
                                            ,[City] = @City
                                            ,[Region] = @Region
                                            ,[PostalCode] = @PostalCode
                                            ,[Country] = @Country
                                        WHERE [Id] = @Id", conn);
            updateCmd.Parameters.AddWithValue("@Id", site.Id);
            updateCmd.Parameters.AddWithValue("@SiteCode", site.SiteCode);
            updateCmd.Parameters.AddWithValue("@SiteType", DbUtils.ToDbValue(site.SiteType));
            updateCmd.Parameters.AddWithValue("@Size", DbUtils.ToDbValue(site.Size));
            updateCmd.Parameters.AddWithValue("@Population", DbUtils.ToDbValue(site.Population));
            updateCmd.Parameters.AddWithValue("@Notes", DbUtils.ToDbValue(site.Notes));
            updateCmd.Parameters.AddWithValue("@Status", DbUtils.ToDbValue(site.Status));
            updateCmd.Parameters.AddWithValue("@Address1", DbUtils.ToDbValue(site.Address1));
            updateCmd.Parameters.AddWithValue("@Address2", DbUtils.ToDbValue(site.Address2));
            updateCmd.Parameters.AddWithValue("@City", DbUtils.ToDbValue(site.City));
            updateCmd.Parameters.AddWithValue("@Region", DbUtils.ToDbValue(site.Region));
            updateCmd.Parameters.AddWithValue("@PostalCode", DbUtils.ToDbValue(site.PostalCode));
            updateCmd.Parameters.AddWithValue("@Country", DbUtils.ToDbValue(site.Country));
            updateCmd.ExecuteNonQuery();
        }

        public void DeleteSite(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var deleteCmd = new SqlCommand(@"DELETE FROM AVIData..Amazon_Site WHERE [Id] = @Id", conn);
            deleteCmd.Parameters.AddWithValue("@Id", id);
            deleteCmd.ExecuteNonQuery();
        }

        public async Task InsertSiteAsync(AmazonSite site, SqlConnection conn, SqlTransaction tx)
        {
            var insertCmd = new SqlCommand(@"INSERT INTO AVIData..Amazon_Site (
                                                [SiteCode]
                                                ,[SiteType]
                                                ,[Size]
                                                ,[Population]
                                                ,[Notes]
                                                ,[Status]
                                                ,[Address1]
                                                ,[Address2]
                                                ,[City]
                                                ,[Region]
                                                ,[PostalCode]
                                                ,[Country]
                                                )
                                    VALUES (@SiteCode
                                            ,@SiteType
                                            ,@Size
                                            ,@Population
                                            ,@Notes
                                            ,@Status
                                            ,@Address1
                                            ,@Address2
                                            ,@City
                                            ,@Region
                                            ,@PostalCode
                                            ,@Country); SELECT SCOPE_IDENTITY();", conn, tx);
            insertCmd.Parameters.AddWithValue("@SiteCode", site.SiteCode);
            insertCmd.Parameters.AddWithValue("@SiteType", DbUtils.ToDbValue(site.SiteType));
            insertCmd.Parameters.AddWithValue("@Size", DbUtils.ToDbValue(site.Size));
            insertCmd.Parameters.AddWithValue("@Population", DbUtils.ToDbValue(site.Population));
            insertCmd.Parameters.AddWithValue("@Notes", DbUtils.ToDbValue(site.Notes));
            insertCmd.Parameters.AddWithValue("@Status", DbUtils.ToDbValue(site.Status));
            insertCmd.Parameters.AddWithValue("@Address1", DbUtils.ToDbValue(site.Address1));
            insertCmd.Parameters.AddWithValue("@Address2", DbUtils.ToDbValue(site.Address2));
            insertCmd.Parameters.AddWithValue("@City", DbUtils.ToDbValue(site.City));
            insertCmd.Parameters.AddWithValue("@Region", DbUtils.ToDbValue(site.Region));
            insertCmd.Parameters.AddWithValue("@PostalCode", DbUtils.ToDbValue(site.PostalCode));
            insertCmd.Parameters.AddWithValue("@Country", DbUtils.ToDbValue(site.Country));
            var newId = Convert.ToInt32(await insertCmd.ExecuteScalarAsync());

            site.Id = newId; // Update in-memory ID
        }

        public async Task UpdateSiteAsync(AmazonSite site, SqlConnection conn, SqlTransaction tx)
        {
            var updateCmd = new SqlCommand(@"UPDATE AVIData..Amazon_Site SET
                                            [SiteCode] = @SiteCode
                                            ,[SiteType] = @SiteType
                                            ,[Size] = @Size
                                            ,[Population] = @Population
                                            ,[Notes] = @Notes
                                            ,[Status] = @Status
                                            ,[Address1] = @Address1
                                            ,[Address2] = @Address2
                                            ,[City] = @City
                                            ,[Region] = @Region
                                            ,[PostalCode] = @PostalCode
                                            ,[Country] = @Country
                                        WHERE [Id] = @Id", conn, tx);
            updateCmd.Parameters.AddWithValue("@Id", site.Id);
            updateCmd.Parameters.AddWithValue("@SiteCode", site.SiteCode);
            updateCmd.Parameters.AddWithValue("@SiteType", DbUtils.ToDbValue(site.SiteType));
            updateCmd.Parameters.AddWithValue("@Size", DbUtils.ToDbValue(site.Size));
            updateCmd.Parameters.AddWithValue("@Population", DbUtils.ToDbValue(site.Population));
            updateCmd.Parameters.AddWithValue("@Notes", DbUtils.ToDbValue(site.Notes));
            updateCmd.Parameters.AddWithValue("@Status", DbUtils.ToDbValue(site.Status));
            updateCmd.Parameters.AddWithValue("@Address1", DbUtils.ToDbValue(site.Address1));
            updateCmd.Parameters.AddWithValue("@Address2", DbUtils.ToDbValue(site.Address2));
            updateCmd.Parameters.AddWithValue("@City", DbUtils.ToDbValue(site.City));
            updateCmd.Parameters.AddWithValue("@Region", DbUtils.ToDbValue(site.Region));
            updateCmd.Parameters.AddWithValue("@PostalCode", DbUtils.ToDbValue(site.PostalCode));
            updateCmd.Parameters.AddWithValue("@Country", DbUtils.ToDbValue(site.Country));
            await updateCmd.ExecuteNonQueryAsync();
        }


        public async Task DeleteSiteAsync(int id, SqlConnection conn, SqlTransaction tx)
        {
            var deleteCmd = new SqlCommand("DELETE FROM AVIData..Amazon_Site WHERE Id = @Id", conn, tx);
            deleteCmd.Parameters.AddWithValue("@Id", id);
            await deleteCmd.ExecuteNonQueryAsync();
        }
    }
}
