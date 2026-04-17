using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Admin.ThemeManager.ThemeManager;

namespace Admin.ThemeManager
{
    public class ColorPallates
    {
        public ThemeMode PalatteModeName { get; set; }
        public Color PrimaryI { get; set; }
        public Color PrimaryII { get; set; }
        public Color PrimaryIII { get; set; }
        public Color SecondaryI { get; set; }
        public Color SecondaryII { get; set; }
        public Color SecondaryIII { get; set; }
        public Color TextPrimary { get; internal set; }
        public Color TextSecondary { get; internal set; }
        public Color Success { get; internal set; }
        public Color Warning { get; internal set; }
        public Color Danger { get; internal set; }
    }
}
