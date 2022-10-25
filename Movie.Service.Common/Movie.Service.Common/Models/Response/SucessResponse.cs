namespace Movies.service.Common.Models
{
    public class SucessResponse : Response
    {
        public override int ResultCode { get; set; } = 200;
        public SucessResponse() { }
    }
}