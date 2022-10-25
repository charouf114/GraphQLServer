namespace Movies.Service.Common.Models
{
    public class UpdateUserInput
    {
        public User User { get; set; }
        public updateUserType UpdateType { get; set; }
    }

    public enum updateUserType
    {
        Add = 0,
        Update = 1,
        Disable = 2, // To Double Check 
        Delete = 3
    }
}
