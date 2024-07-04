using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Models
{
    public class AuthConfig
    {
        public readonly string _secretKey;
        public readonly string _issuer;
        public readonly string _audience;
        public AuthConfig(string secretKey, string issuer, string audience)
        {
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }
    }
}
