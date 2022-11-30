namespace BirlesikOdeme.Models
{
    public class ResponseAuthentication
    {
        public bool fail { get; set; }
        public int statusCode { get; set; }
        public ResultDTO result { get; set; }
        public int count { get; set; }
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}
