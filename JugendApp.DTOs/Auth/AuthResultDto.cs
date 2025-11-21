using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JugendApp.DTOs.Auth
{
    public class AuthResultDto
    {
        public string Token { get; set; } = "";
        public int UserId { get; set; }
        public int PersonId { get; set; }
        public string Username { get; set; } = "";
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
    }


}
