using System.Data.Entity;

namespace WindowsFormsApp7
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=demoEntities") { }
        public DbSet<Material_type_import> material_Type_Imports { get; set; }
        public DbSet<Products_import> products_Imports { get; set; }
        public DbSet<Product_type_import> product_Type_Imports { get; set; }
        public DbSet<Partners_import> partners_Imports { get; set; }
        public DbSet<Partner_products_import> partner_Products_Imports { get; set; }
        public DbSet<Partner_type> partner_Types { get; set; }


    }
}
