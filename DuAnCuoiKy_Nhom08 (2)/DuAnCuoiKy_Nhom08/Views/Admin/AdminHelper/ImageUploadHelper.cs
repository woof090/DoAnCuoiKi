using System;
using System.IO;
using System.Web;

namespace DuAnCuoiKy_Nhom08.Helpers
{
    /// <summary>Lưu ảnh upload (thumbnail blog, avatar) vào thư mục trong dự án và xóa ảnh cũ an toàn.</summary>
    public static class ImageUploadHelper
    {
        private const int MaxBytes = 2 * 1024 * 1024; // 2 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        /// <summary>
        /// Lưu file vào virtualFolder (vd "~/Images/Blog"). Trả về đường dẫn dạng "~/Images/Blog/xxxx.jpg".
        /// Trả về null và gán error nếu file không hợp lệ.
        /// </summary>
        public static string Save(HttpPostedFileBase file, HttpServerUtilityBase server, string virtualFolder, out string error)
        {
            error = null;
            var ext = Path.GetExtension(file.FileName ?? "");
            ext = (ext ?? "").ToLowerInvariant();

            if (Array.IndexOf(AllowedExtensions, ext) < 0)
            {
                error = "Chỉ chấp nhận ảnh .jpg, .jpeg, .png, .gif hoặc .webp.";
                return null;
            }
            if (file.ContentLength <= 0 || file.ContentLength > MaxBytes)
            {
                error = "Ảnh phải nhỏ hơn 2 MB.";
                return null;
            }

            var header = new byte[12];
            var read = file.InputStream.Read(header, 0, header.Length);
            file.InputStream.Position = 0;
            if (read < 12 || !LooksLikeImage(header))
            {
                error = "File tải lên không phải ảnh hợp lệ.";
                return null;
            }

            var physicalFolder = server.MapPath(virtualFolder);
            Directory.CreateDirectory(physicalFolder);
            var fileName = Guid.NewGuid().ToString("N") + (ext == ".jpeg" ? ".jpg" : ext);
            file.SaveAs(Path.Combine(physicalFolder, fileName));
            return virtualFolder.TrimEnd('/') + "/" + fileName;
        }

        /// <summary>Xóa ảnh đã lưu, chỉ khi nó nằm trong đúng thư mục virtualFolder.</summary>
        public static void Delete(string storedPath, HttpServerUtilityBase server, string virtualFolder)
        {
            if (string.IsNullOrEmpty(storedPath)) return;
            if (!storedPath.StartsWith(virtualFolder.TrimEnd('/') + "/", StringComparison.OrdinalIgnoreCase)) return;
            try
            {
                var folder = Path.GetFullPath(server.MapPath(virtualFolder));
                var full = Path.GetFullPath(server.MapPath(storedPath));
                if (full.StartsWith(folder, StringComparison.OrdinalIgnoreCase) && File.Exists(full))
                {
                    File.Delete(full);
                }
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }

        private static bool LooksLikeImage(byte[] h)
        {
            // JPEG
            if (h[0] == 0xFF && h[1] == 0xD8 && h[2] == 0xFF) return true;
            // PNG
            if (h[0] == 0x89 && h[1] == 0x50 && h[2] == 0x4E && h[3] == 0x47) return true;
            // GIF
            if (h[0] == 0x47 && h[1] == 0x49 && h[2] == 0x46 && h[3] == 0x38) return true;
            // WEBP: "RIFF" .... "WEBP"
            if (h[0] == 0x52 && h[1] == 0x49 && h[2] == 0x46 && h[3] == 0x46 &&
                h[8] == 0x57 && h[9] == 0x45 && h[10] == 0x42 && h[11] == 0x50) return true;
            return false;
        }
    }
}
