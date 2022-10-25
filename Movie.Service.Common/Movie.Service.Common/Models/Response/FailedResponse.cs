namespace Movies.service.Common.Models
{
    public class FailedResponse : Response
    {
        public override int ResultCode { get; set; } = 400;
        public FailedResponse() { }
    }
}