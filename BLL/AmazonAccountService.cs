using desktop_AmzOpsApi.DAL;
using System.Collections.Generic;
using desktop_AmzOpsApi.Models;
using System.Linq;

namespace desktop_AmzOpsApi.BLL
{
    public class AmazonAccountService
    {
        private readonly AmazonAccountRepo _repo;

        public AmazonAccountService(AmazonAccountRepo repo)
        {
            _repo = repo;
        }

        public List<AmazonAccount> GetAccounts() => _repo.GetAllAccounts();

        public AmazonAccount? GetAccountById(string accountNumber)
        {
            // Example: add business logic here before/after repo call
            return _repo.GetAllAccounts().FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        public void CreateAccount(AmazonAccount account)
        {
            // Add validation/business rules as needed
            _repo.InsertAccount(account);
        }

        public void UpdateAccount(AmazonAccount account)
        {
            // Add validation/business rules as needed
            _repo.UpdateAccount(account);
        }

        public void DeleteAccount(string accountNumber)
        {
            // Add validation/business rules as needed
            _repo.DeleteAccount(accountNumber);
        }
    }
}
