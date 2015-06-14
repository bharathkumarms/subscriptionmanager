using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc;

namespace CEvery.Models
{
    public class Subscription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SubscriptionNo { get; set; }
        [DisplayName("Customer")]
        public string CustomerName { get; set; }
        public string Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Pin { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Comments { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsInvalid { get; set; }

        public string Phone { get; set; }
        public string Address3 { get; set; }
    }

    public class SubscriptionDBContext : DbContext
    {
        public SubscriptionDBContext() : base("LeadDBContext") { }
        public DbSet<Subscription> Subscription { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MyDbContextInitializer());

            base.OnModelCreating(modelBuilder);
        }

        public class MyDbContextInitializer : DropCreateDatabaseIfModelChanges<SubscriptionDBContext>
        {
            protected override void Seed(SubscriptionDBContext dbContext)
            {
                // seed data

                base.Seed(dbContext);
            }
        }
    }
}