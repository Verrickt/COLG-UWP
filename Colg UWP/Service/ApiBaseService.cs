using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Colg_UWP.Util;

namespace Colg_UWP.Service
{
    public  class ApiBaseService:ApiBase
    {
        protected static async Task<JObject> GetJson(string url,Dictionary<string,string> dictioanry)
        {
            try
            {
                string json;
                if (dictioanry==null)
                {
                    json = await GetPost(url).ConfigureAwait(false);
                }
                else
                {
                    json = await GetPost(url,dictioanry).ConfigureAwait(false);

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
            catch (Exception e)
            {
                Logging.WriteLine($"Exceptoin thown at ApiBaseService.GetJson{Environment.NewLine}" +
                    $"{e.ToString()}");
                return null;
            }
        }

        protected static async Task<JObject> GetJson(string url) => await GetJson(url, null).ConfigureAwait(false);
    }
}
