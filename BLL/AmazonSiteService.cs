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
    public class AmazonSiteService
    {
        private readonly AmazonSiteRepo _repo;

        public AmazonSiteService(AmazonSiteRepo repo)
        {
            _repo = repo;
        }

        public List<AmazonSite> GetSites() => _repo.GetAllSites();

        public AmazonSite? GetSiteById(int id)
        {
            // Example: add business logic here before/after repo call
            return _repo.GetAllSites().FirstOrDefault(s => s.Id == id);
        }

        public void CreateSite(AmazonSite site)
        {
            // Add validation/business rules as needed
            _repo.InsertSite(site);
        }

        public void UpdateSite(AmazonSite site)
        {
            // Add validation/business rules as needed
            _repo.UpdateSite(site);
        }

        public void DeleteSite(int id)
        {
            // Add validation/business rules as needed
            _repo.DeleteSite(id);
        }


        public async Task<SaveResult<AmazonSite>> SaveChangesAsync(IEnumerable<AmazonSite> sites)
        {
            var result = new SaveResult<AmazonSite>();

            using var conn = await _repo.GetOpenConnectionAsync();
            using var tx = conn.BeginTransaction();

            try
            {
                foreach (var site in sites)
                {
                    switch (site.State)
                    {
                        case SiteRowState.New:
                            await _repo.InsertSiteAsync(site, conn, tx);
                            result.Inserted.Add(site);
                            break;
                        case SiteRowState.Modified:
                            await _repo.UpdateSiteAsync(site, conn, tx);
                            result.Updated.Add(site);
                            break;
                        case SiteRowState.Deleted:
                            await _repo.DeleteSiteAsync(site.Id, conn, tx);
                            result.Deleted.Add(site);
                            break;
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
