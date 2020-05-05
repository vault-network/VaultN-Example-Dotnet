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
            RsaPrivateCrtKeyParameters keyPair;
            
            using (var reader = File.OpenText("new-private.key"))
            {
                var pemreader = new PemReader(reader);
                var temp  = pemreader.ReadObject();
                keyPair = (RsaPrivateCrtKeyParameters)temp;
            }
            

            var keyParams = DotNetUtilities.ToRSAParameters(keyPair);
            
            var key = new RsaSecurityKey(keyParams);
            
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Issuer",
                Audience = "Audience",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, "test")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }
    }
}
