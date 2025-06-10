using System;
using System.ComponentModel;

namespace desktop_AmzOpsApi.Models
{
    public enum TeamMemberRowState { Unchanged, New, Modified, Deleted }
    public class AmazonTeamMember
    {
        private bool _hasBadge;
        public static bool SuppressTeamMemberPropertyChangeTracking { get; set; }
        public required string AdpEmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? HireDate { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string AdpStatus { get; set; }
        public DateOnly? TermDate { get; set; }
        public DateOnly? BackgroundCheckDate { get; set; }
        public string? BackgroundCheckReferenceId { get; set; }
        public DateOnly? AvettaCreateDate { get; set; }
        public string? AvettaLogin { get; set; }
        public string? AvettaFlagStatus { get; set; }
        //public DateTime? BadgeRequestDate { get; set; }
        //public DateTime? BadgeReceiveDate { get; set; }
        //public DateTime? BadgeDeactivateDate { get; set; }
        //public bool HasBadge
        //{
        //    get => _hasBadge;
        //    set
        //    {
        //        if (_hasBadge != value)
        //        {
        //            _hasBadge = value;
        //            OnTeamMemberPropertyChanged(nameof(HasBadge));
        //            if (!SuppressTeamMemberPropertyChangeTracking &&
        //                State != TeamMemberRowState.New &&
        //                State != TeamMemberRowState.Deleted &&
        //                State == TeamMemberRowState.Unchanged)
        //            {
        //                State = TeamMemberRowState.Modified;
        //            }
        //        }
        //    }
        //}
        public TeamMemberRowState State { get; set; } = TeamMemberRowState.Unchanged;
        public event PropertyChangedEventHandler TeamMemberPropertyChanged;
        protected void OnTeamMemberPropertyChanged(string name) =>
            TeamMemberPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
