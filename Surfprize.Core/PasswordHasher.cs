﻿using System;
using System.Linq;
using System.Security.Cryptography;

namespace Surfprize.Core
{
    public static class PasswordHasher
    {
        public static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;
            return !a1.Where((t, i) => t != a2[i]).Any();
        }

        public static string HashPassword(this string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            using (var bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            var dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
                return false;
            if (password == null)
                throw new ArgumentNullException(nameof(password));
            var src = Convert.FromBase64String(hashedPassword);
            if (src.Length != 0x31 || src[0] != 0)
                return false;
            var dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            var buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (var bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArrayCompare(buffer3, buffer4);
        }
    }
}
