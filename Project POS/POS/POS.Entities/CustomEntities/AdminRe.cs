using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Entities
{
    public enum AdminReRole
    {
        SoftwareAd = 1,
        AsowelAd = 2,
        AdPressAd = 3,
        HigherAd = 4
    }

    public partial class AdminRe
    {
        public int AdRole { get; set; } // ad_role
        public string DecryptedPass { get; set; }
    }
}
