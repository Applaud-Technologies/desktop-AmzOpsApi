using System;

namespace desktop_AmzOpsApi.Models
{
    public class AmazonAccount
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public string SiteCode { get; set; }
        public string Class { get; set; }
        public DateOnly? InstallDate { get; set; }
        public DateOnly? PullDate { get; set; }
        public bool IsInactive { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
    }
}
