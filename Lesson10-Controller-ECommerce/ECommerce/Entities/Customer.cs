using System.Text.Json.Serialization;

namespace ECommerce.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
