using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EastWood.Utilities
{
    public class FileHelper
    {
        public static string GetConvertedFileSize(double Size)
        {
            string result = "0 bytes";
            if (Size != 0)
            {
                if ((Size / 1024) < 1)
                {
                    result = Size.ToString(".0") + " bytes";
                }
                else if ((Size / 1048576) < 1)
                {
                    result = (Size / 1024F).ToString(".0") + " KB";
                }
                else if ((Size / 1073741824) < 1)
                {
                    result = (Size / 1048576F).ToString(".0") + " MB";
                }
                else if ((Size / 1099511627776) < 1)
                {
                    result = (Size / 1073741824F).ToString(".0") + " GB";
                }
                else if ((Size / 1099511627776000) < 1)
                {
                    result = (Size / 1099511627776F).ToString(".0") + " TB";
                }
            }
            return result;
        }
    }
}