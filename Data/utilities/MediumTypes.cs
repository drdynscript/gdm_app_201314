using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.utilities
{
    public class MediumTypes
    {
        public static string MediumTypeFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "image/jpeg":
                    return "IMAGE";
                case "image/pjpeg":
                    return "IMAGE";
                case "image/gif":
                    return "IMAGE";
                case "image/png":
                    return "IMAGE";
                default:
                    return "GENERIC";
            }
        }
    }
}
