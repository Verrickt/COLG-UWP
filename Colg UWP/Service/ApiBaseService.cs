using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Colg_UWP.Service
{
    public  class ApiBaseService
    {
        public static async Task<JObject> GetJson(string url,Dictionary<string,string> dictioanry)
        {
            try
            {
                string json;
                if (dictioanry==null)
                {
                    json = await ApiBase.GetPost(url).ConfigureAwait(false);
                }
                else
                {
                    json = await ApiBase.GetPost(url,dictioanry).ConfigureAwait(false);

                }
                if (String.IsNullOrWhiteSpace(json))
                {
                    return null;
                }
                else
                {
                    return JObject.Parse(json);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<JObject> GetJson(string url) => await GetJson(url, null).ConfigureAwait(false);
    }
}
