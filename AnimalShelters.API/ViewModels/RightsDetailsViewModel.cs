using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.ViewModels
{
    public class RightsDetailsViewModel
    {
        public int Id { get; set; }
        public int IdRight { get; set; }
        public int IdUser { get; set; }
        public int IdModule { get; set; }

        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string Email { get; set; }

        public string ModuleName { get; set; }
        public string ModuleSymbol { get; set; }

        public string RightName { get; set; }
        public string RightSymbol { get; set; }
    }
}
