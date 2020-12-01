using System;
using System.Collections.Generic;

#nullable disable

namespace MusicManager
{
    public partial class Favourite
    {
        public int TabId { get; set; }
        public int UserId { get; set; }

        public virtual Tab Tab { get; set; }
        public virtual User User { get; set; }
    }
}
