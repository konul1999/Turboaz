using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboAz.Models.Auditable;

namespace TurboAz.Models.Entities
{
    public class Model: AuditableEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int MarkaId { get; set; }

    }
}
