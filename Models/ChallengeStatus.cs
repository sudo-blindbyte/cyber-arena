namespace CyberArenaWebForms.Models
{
    /// <summary>
    /// Challenge status constants
    /// </summary>
    public static class ChallengeStatus
    {
        public const string Active = "Active";
        public const string Draft = "Draft";
        public const string Archived = "Archived";

        public static readonly string[] All = { Active, Draft, Archived };
    }
}
