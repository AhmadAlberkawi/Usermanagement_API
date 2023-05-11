using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs
{
    public class UserRegisterDto
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public IFormFile? Photo { get; set; }
        public required string Role { get; set; }
        [StringLength(15, MinimumLength = 4)]
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
