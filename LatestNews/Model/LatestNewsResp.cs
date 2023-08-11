namespace LatestNews.Model
{
    public class LatestNewsResp
    {
        public string code { get; set; }
        public string message { get; set; }
        public string result_count { get; set; }
        public List<LatestNewsData> result { get; set; }
    }
    public class LatestNewsData
    {
        public string date_time { get; set; }
        public string img_url { get; set; }
        public string thumb_image { get; set; }
        public string url { get; set; }
        public string url_type { get; set; }
        public string title { get; set; }
        public string short_desc { get; set; }
        public string description { get; set; }
    }
    public class LatestNewsReq
    {
        public string component_type { get; set; }
        public string page_size { get; set; }
        public string created_date { get; set; }
    }

}
