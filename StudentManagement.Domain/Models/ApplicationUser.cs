﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {

        [MaxLength(30)]
        public string? Name { get; set; }
    }
}
