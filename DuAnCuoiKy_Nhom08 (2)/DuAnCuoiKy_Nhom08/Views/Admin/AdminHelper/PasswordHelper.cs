using System;
using System.Security.Cryptography;

namespace DuAnCuoiKy_Nhom08.Helpers
{
    /// <summary>
    /// Băm / kiểm tra mật khẩu bằng PBKDF2-HMAC-SHA256.
    /// Định dạng lưu trong cột Admins.PasswordHash: PBKDF2$iterations$salt(base64)$hash(base64)
    /// </summary>
    public static class PasswordHelper
    {
        private const string Prefix = "PBKDF2";
        private const int Iterations = 100000;
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static string Hash(string password)
        {
            var salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var hash = Derive(password, salt, Iterations);
            return string.Concat(Prefix, "$", Iterations, "$", Convert.ToBase64String(salt), "$", Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Kiểm tra mật khẩu. Nếu cột PasswordHash đang là mật khẩu thô (dữ liệu cũ do nhóm tự nhập)
        /// thì vẫn cho đăng nhập và báo needsUpgrade = true để controller băm lại.
        /// </summary>
        public static bool Verify(string password, string stored, out bool needsUpgrade)
        {
            needsUpgrade = false;
            if (password == null || string.IsNullOrEmpty(stored)) return false;

            if (stored.StartsWith(Prefix + "$", StringComparison.Ordinal))
            {
                var parts = stored.Split('$');
                int iterations;
                if (parts.Length != 4 || !int.TryParse(parts[1], out iterations) || iterations < 1) return false;
                try
                {
                    var salt = Convert.FromBase64String(parts[2]);
                    var expected = Convert.FromBase64String(parts[3]);
                    var actual = Derive(password, salt, iterations);
                    return FixedTimeEquals(expected, actual);
                }
                catch (FormatException)
                {
                    return false;
                }
            }

            // Dữ liệu cũ: mật khẩu lưu dạng thô
            var ok = FixedTimeEquals(System.Text.Encoding.UTF8.GetBytes(stored), System.Text.Encoding.UTF8.GetBytes(password));
            needsUpgrade = ok;
            return ok;
        }

        /// <summary>Mật khẩu mới: >= 8 ký tự, có chữ hoa, chữ thường, số và ký tự đặc biệt (giống form đăng ký).</summary>
        public static bool IsStrong(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8) return false;
            bool upper = false, lower = false, digit = false, special = false;
            foreach (var c in password)
            {
                if (c >= 'A' && c <= 'Z') upper = true;
                else if (c >= 'a' && c <= 'z') lower = true;
                else if (c >= '0' && c <= '9') digit = true;
                else special = true;
            }
            return upper && lower && digit && special;
        }

        private static byte[] Derive(string password, byte[] salt, int iterations)
        {
            using (var kdf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(HashSize);
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            var diff = a.Length ^ b.Length;
            for (var i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }
}
