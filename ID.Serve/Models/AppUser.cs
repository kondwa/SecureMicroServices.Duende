﻿using Microsoft.AspNetCore.Identity;

namespace ID.Serve.Models
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
