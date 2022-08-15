using System.Linq;
namespace LMS.Models
{
    public class AccountRepo : IAccountrepo
    {
        private readonly LMSContext _context;

        public AccountRepo(LMSContext context)
        {
            _context = context;
        }

        public Account getUserByName(string username)
        {
            return _context.Accounts.FirstOrDefault(u => u.UserName == username);
        }
    }
}
