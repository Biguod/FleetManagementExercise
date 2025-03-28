using System.ComponentModel.DataAnnotations;

namespace FleetManagement.Domain.Models.Common
{
    public class BaseClass
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Archived { get; set; }
    }
}
