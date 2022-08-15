namespace LMS.Models
{
    public interface IAccountrepo
    {
        Account getUserByName(string username);
    }
}
