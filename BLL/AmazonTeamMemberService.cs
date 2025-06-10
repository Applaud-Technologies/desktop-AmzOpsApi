using AmazonOperations.Common;
using desktop_AmzOpsApi.DAL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using desktop_AmzOpsApi.Models;
using desktop_AmzOpsApi.DAL;

namespace desktop_AmzOpsApi.BLL
{
    public class AmazonTeamMemberService
    {
        private readonly AmazonTeamMemberRepo _repo;

        public AmazonTeamMemberService(AmazonTeamMemberRepo repo)
        {
            _repo = repo;
        }

        public List<AmazonTeamMember> GetTeamMembers() => _repo.GetAllTeamMembers();

        public AmazonTeamMember? GetTeamMemberById(string adpEmployeeId)
        {
            // Example: add business logic here before/after repo call
            return _repo.GetAllTeamMembers().FirstOrDefault(t => t.AdpEmployeeId == adpEmployeeId);
        }

        public void CreateTeamMember(AmazonTeamMember teamMember)
        {
            // Add validation/business rules as needed
            _repo.InsertTeamMember(teamMember);
        }

        public void UpdateTeamMember(AmazonTeamMember teamMember)
        {
            // Add validation/business rules as needed
            _repo.UpdateTeamMember(teamMember);
        }

        public void DeleteTeamMember(string adpEmployeeId)
        {
            // Add validation/business rules as needed
            _repo.DeleteTeamMember(adpEmployeeId);
        }


        public async Task<SaveResult<AmazonTeamMember>> SaveChangesAsync(IEnumerable<AmazonTeamMember> teamMembers)
        {
            var result = new SaveResult<AmazonTeamMember>();

            using var conn = await _repo.GetOpenConnectionAsync();
            using var tx = conn.BeginTransaction();

            try
            {
                foreach (var teamMember in teamMembers)
                {
                    switch (teamMember.State)
                    {
                        //case TeamMemberRowState.New:
                        //    await _repo.InsertTeamMemberAsync(teamMember, conn, tx);
                        //    result.Inserted.Add(teamMember);
                        //    break;
                        case TeamMemberRowState.Modified:
                            await _repo.UpdateTeamMemberAsync(teamMember, conn, tx);
                            result.Updated.Add(teamMember);
                            break;
                        //case TeamMemberRowState.Deleted:
                        //    await _repo.DeleteTeamMemberAsync(teamMember.AdpEmployeeId, conn, tx);
                        //    result.Deleted.Add(teamMember);
                        //    break;
                    }
                }
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
            return result;
        }

    }
}
