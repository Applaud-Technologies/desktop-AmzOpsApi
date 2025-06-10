using System;
using System.Collections.Generic;
using desktop_AmzOpsApi.Models;

namespace desktop_AmzOpsApi
{
    public interface ITestDataProvider
    {
        List<AmazonAccount> GetAccounts();
        List<AmazonSite> GetSites();
        List<AmazonTeamMember> GetTeamMembers();
        List<Branch> GetBranches();
        List<Contact> GetContacts();
    }

    public class TestDataProvider : ITestDataProvider
    {
        public List<AmazonAccount> GetAccounts() => new List<AmazonAccount>
        {
            new AmazonAccount {
                AccountNumber = "A001",
                Name = "Test Account 1",
                SiteCode = "S001",
                Class = "Prime",
                InstallDate = DateOnly.FromDateTime(new DateTime(2022, 1, 15)),
                PullDate = null,
                IsInactive = false,
                Address1 = "123 Main St",
                Address2 = "",
                City = "Metropolis",
                State = "CA",
                PostalCode = "90001"
            },
            new AmazonAccount {
                AccountNumber = "A002",
                Name = "Test Account 2",
                SiteCode = "S002",
                Class = "Basic",
                InstallDate = DateOnly.FromDateTime(new DateTime(2023, 5, 10)),
                PullDate = null,
                IsInactive = true,
                Address1 = "456 Elm St",
                Address2 = "Apt 2B",
                City = "Gotham",
                State = "NY",
                PostalCode = "10001"
            }
        };

        public List<AmazonSite> GetSites() => new List<AmazonSite>
        {
            new AmazonSite {
                Id = 1,
                SiteCode = "S001",
                SiteType = "Warehouse",
                Size = 15000,
                Population = 120,
                Notes = "Main warehouse",
                Status = "Active",
                Address1 = "123 Main St",
                Address2 = "",
                City = "Metropolis",
                Region = "CA",
                PostalCode = "90001",
                Country = "USA"
            },
            new AmazonSite {
                Id = 2,
                SiteCode = "S002",
                SiteType = "Office",
                Size = 5000,
                Population = 40,
                Notes = "Regional office",
                Status = "Inactive",
                Address1 = "456 Side Ave",
                Address2 = "Suite 200",
                City = "Gotham",
                Region = "NY",
                PostalCode = "10001",
                Country = "USA"
            }
        };

        public List<AmazonTeamMember> GetTeamMembers() => new List<AmazonTeamMember>
        {
            new AmazonTeamMember {
                AdpEmployeeId = "E001",
                FirstName = "Alice",
                LastName = "Smith",
                HireDate = DateOnly.FromDateTime(new DateTime(2021, 6, 1)),
                Job = "Manager",
                Department = "Operations",
                AdpStatus = "Active",
                TermDate = null,
                BackgroundCheckDate = DateOnly.FromDateTime(new DateTime(2021, 5, 20)),
                BackgroundCheckReferenceId = "BG123",
                AvettaCreateDate = DateOnly.FromDateTime(new DateTime(2021, 6, 2)),
                AvettaLogin = "alice.smith@avetta.com",
                AvettaFlagStatus = "Green",
                State = TeamMemberRowState.Unchanged
            },
            new AmazonTeamMember {
                AdpEmployeeId = "E002",
                FirstName = "Bob",
                LastName = "Johnson",
                HireDate = DateOnly.FromDateTime(new DateTime(2022, 2, 15)),
                Job = "Associate",
                Department = "Logistics",
                AdpStatus = "Inactive",
                TermDate = DateOnly.FromDateTime(new DateTime(2023, 1, 10)),
                BackgroundCheckDate = DateOnly.FromDateTime(new DateTime(2022, 2, 1)),
                BackgroundCheckReferenceId = "BG456",
                AvettaCreateDate = DateOnly.FromDateTime(new DateTime(2022, 2, 16)),
                AvettaLogin = "bob.johnson@avetta.com",
                AvettaFlagStatus = "Yellow",
                State = TeamMemberRowState.Unchanged
            }
        };
        public List<Branch> GetBranches() => new List<Branch>
        {
            new Branch { Id = 1, Name = "West Coast Branch", Location = "San Francisco, CA", Code = "WCB" },
            new Branch { Id = 2, Name = "East Coast Branch", Location = "New York, NY", Code = "ECB" },
            new Branch { Id = 3, Name = "Midwest Branch", Location = "Chicago, IL", Code = "MWB" }
        };

        public List<Contact> GetContacts() => new List<Contact>
        {
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "555-1001", BranchId = 1 },
            new Contact { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Phone = "555-1002", BranchId = 1 },
            new Contact { Id = 3, FirstName = "Mike", LastName = "Brown", Email = "mike.brown@example.com", Phone = "555-2001", BranchId = 2 },
            new Contact { Id = 4, FirstName = "Emily", LastName = "Davis", Email = "emily.davis@example.com", Phone = "555-3001", BranchId = 3 },
            new Contact { Id = 5, FirstName = "Sarah", LastName = "Wilson", Email = "sarah.wilson@example.com", Phone = "555-2002", BranchId = 2 }
        };
    }
}
