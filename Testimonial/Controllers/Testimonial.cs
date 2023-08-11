using JWTAuthorization.Helper;
using JWTAuthorization.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using Testimonial.Model;

namespace Testimonial.Controllers
{
    [ApiController]
    public class Testimonial : ControllerBase
    {
        [HttpGet]
        [Route("api/GetTestimonialData")]
        public async Task<ListReturnType<TestimonialData>> GetTestimonialData(TestimonilaReq req)
        {
            ListReturnType<TestimonialData> testimonilaResponse = new ListReturnType<TestimonialData>();

            TestimonialData GetTestimonialData = null;
            List<TestimonialData> GetTestimonialList = new List<TestimonialData>();

            string URL = "https://run.mocky.io/v3/78592487-7048-43a8-b8bf-2c6c4dd75c0a";
            var res = APIHelper(URL, "");
            TestimonialResp? GetData = System.Text.Json.JsonSerializer.Deserialize<TestimonialResp>(res);

            foreach (var item in GetData.result)
            {
                GetTestimonialData = new TestimonialData()
                {
                    description = item.description,
                    img_url = item.img_url,
                    name = item.name,
                    short_desc = item.short_desc,
                    video_url = item.video_url
                };
                GetTestimonialList.Add(GetTestimonialData);
            }
            testimonilaResponse.code = Convert.ToInt32(GetData.code);
            testimonilaResponse.message = GetData.message;
            testimonilaResponse.result = GetTestimonialList;
            return testimonilaResponse;
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
                    string sb = System.Text.Json.JsonSerializer.Serialize(requestObj);
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
