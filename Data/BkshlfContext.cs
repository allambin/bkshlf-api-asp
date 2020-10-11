using BKSHLF.Models;
using Microsoft.EntityFrameworkCore;

namespace BKSHLF.Data
{
    public class BkshlfContext : DbContext
    {
        public BkshlfContext(DbContextOptions<BkshlfContext> opt) : base(opt)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}