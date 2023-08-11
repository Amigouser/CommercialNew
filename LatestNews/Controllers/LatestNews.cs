using JWTAuthorization.Helper;
using JWTAuthorization.Model;
using LatestNews.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace LatestNews.Controllers
{
    [ApiController]
    public class LatestNews : ControllerBase
    {
        [HttpGet]
        [Route("api/GetLatestNews")]

        public async Task<LatestNewsResp> GetLatestNews(LatestNewsReq req)
        {
            LatestNewsResp latestNewsResponse = new LatestNewsResp();

            #region Token Validating //Validate Token
            string? authorization = HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authorization))
            {
                ServiceHeaderInfo headerInfo = JwtHelper.AuthenticateJWTToken(authorization);

                if (!headerInfo.IsAuthenticated)
                {
                    latestNewsResponse.code = "205";
                    latestNewsResponse.message = "ERROR - Token is invailid.";
                    latestNewsResponse.result = null;
                    return latestNewsResponse;
                }
            }
            else
            {
                return latestNewsResponse;
            }
            #endregion
            LatestNewsData GetNewsData= null;
            List<LatestNewsData> GetNewsList = new List<LatestNewsData>();

            string URL = "https://run.mocky.io/v3/e1fffb22-f3c7-45b9-b9bc-1e6b31cc91e8";
            var res = APIHelper(URL, "");
            LatestNewsResp? GetData = JsonConvert.DeserializeObject<LatestNewsResp>(res);

            foreach (var item in GetData.result)
            {
                GetNewsData = new LatestNewsData()
                {
                    date_time = item.date_time,
                    img_url = item.img_url,
                    thumb_image = item.thumb_image,
                    url = item.url,
                    url_type = item.url_type,
                    title = item.title,
                    short_desc = item.short_desc,
                    description = item.description
                };
                GetNewsList.Add(GetNewsData);
            }
            latestNewsResponse.code = GetData.code;
            latestNewsResponse.message = GetData.message;
            latestNewsResponse.result_count = GetData.result_count;
            latestNewsResponse.result = GetNewsList;
            
            return latestNewsResponse;
        }
        public static string APIHelper(string url, object requestObj)
        {
            string jsonResponse = string.Empty;
            Stream stream = null;
            Stream newstream = null;
            try
            {
                if (!ServicePointManager.SecurityProtocol.HasFlag(SecurityProtocolType.Tls12))
                {
                    ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls12;
                }

                Uri uri = new Uri(url);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Headers.Add("token", "dR$545#h^jjmanJ");
                request.ContentType = "application/json";
                request.Method = "POST";
                if (requestObj != null)
                {
                    string sb = JsonConvert.SerializeObject(requestObj);
                    byte[] data = Encoding.ASCII.GetBytes(sb);
                    stream = request.GetRequestStream();
                    stream.Write(data, 0, data.Length);
                    stream.Close();
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {                    
                    newstream = response.GetResponseStream();
                    if (newstream != null)
                    {
                        StreamReader sr = new StreamReader(newstream);
                        jsonResponse = sr.ReadToEnd();
                        string errorResponse = "\"p_err_cd\":\"1\"";
                        if (jsonResponse.Contains(errorResponse))
                        {
                            return null;
                        }
                        sr.Close();
                    }
                }
            }
            catch (Exception)
            {
                ////catch
            }
            finally
            {
                if (newstream != null)
                {
                    newstream.Close();
                }
                if (stream != null)
                {
                    stream.Close();
                }
            }
            return jsonResponse;
        }
    }
}
