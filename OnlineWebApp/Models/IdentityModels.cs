using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineWebApp.Models.AppModels;

namespace OnlineWebApp.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Display name")]
        public string Display_Name { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Colour> Colours { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Shirt> Shirts { get; set; }
       
        public DbSet<Size> Sizes { get; set; }
        public DbSet<WishList> WishLists { get; set; }
      
        public DbSet<AddressBook> AddressBooks { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Point>Points { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<RecievedStock> RecievedStocks{ get; set; }
        public DbSet<DriverInfo> DriverInfos{ get; set; }
        public DbSet<JobPost>JobPosts { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Please> Pleases { get; set; }
        public DbSet<Solo> Solos { get; set; }
        public DbSet<Want> Wants { get; set; }
        //Role Management
        public DbSet<IdentityUserRole> UserInRole { get; set; }
        // public DbSet<ApplicationUser> appUsers { get; set; }
        public DbSet<ApplicationRole> appRoles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<OnlineWebApp.Models.AppModels.Pack> Packs { get; set; }

        public System.Data.Entity.DbSet<OnlineWebApp.Models.AppModels.Collect> Collects { get; set; }

        public System.Data.Entity.DbSet<OnlineWebApp.Models.AppModels.AssignDriver> AssignDrivers { get; set; }

		public System.Data.Entity.DbSet<OnlineWebApp.Models.AppModels.OrderList> OrderLists { get; set; }

        public System.Data.Entity.DbSet<OnlineWebApp.Models.AppModels.QRCode> QRCodes { get; set; }
    }
}