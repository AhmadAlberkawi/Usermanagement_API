using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs
{
    public class UserLoginDto
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
