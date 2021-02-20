using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace OneGate.Backend.Gateway.Shared.Utils
{
    public class Pbkdf2HashProvider : IHashProvider
    {
        public string Hash(string src)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: src,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
        }
    }
}