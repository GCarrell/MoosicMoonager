using System;
using System.Collections.Generic;

#nullable disable

namespace MusicManager
{
    public partial class Tab
    {
        public int TabId { get; set; }
        public string TabName { get; set; }
        public string BandName { get; set; }
        public string Instrument { get; set; }
        public string TabUrl { get; set; }
        public int TabCreator { get; set; }

        public virtual User TabCreatorNavigation { get; set; }

        public override string ToString()
        {
            return $"{TabName} {Instrument}";
        }
    }
}
