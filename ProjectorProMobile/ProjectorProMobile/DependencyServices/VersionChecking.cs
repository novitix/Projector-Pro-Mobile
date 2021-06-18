using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectorProMobile.DependencyServices
{
    static class VersionChecking
    {
        private static string compatibleServerVersion = "1.2";
        public static string UpdatesServer = "http://www.ppmserver.tk";

        public async static Task<bool> IsServerCompatible()
        {
            string serverVersion = await GetServerVersion();
            if (serverVersion is null) return false;

            return serverVersion == compatibleServerVersion;
        }

        private async static Task<string> GetServerVersion()
        {
            Querier querier = new Querier();

            string[] versionStr = (await querier.GetQueryAsync(UpdatesServer + "/api/version")).Split('.');
            if (versionStr.Length >= 2)
            {
                return versionStr[0] + '.' + versionStr[1];
            }
            else
            {
                return null;
            }
        }
    }
}
