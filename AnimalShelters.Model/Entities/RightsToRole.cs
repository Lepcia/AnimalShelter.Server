using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class RightsToRole : IEntityBase
    {
        public RightsToRole() { }

        public int Id { get; set; }
        public int IdRight { get; set; }
        public int IdRole { get; set; }

        public Rights Right { get; set; }

        [ForeignKey("IdRole")]
        public Role Role { get; set; }
    }
}
