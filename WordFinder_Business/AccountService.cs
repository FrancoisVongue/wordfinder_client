using Microsoft.EntityFrameworkCore;
using WordFinder_Repository;

namespace WordFinder_Business
{
    public class AccountService
    {
        private readonly ApiContext _context;
        
        public AccountService(ApiContext context)
        {
            _context = context;
        }
        
        
    }
}