namespace RiseTechnology.Common
{
    public static class Enums
    {
        public enum ContactType : ushort
        {
            Telephone = 1,
            EMail = 2,
            Location = 3,
        }
        public enum ReportStatus : ushort
        {
            Pending = 1,
            Preparing = 2,
            Prepared = 3,
        }
    }
}
