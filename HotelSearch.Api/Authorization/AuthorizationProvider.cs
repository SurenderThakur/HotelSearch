using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelSearch.Api.Authorization
{
    public class AuthorizationProvider: OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //go to db
            if (context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, "5"));
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Administrator"));
                context.Validated(identity);
            }
            else if (context.UserName == "guestuser" && context.Password == "guestuser")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "guestuser"));
                identity.AddClaim(new Claim("username", "guestuser"));
                identity.AddClaim(new Claim(ClaimTypes.UserData, "1"));
                identity.AddClaim(new Claim(ClaimTypes.SerialNumber, Guid.NewGuid().ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Guest User"));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "provided username and password is incorrect");
                return;
            }
        }

    }
}