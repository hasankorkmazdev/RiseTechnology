﻿namespace RiseTechnology.Contact.API.Models
{
    public class CreatePerson
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
    }
}
