using dagnys_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace dagnys_api.Data;

    
    public class DataContext : DbContext
{
    public DbSet<Product>Products { get; set; }
    public DbSet<Supplier>Suppliers { get; set; }
    public DbSet<Recipe>Recipes { get; set; }
    public DbSet<Purchase>Purchases { get; set; }
    public DbSet<RawMaterial>RawMaterials { get; set; }
    public DbSet<SupplierRawMaterial>SupplierRawMaterials { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
     public DbSet<Address> Addresses { get; set; }
    public DbSet<PostalAddress> PostalAddresses { get; set; }
    public DbSet<AddressType> AddressTypes { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<SupplierAddress> SupplierAddresses { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }    

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<SupplierRawMaterial>()
                .HasKey(srm => new { srm.SupplierId, srm.RawMaterialId });

            modelBuilder.Entity<SupplierRawMaterial>()
                .HasOne(srm => srm.Supplier)
                .WithMany(s => s.SupplierRawMaterials)
                .HasForeignKey(srm => srm.SupplierId);

            modelBuilder.Entity<SupplierRawMaterial>()
                .HasOne(srm => srm.RawMaterial)
                .WithMany(r => r.SupplierRawMaterials)
                .HasForeignKey(srm => srm.RawMaterialId);

        modelBuilder.Entity<OrderItem>().HasKey(o => new { o.ProductId, o.OrderId });
        modelBuilder.Entity<CustomerAddress>().HasKey(c => new { c.AddressId, c.CustomerId });
        modelBuilder.Entity<SupplierAddress>().HasKey(s => new { s.AddressId, s.SupplierId });

        
        /*modelBuilder.Entity<Address>()
            .HasOne(a => a.PostalAddress)
            .WithMany()
            .HasForeignKey(a => a.PostalAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.AddressType)
            .WithMany()
            .HasForeignKey(a => a.AddressTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.CustomerAddresses)
            .WithOne(ca => ca.Customer)
            .HasForeignKey(ca => ca.CustomerId);

        modelBuilder.Entity<Supplier>()
            .HasMany(s => s.SupplierAddresses)
            .WithOne(sa => sa.Supplier)
            .HasForeignKey(sa => sa.SupplierId);*/
        }
}
