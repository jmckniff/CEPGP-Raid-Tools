using System.Configuration;

namespace CEPGP.RaidTools
{
    public class AppSettings
    {
        public static string GuildName = ConfigurationManager.AppSettings["GuildName"];
    }
}