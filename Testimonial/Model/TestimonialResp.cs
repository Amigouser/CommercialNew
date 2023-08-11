namespace Testimonial.Model
{
    public class TestimonialResp
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<TestimonialData> result { get; set; }
    }

    public class TestimonialData
    {
        public string name { get; set; }
        public string img_url { get; set; }
        public string video_url { get; set; }
        public string short_desc { get; set; }
        public string description { get; set; }
    }

    public class TestimonilaReq
    {
        public string component_type { get; set; }
        public string page_size { get; set; }
        public string created_date { get; set; }
    }
}
