using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using spor_salonu.Models;


namespace spor_salonu.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        
        public static class ConfigSettings
        {
            public static string ConfigSettingss()
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                using (StreamReader file = File.OpenText(path))
                {
                    JObject o = JObject.Parse(file.ReadToEnd());
                    return (string)o["ConnectionStrings"]["DefaultConnectionString"];
                }
                return null;
            }
        }
        public DbSet<LogAdmin> Admins { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Mesaj> Mesajlar { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigSettings.ConfigSettingss());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }


}
