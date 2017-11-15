using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace POS.Helper.PrintHelper
{
    interface IPrintHelper
    {
        FlowDocument CreateDocument();
    }
}
