using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class ModuleDetailsViewModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public int IdUser { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserEmail { get; set; }
    }
}
