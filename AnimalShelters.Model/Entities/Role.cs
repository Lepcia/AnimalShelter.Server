using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Role : IEntityBase
    {
        public Role() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        
        [InverseProperty("Role")]
        public List<RightsToRole> RightsToRole { get; set; }
    }
}
