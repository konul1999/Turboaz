using TurboAz.Models.Auditable;

namespace TurboAz.Models.Entities
{
    public class Marka : AuditableEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }

    }
}
