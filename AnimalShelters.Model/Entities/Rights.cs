using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AnimalShelters.Model.Entities
{
    public class Rights : IEntityBase
    {
        public Rights() { }

        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public int IdModule { get; set; }

        [ForeignKey("IdModule")]
        public Module Module { get; set; }
        public int RightsToUserId { get; set; }

        public List<RightsToUser> RightsToUser { get; set; }
    }
}
