using ApprovalChain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ApprovalChain.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        public DbSet<ArcDocument> ArcDocuments { get; set; }
        public DbSet<Employee> Employees { get; set; }  
    }
}
