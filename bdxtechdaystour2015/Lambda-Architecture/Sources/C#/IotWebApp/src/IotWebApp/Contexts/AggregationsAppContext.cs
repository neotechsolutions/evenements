using IotWebApp.Models;
using Microsoft.Data.Entity;

namespace IotWebApp.Contexts
{
    public class AggregationsAppContext : DbContext
    {
        public DbSet<Aggregation> Aggregation { get; set; }
    }
}
