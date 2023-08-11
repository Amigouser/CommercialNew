namespace JWTAuthorization.Model
{
    public class JWTAPIToken
    {
        public string Access_Token { get; set; }
    }

    public class TokenResponse
    {
        public int code { get; set; }
        public string? message { get; set; }
        public List<JWTAPIToken> result { get; set; }
    }
    public class ServiceHeaderInfo
    {
        public bool IsAuthenticated { get; set; }
        public string? Token { get; set; }
        public string? Message { get; set; }
    }

    public class ListReturnType<ReturnType>
    {
        public int code { get; set; }
        public string? message { get; set; }
        public List<ReturnType> result { get; set; }
    }
    public enum ServiceMassageCode
    {
        SUCCESS = 200,
        INVALID_PARAMETER = 201,
        ERROR = 202,
        DATA_NOT_EXIST = 203,
        SQL_ERROR = 204,
        UNAUTHORIZED_REQUEST = 205,
        ITEM_NOT_EXIST = 206,
        DATA_ALREADY_EXIST = 207,
        UNAUTHORIZED_USER= 208,
    }



}
