using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        { }

        public DbSet<Job> Jobs { get; set; }
    }
}
