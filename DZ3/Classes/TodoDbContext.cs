using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ3.Classes
{
    public class TodoDbContext : System.Data.Entity.DbContext
    {
        public TodoDbContext(string connectionString) : base(connectionString)
        {
        }

        public IDbSet<TodoItem> TodoItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>().HasKey(item => item.Id);
            modelBuilder.Entity<TodoItem>().Property(item => item.Text).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(item => item.IsCompleted).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(item => item.DateCreated).IsRequired();
            modelBuilder.Entity<TodoItem>().Property(item => item.UserId).IsRequired();

            modelBuilder.Entity<TodoItem>().Property(item => item.DateCompleted).IsOptional();
        }
    }

}
