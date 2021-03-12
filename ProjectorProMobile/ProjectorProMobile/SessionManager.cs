using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ProjectorProMobile
{
    public static class SessionManager
    {
        private static int _sessionCode = 0000;
        public static int SessionCode
        {
            get
            {
                return _sessionCode;
            }
            set
            {
                _sessionCode = value;
                Preferences.Set("sessionCode", _sessionCode.ToString());
            }
        }
        public enum HostStatus
        {
            Solo,
            Follow,
            Host
        }
        private static HostStatus _hosting = HostStatus.Solo;
        public static HostStatus Hosting
        {
            get
            {
                return _hosting;
            }
            set
            {
                if (value == HostStatus.Solo && _hosting == HostStatus.Follow)
                {
                    ResetId();
                }
                _hosting = value;
                Preferences.Set("hostStatus", (int)_hosting);
            }
        }
        public static async Task<int?> CreateSessionAsync()
        {
            string createEndPt = string.Format("http://{0}/api/session/create-session", getBaseUrl());
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage task = await client.PostAsync(createEndPt, null);

                string relativeUrl = "/api/session/last-session-id";
                var result = await httpGet(relativeUrl, null);

                SessionCode = int.Parse(await result.Content.ReadAsStringAsync());
                Hosting = HostStatus.Host;
                return SessionCode;
            }
            catch(Exception)
            {
                return null;
            }


        }

        public static async Task<bool> UpdateSessionAsync(int id)
        {
            string updateEndPt = string.Format("http://{0}/api/session/update-session", getBaseUrl());
            HttpClient client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                sessionCode = SessionCode,
                songId = id
            });
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                _ = await client.PostAsync(updateEndPt, stringContent);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public static async Task<int?> CheckSessionChanges(int id)
        {
            string relativeUrl = "/api/session/get-session-changes";
            var paramsList = new List<KeyValuePair<string, string>> 
            {
                new KeyValuePair<string, string>("id", id.ToString()),
                new KeyValuePair<string, string>("code", SessionCode.ToString())
            };

            try
            {
                HttpResponseMessage response = await httpGet(relativeUrl, paramsList);
                if (response.StatusCode == System.Net.HttpStatusCode.NotModified)
                {
                    return id;
                }

                string contents = await response.Content.ReadAsStringAsync();
                return int.Parse(contents);
            }
            catch(Exception)
            {
                return null;
            }

            
        }

        public static async Task<bool?> CheckSessionExists()
        {
            string relativeUrl = "/api/session/get-session-exists";
            var paramsList = new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("code", SessionCode.ToString()) };

            try
            {
                var response = await httpGet(relativeUrl, paramsList);
                string result = await response.Content.ReadAsStringAsync();
                return (result == "true");
            }
            catch
            {
                return null;
            }
        }

        private static async Task<HttpResponseMessage> httpGet(string relativeUrl, List<KeyValuePair<string, string>> paramsList)
        {
            string endPt = "http://" + getBaseUrl() + relativeUrl;
            if (paramsList != null)
            {
                for (int i = 0; i < paramsList.Count; i++)
                {
                    endPt += (i == 0) ? "?" : "&";
                    endPt += paramsList[i].Key + "=" + paramsList[i].Value;
                }
            }

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(endPt);
            return response;
        }

        private static string getBaseUrl()
        {
            const string defaultUrl = "projector-pro-server.herokuapp.com";
            string url = Preferences.Get("serverAddress", defaultUrl);
            return url;
        }

        // Watch for updates
        public delegate void IdChangedHandler(int newId);
        public static event IdChangedHandler IdChanged;

        static bool finishedUpdating = true;
        static bool checkUpdates;
        static int _id;
        static int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    _id = value;
                    IdChanged(value);
                }
            }
        }
        public static void BeginUpdateChecks()
        {
            if (Hosting != HostStatus.Follow) return;

            checkUpdates = true;
            int updateInterval = 2; // check for updates every 2 seconds
            Device.StartTimer(TimeSpan.FromSeconds(updateInterval), () =>
            {
                _ = Task.Run(async () =>
                  {
                      if (finishedUpdating)
                      {
                          finishedUpdating = false;
                          if (checkUpdates)
                          {
                              int? newId = await CheckSessionChanges(Id);
                              if (newId != null)
                              {
                                  Id = (int)newId;
                              }
                              else
                              {
                                  //error; don't do anything and allow updates to continue
                              }
                          }
                          finishedUpdating = true;
                      }
                  });
                return checkUpdates;
            });
        }

        public static void StopUpdateChecks()
        {
            checkUpdates = false;
        }

        private static void ResetId()
        {
            _id = default(int);
        }
    }
}