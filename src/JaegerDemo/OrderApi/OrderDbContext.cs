namespace OrderApi
{
    using Microsoft.EntityFrameworkCore;

    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public void Seed()
        {
            Database.OpenConnection();
            Database.EnsureCreated();
            Database.Migrate();

            Orders.Add(new Order(1, 1, "surface book 2"));
            Orders.Add(new Order(2, 1, "surface go"));
            Orders.Add(new Order(3, 1, "surface pro 6"));
            Orders.Add(new Order(4, 2, "surace book 2"));

            SaveChanges();
        }
    }

    public class Order
    { 
        public Order(int id, int customerId, string productName)
        {
            this.Id = id;
            this.CustomerId = customerId;
            this.ProductName = productName;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ProductName { get; set; }
    }
}
