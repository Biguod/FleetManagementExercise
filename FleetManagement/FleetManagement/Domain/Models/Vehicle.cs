using System.ComponentModel.DataAnnotations.Schema;
using FleetManagement.Domain.Models.Common;

namespace FleetManagement.Domain.Models
{
    public class Vehicle : BaseClass
    {
        public string ChassisId 
        {
            get { return string.Format("{0}{1}", ChassisSeries, ChassisNumber); }
        }
        public required string ChassisSeries { get; set; }
        public required uint ChassisNumber { get; set; }
        public required Guid VehicleDetailId { get; set; }
        [ForeignKey(nameof(VehicleDetailId))]
        public virtual VehicleDetail Details { get; set; }
        public required string Color { get; set; }
        //Colocar data notations
    }
}
