﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalShelters.API.UserDtoN
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }     
        public string Email { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string ShelterName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Avatar { get; set; }
    }
}
