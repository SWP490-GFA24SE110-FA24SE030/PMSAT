using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSAT.BusinessTier.Utils
{
    public class ImageUtils
    {
        //check ảnh có hợp lệ hay không và có lớn hơn so với BR
        public static bool ValidationImage(IFormFile file)
        {
            const int MAX_SIZE = 5 * 1024 * 1024; // 10MB
            string[] listExtensions = { ".png", ".jpeg", ".jpg", ".jfif", ".gif", ".webp" };

            if (file == null || file.Length == 0)
            {
                throw new NullReferenceException("Null or empty file");
            }

            // Check file size
            if (file.Length > MAX_SIZE)
            {
                return false; // File size exceeds maximum limit
            }

            // Check file extension
            string extensionFile = Path.GetExtension(file.FileName);
            return listExtensions.Contains(extensionFile, StringComparer.OrdinalIgnoreCase);
        }
    }
}
