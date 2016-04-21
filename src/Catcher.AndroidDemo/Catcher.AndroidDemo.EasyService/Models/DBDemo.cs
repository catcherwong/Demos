namespace Catcher.AndroidDemo.EasyService.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBDemo : DbContext
    {
        public DBDemo()
            : base("name=DBDemo")
        {
        }

        public virtual DbSet<UserInfo> UserInfo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.UPassword)
                .IsUnicode(false);
        }
    }
}
