using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace POS.Helper.PrintHelper.Report
{
    public class AppPath
    {
        public static string ApplicationPath
        {
            get
            {
                if (isInWeb)
                    return HttpRuntime.AppDomainAppPath;

                return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            }
        }

        private static bool isInWeb
        {
            get
            {
                return HttpContext.Current != null;
            }
        }
    }
}
