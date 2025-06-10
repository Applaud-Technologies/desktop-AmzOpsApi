using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace desktop_AmzOpsApi.Models
{
    public enum SiteRowState { Unchanged, New, Modified, Deleted }
    public class AmazonSite
    {
        private string _siteCode;
        private string? _siteType;
        private int? _size;
        private int? _population;
        private string? _notes;
        private string? _status;
        private string? _address1;
        private string? _address2;
        private string? _city;
        private string? _region;
        private string? _country;
        private string? _postalCode;

        public static bool SuppressSitePropertyChangeTracking { get; set; }
        public int Id { get; set; }
        public string SiteCode
        {
            get => _siteCode;
            set
            {
                if (_siteCode != value)
                {
                    _siteCode = value;
                    OnSitePropertyChanged(nameof(SiteCode));
                    if (!SuppressSitePropertyChangeTracking &&
                        State != SiteRowState.New &&
                        State != SiteRowState.Deleted &&
                        State == SiteRowState.Unchanged)
                    {
                        State = SiteRowState.Modified;
                    }
                }
            }
        }
        public string? SiteType
        {
            get => _siteType;
            set
            {
                if (_siteType != value)
                {
                    _siteType = value;
                    OnSitePropertyChanged(nameof(SiteType));
                    if (!SuppressSitePropertyChangeTracking &&
                        State != SiteRowState.New &&
                        State != SiteRowState.Deleted &&
                        State == SiteRowState.Unchanged)
                    {
                        State = SiteRowState.Modified;
                    }
                }
            }
        }
        public int? Size { get; set; }
        public int? Population { get; set; }
        public string? Notes { get; set; }
        public string? Status { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public SiteRowState State { get; set; } = SiteRowState.Unchanged;

        public event PropertyChangedEventHandler SitePropertyChanged;
        protected void OnSitePropertyChanged(string name) =>
            SitePropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
