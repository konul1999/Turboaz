using Microsoft.VisualBasic;
using Turboaz.Enums;
using TurboAz.enums;
using TurboAz.Enums;
using TurboAz.Models.Auditable;

namespace TurboAz.Models.Entities
{
    public class Announcement : AuditableEntity
    {

        public int Id { get; set; }
        public int March { get; set; }
        public double Price { get; set; }
        public int ModelId { get; set; }
        public Banner Banner { get; set; }
        public FuelType FuelType { get; set; }
        public GearBox GearBox { get; set; }
        public Transmission Transmission { get; set; }
        public int Year { get; set; }

    }
}
