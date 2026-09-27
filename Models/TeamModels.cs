using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CyberArenaWebForms.Models
{
    public class TeamMemberViewModel
    {
        public int    Id               { get; set; }
        public string Username         { get; set; } = string.Empty;
        public string AvatarInitials   { get; set; }
        public string AvatarClass      { get; set; } = string.Empty;
        public bool   IsCaptain        { get; set; }
        public int    Score            { get; set; }
        public int    ChallengesSolved { get; set; }
        public DateTime JoinedAt       { get; set; }
    }

    public class TeamListItemViewModel
    {
        public int    Id               { get; set; }
        public string Name             { get; set; } = string.Empty;
        public string CaptainUsername  { get; set; } = string.Empty;
        public int    Rank             { get; set; }
        public int    Score            { get; set; }
        public int    MemberCount      { get; set; }
        public int    ChallengesSolved { get; set; }
        public string Country          { get; set; } = string.Empty;
        public bool   IsMyTeam         { get; set; }

        public string RankBadgeClass
        {
            get
            {
                switch (Rank)
                {
                    case 1: return "gold";
                    case 2: return "silver";
                    case 3: return "bronze";
                    default: return "other";
                }
            }
        }
    }

    public class TeamListViewModel
    {
        public List<TeamListItemViewModel> Teams { get; set; } = new List<TeamListItemViewModel>();
        public string SearchQuery { get; set; }
        public bool   UserHasTeam { get; set; }
        public int?   MyTeamId    { get; set; }
    }

    public class TeamDetailsViewModel
    {
        public int    Id               { get; set; }
        public string Name             { get; set; } = string.Empty;
        public string Description      { get; set; }
        public string CaptainUsername  { get; set; } = string.Empty;
        public int    Rank             { get; set; }
        public int    Score            { get; set; }
        public int    ChallengesSolved { get; set; }
        public int    TotalChallenges  { get; set; }
        public string Country          { get; set; } = string.Empty;
        public DateTime CreatedAt      { get; set; }
        public bool   IsMyTeam         { get; set; }
        public bool   IsCaptain        { get; set; }

        public List<TeamMemberViewModel> Members { get; set; } = new List<TeamMemberViewModel>();

        // Score history for mini chart
        public List<int> ScoreHistory { get; set; } = new List<int>();
        public List<string> ScoreHistoryLabels { get; set; } = new List<string>();

        public int SolvedPercent
        {
            get
            {
                return TotalChallenges > 0 ? (int)((double)ChallengesSolved / TotalChallenges * 100) : 0;
            }
        }

        public string RankBadgeClass
        {
            get
            {
                switch (Rank)
                {
                    case 1: return "gold";
                    case 2: return "silver";
                    case 3: return "bronze";
                    default: return "other";
                }
            }
        }
    }

    public class TeamMockData
    {
        public static List<TeamListItemViewModel> GetAll()
        {
            return new List<TeamListItemViewModel>
            {
                new TeamListItemViewModel { Id=1, Name="ByteForce",   CaptainUsername="CipherMaster", Rank=1, Score=32400, MemberCount=4, ChallengesSolved=58, Country="US" },
                new TeamListItemViewModel { Id=2, Name="CodeStrike",  CaptainUsername="n3tR4nger",    Rank=2, Score=28900, MemberCount=3, ChallengesSolved=52, Country="DE" },
                new TeamListItemViewModel { Id=3, Name="NullByte",    CaptainUsername="0x_exploit",   Rank=3, Score=25600, MemberCount=4, ChallengesSolved=47, Country="GB", IsMyTeam=true },
                new TeamListItemViewModel { Id=4, Name="PhantomByte", CaptainUsername="r00t_daemon",  Rank=4, Score=21300, MemberCount=3, ChallengesSolved=41, Country="IN" },
                new TeamListItemViewModel { Id=5, Name="ShadowStack", CaptainUsername="vuln_hunter",  Rank=5, Score=17800, MemberCount=2, ChallengesSolved=34, Country="KR" },
                new TeamListItemViewModel { Id=6, Name="ZeroDay",     CaptainUsername="b1nary_b0ss",  Rank=6, Score=14200, MemberCount=4, ChallengesSolved=28, Country="ES" },
                new TeamListItemViewModel { Id=7, Name="PwnStars",    CaptainUsername="stack_smasher",Rank=7, Score=11500, MemberCount=2, ChallengesSolved=22, Country="NL" },
                new TeamListItemViewModel { Id=8, Name="CryptoKings", CaptainUsername="crypto_cracker",Rank=8, Score=9100, MemberCount=3, ChallengesSolved=17, Country="IT" }
            };
        }

        public static TeamDetailsViewModel GetDetail(int id)
        {
            switch (id)
            {
                case 1:
                    return new TeamDetailsViewModel
                    {
                        Id=1, Name="ByteForce", Description="Elite offensive security team specialising in web exploitation and binary pwn.", CaptainUsername="CipherMaster",
                        Rank=1, Score=32400, ChallengesSolved=58, TotalChallenges=72, Country="US",
                        CreatedAt=DateTime.UtcNow.AddDays(-90),
                        Members = new List<TeamMemberViewModel>
                        {
                            new TeamMemberViewModel { Id=1, Username="CipherMaster",  AvatarInitials="CM", AvatarClass="",     IsCaptain=true,  Score=9850, ChallengesSolved=22, JoinedAt=DateTime.UtcNow.AddDays(-90) },
                            new TeamMemberViewModel { Id=2, Username="shell_ghost",   AvatarInitials="SG", AvatarClass="alt-1", IsCaptain=false, Score=7980, ChallengesSolved=19, JoinedAt=DateTime.UtcNow.AddDays(-85) },
                            new TeamMemberViewModel { Id=3, Username="xor_queen",     AvatarInitials="XQ", AvatarClass="alt-2", IsCaptain=false, Score=5870, ChallengesSolved=15, JoinedAt=DateTime.UtcNow.AddDays(-80) },
                            new TeamMemberViewModel { Id=4, Username="rop_chain",     AvatarInitials="RC", AvatarClass="alt-3", IsCaptain=false, Score=3850, ChallengesSolved=10, JoinedAt=DateTime.UtcNow.AddDays(-70) },
                        },
                        ScoreHistory       = new List<int> {800, 2100, 3900, 6200, 9100, 14500, 20300, 28100, 32400},
                        ScoreHistoryLabels = new List<string> {"Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"},
                    };
                case 2:
                    return new TeamDetailsViewModel
                    {
                        Id=2, Name="CodeStrike", Description="Red team veterans from Europe focused on cryptography and reverse engineering.", CaptainUsername="n3tR4nger",
                        Rank=2, Score=28900, ChallengesSolved=52, TotalChallenges=72, Country="DE",
                        CreatedAt=DateTime.UtcNow.AddDays(-80),
                        Members = new List<TeamMemberViewModel>
                        {
                            new TeamMemberViewModel { Id=5, Username="n3tR4nger",    AvatarInitials="NR", AvatarClass="",     IsCaptain=true,  Score=9340, ChallengesSolved=21, JoinedAt=DateTime.UtcNow.AddDays(-80) },
                            new TeamMemberViewModel { Id=6, Username="pwn_wizard",   AvatarInitials="PW", AvatarClass="alt-2", IsCaptain=false, Score=6300, ChallengesSolved=16, JoinedAt=DateTime.UtcNow.AddDays(-75) },
                            new TeamMemberViewModel { Id=7, Username="stack_smasher",AvatarInitials="SS", AvatarClass="alt-3", IsCaptain=false, Score=4200, ChallengesSolved=11, JoinedAt=DateTime.UtcNow.AddDays(-65) },
                        },
                        ScoreHistory       = new List<int> {500, 1800, 3400, 5900, 8800, 13200, 18600, 24000, 28900},
                        ScoreHistoryLabels = new List<string> {"Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"},
                    };
                case 3:
                    return new TeamDetailsViewModel
                    {
                        Id=3, Name="NullByte", Description="Your team — hackers by day, defenders by night. Specialising in forensics and OSINT.", CaptainUsername="0x_exploit",
                        Rank=3, Score=25600, ChallengesSolved=47, TotalChallenges=72, Country="GB",
                        CreatedAt=DateTime.UtcNow.AddDays(-75), IsMyTeam=true,
                        Members = new List<TeamMemberViewModel>
                        {
                            new TeamMemberViewModel { Id=8,  Username="0x_exploit",  AvatarInitials="0E", AvatarClass="",     IsCaptain=true,  Score=8720, ChallengesSolved=20, JoinedAt=DateTime.UtcNow.AddDays(-75) },
                            new TeamMemberViewModel { Id=9,  Username="h4x0r_pro",   AvatarInitials="AC", AvatarClass="alt-1", IsCaptain=false, Score=6920, ChallengesSolved=17, JoinedAt=DateTime.UtcNow.AddDays(-70) },
                            new TeamMemberViewModel { Id=10, Username="hex_ninja",   AvatarInitials="HN", AvatarClass="alt-2", IsCaptain=false, Score=4990, ChallengesSolved=13, JoinedAt=DateTime.UtcNow.AddDays(-60) },
                            new TeamMemberViewModel { Id=11, Username="xss_panda",   AvatarInitials="XP", AvatarClass="alt-3", IsCaptain=false, Score=1850, ChallengesSolved=3,  JoinedAt=DateTime.UtcNow.AddDays(-20) },
                        },
                        ScoreHistory       = new List<int> {300, 1400, 2900, 5000, 7600, 11900, 17000, 22500, 25600},
                        ScoreHistoryLabels = new List<string> {"Sep 1","Sep 8","Sep 15","Sep 22","Sep 29","Oct 6","Oct 13","Oct 20","Oct 27"},
                    };
                default:
                    return new TeamDetailsViewModel
                    {
                        Id=id, Name="Unknown Team", Rank=99, Score=0, ChallengesSolved=0, TotalChallenges=72,
                        CaptainUsername="—", CreatedAt=DateTime.UtcNow.AddDays(-30)
                    };
            }
        }
    }
}
