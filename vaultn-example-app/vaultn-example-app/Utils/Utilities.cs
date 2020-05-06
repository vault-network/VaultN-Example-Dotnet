using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Microsoft.IdentityModel.Logging;
namespace vaultn_example_app
{
    public class Utilities: IUtilities
    {
        public Utilities()
        {
        }

        public string GenerateToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var cert = new X509Certificate2("vaultn-test.pfx", "1q2w");
            var creds = new X509SigningCredentials(cert);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Self",
                Audience = "VaultN",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "CEC83A59-B82D-4866-8419-BD219CE7AEF2")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }

        public  string GenerateTokenWithCert()
        {
            try
            {
                
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
