using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using desktop_AmzOpsApi.Models;
using Microsoft.Data.SqlClient;

namespace desktop_AmzOpsApi.DAL
{
    public class AmazonTeamMemberRepo
    {
        private readonly string _connectionString;

        public AmazonTeamMemberRepo(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<SqlConnection> GetOpenConnectionAsync()
        {
            var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            return conn;
        }

        public List<AmazonTeamMember> GetAllTeamMembers()
        {
            var amazonTeamMembers = new List<AmazonTeamMember>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"SELECT e.[empl_id] [AdpEmployeeId]
                                          ,IIF(e.[preferred_name] = '', e.[first_name],e.[preferred_name]) [FirstName]
                                          ,e.[last_name] [LastName]
	                                      ,IIF(e.[rehire_date] = '1901-01-01',e.[hire_date],e.[rehire_date]) [HireDate]
                                          ,e.[job] [Job]
                                          ,e.[dept] [Department]
                                          ,e.[status_descr] [AdpStatus]
                                          ,IIF(e.[term_date] = '1901-01-01', NULL, e.[term_date]) [TermDate]
	                                      ,cm.[received_dt] [BackgroundCheckDate]
	                                      ,cm.[number] [BackgroundCheckReferenceId]
	                                      ,au.[CreatedDate] [AvettaCreateDate]
	                                      ,au.[LoginId] [AvettaLogin]
	                                      ,au.[Flag] [AvettaFlagStatus]
                                      FROM [ADP]..[employee] e
                                      JOIN [ADP]..[CERT_Mast] cm ON cm.empl_id = e.empl_id AND cm.[type] = 'AMZ'
                                      LEFT JOIN [ADP]..[Avetta_Users] au ON au.AdpEmployeeId = e.empl_id", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                amazonTeamMembers.Add(new AmazonTeamMember
                {
                    AdpEmployeeId = reader["AdpEmployeeId"]?.ToString() ?? string.Empty,
                    FirstName = reader["FirstName"]?.ToString() ?? string.Empty,
                    LastName = reader["LastName"]?.ToString() ?? string.Empty,
                    HireDate = reader["HireDate"] != DBNull.Value && Convert.ToDateTime(reader["HireDate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["HireDate"]))
                        : null,
                    Job = reader["Job"]?.ToString() ?? string.Empty,
                    Department = reader["Department"]?.ToString() ?? string.Empty,
                    AdpStatus = reader["AdpStatus"]?.ToString() ?? string.Empty,
                    TermDate = reader["TermDate"] != DBNull.Value && Convert.ToDateTime(reader["TermDate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["TermDate"]))
                        : null,
                    BackgroundCheckDate = reader["BackgroundCheckDate"] != DBNull.Value && Convert.ToDateTime(reader["BackgroundCheckDate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["BackgroundCheckDate"]))
                        : null,
                    BackgroundCheckReferenceId = reader["BackgroundCheckReferenceId"]?.ToString() ?? string.Empty,
                    AvettaCreateDate = reader["AvettaCreateDate"] != DBNull.Value && Convert.ToDateTime(reader["AvettaCreateDate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["AvettaCreateDate"]))
                        : null,
                    AvettaLogin = reader["AvettaLogin"] != DBNull.Value ? (string?)reader["AvettaLogin"].ToString() : null,
                    AvettaFlagStatus = reader["AvettaFlagStatus"] != DBNull.Value ? (string?)reader["AvettaFlagStatus"].ToString() : null,
                });
            }

            return amazonTeamMembers;
        }

        public void InsertTeamMember(AmazonTeamMember teamMember)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"INSERT INTO [ADP]..[employee] (empl_id, first_name, last_name, hire_date, job, dept, status_descr) VALUES (@empl_id, @first_name, @last_name, @hire_date, @job, @dept, @status_descr)", conn);
            cmd.Parameters.AddWithValue("@empl_id", teamMember.AdpEmployeeId);
            cmd.Parameters.AddWithValue("@first_name", teamMember.FirstName);
            cmd.Parameters.AddWithValue("@last_name", teamMember.LastName);
            cmd.Parameters.AddWithValue("@hire_date", (object?)teamMember.HireDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@job", teamMember.Job);
            cmd.Parameters.AddWithValue("@dept", teamMember.Department);
            cmd.Parameters.AddWithValue("@status_descr", teamMember.AdpStatus);
            cmd.ExecuteNonQuery();
        }

        public void UpdateTeamMember(AmazonTeamMember teamMember)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"UPDATE [ADP]..[employee] SET first_name=@first_name, last_name=@last_name, hire_date=@hire_date, job=@job, dept=@dept, status_descr=@status_descr WHERE empl_id=@empl_id", conn);
            cmd.Parameters.AddWithValue("@empl_id", teamMember.AdpEmployeeId);
            cmd.Parameters.AddWithValue("@first_name", teamMember.FirstName);
            cmd.Parameters.AddWithValue("@last_name", teamMember.LastName);
            cmd.Parameters.AddWithValue("@hire_date", (object?)teamMember.HireDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@job", teamMember.Job);
            cmd.Parameters.AddWithValue("@dept", teamMember.Department);
            cmd.Parameters.AddWithValue("@status_descr", teamMember.AdpStatus);
            cmd.ExecuteNonQuery();
        }

        public void DeleteTeamMember(string adpEmployeeId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"DELETE FROM [ADP]..[employee] WHERE empl_id=@empl_id", conn);
            cmd.Parameters.AddWithValue("@empl_id", adpEmployeeId);
            cmd.ExecuteNonQuery();
        }

        internal async Task UpdateTeamMemberAsync(AmazonTeamMember teamMember, SqlConnection conn, SqlTransaction tx)
        {
            throw new NotImplementedException();
        }
    }
}
