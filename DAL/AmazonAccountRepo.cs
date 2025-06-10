using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using desktop_AmzOpsApi.Models;

namespace desktop_AmzOpsApi.DAL
{
    public class AmazonAccountRepo
    {
        private readonly string _connectionString;

        public AmazonAccountRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetOpenConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        public List<AmazonAccount> GetAllAccounts()
        {
            var amazonAccounts = new List<AmazonAccount>();

            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var cmd = new SqlCommand(@"SELECT custno
                                            ,custname
                                            ,loc
                                            ,class
                                            ,installdate
                                            ,pulldate
                                            ,inactive
                                            ,address1
                                            ,address2
                                            ,city
                                            ,st
                                            ,zip 
                                        FROM AVIData..CustMast WHERE maj = '01868'", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                amazonAccounts.Add(new AmazonAccount
                {
                    AccountNumber = reader["custno"]?.ToString() ?? string.Empty,
                    Name = reader["custname"]?.ToString() ?? string.Empty,
                    SiteCode = reader["loc"]?.ToString() ?? string.Empty,
                    Class = reader["class"]?.ToString() ?? string.Empty,
                    InstallDate = reader["installdate"] != DBNull.Value && Convert.ToDateTime(reader["installdate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["installdate"]))
                        : null,
                    PullDate = reader["pulldate"] != DBNull.Value && Convert.ToDateTime(reader["pulldate"]) != new DateTime(1900, 1, 1)
                        ? DateOnly.FromDateTime(Convert.ToDateTime(reader["pulldate"]))
                        : null,
                    IsInactive = reader["inactive"] != DBNull.Value && Convert.ToBoolean(reader["inactive"]),
                    Address1 = reader["address1"]?.ToString() ?? string.Empty,
                    Address2 = reader["address2"]?.ToString() ?? string.Empty,
                    City = reader["city"]?.ToString() ?? string.Empty,
                    State = reader["st"]?.ToString() ?? string.Empty,
                    PostalCode = reader["zip"]?.ToString() ?? string.Empty,
                });
            }

            return amazonAccounts;
        }
        public void InsertAccount(AmazonAccount account)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"INSERT INTO AVIData..CustMast (custno, custname, loc, class, installdate, pulldate, inactive, address1, address2, city, st, zip, maj) VALUES (@custno, @custname, @loc, @class, @installdate, @pulldate, @inactive, @address1, @address2, @city, @st, @zip, '01868')", conn);
            cmd.Parameters.AddWithValue("@custno", account.AccountNumber);
            cmd.Parameters.AddWithValue("@custname", account.Name);
            cmd.Parameters.AddWithValue("@loc", account.SiteCode);
            cmd.Parameters.AddWithValue("@class", account.Class);
            cmd.Parameters.AddWithValue("@installdate", (object?)account.InstallDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pulldate", (object?)account.PullDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@inactive", account.IsInactive);
            cmd.Parameters.AddWithValue("@address1", account.Address1);
            cmd.Parameters.AddWithValue("@address2", account.Address2);
            cmd.Parameters.AddWithValue("@city", account.City);
            cmd.Parameters.AddWithValue("@st", account.State);
            cmd.Parameters.AddWithValue("@zip", account.PostalCode);
            cmd.ExecuteNonQuery();
        }

        public void UpdateAccount(AmazonAccount account)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"UPDATE AVIData..CustMast SET custname=@custname, loc=@loc, class=@class, installdate=@installdate, pulldate=@pulldate, inactive=@inactive, address1=@address1, address2=@address2, city=@city, st=@st, zip=@zip WHERE custno=@custno AND maj='01868'", conn);
            cmd.Parameters.AddWithValue("@custno", account.AccountNumber);
            cmd.Parameters.AddWithValue("@custname", account.Name);
            cmd.Parameters.AddWithValue("@loc", account.SiteCode);
            cmd.Parameters.AddWithValue("@class", account.Class);
            cmd.Parameters.AddWithValue("@installdate", (object?)account.InstallDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pulldate", (object?)account.PullDate?.ToDateTime(TimeOnly.MinValue) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@inactive", account.IsInactive);
            cmd.Parameters.AddWithValue("@address1", account.Address1);
            cmd.Parameters.AddWithValue("@address2", account.Address2);
            cmd.Parameters.AddWithValue("@city", account.City);
            cmd.Parameters.AddWithValue("@st", account.State);
            cmd.Parameters.AddWithValue("@zip", account.PostalCode);
            cmd.ExecuteNonQuery();
        }

        public void DeleteAccount(string accountNumber)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();
            var cmd = new SqlCommand(@"DELETE FROM AVIData..CustMast WHERE custno=@custno AND maj='01868'", conn);
            cmd.Parameters.AddWithValue("@custno", accountNumber);
            cmd.ExecuteNonQuery();
        }
    }
}
