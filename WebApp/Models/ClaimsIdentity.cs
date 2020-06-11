using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ClaimsIdentity : System.Security.Principal.IIdentity
    {
        private List<Claim> claims;
        private string authenticationScheme;

        public ClaimsIdentity(List<Claim> claims, string authenticationScheme)
        {
            this.claims = claims;
            this.authenticationScheme = authenticationScheme;
        }

        public string AuthenticationType { get; }

        public bool IsAuthenticated { get; }

        public string Name { get; }

   

    }
}
