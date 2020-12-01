﻿using System;
using System.Collections.Generic;

#nullable disable

namespace MusicManager
{
    public partial class User
    {
        public User()
        {
            Tabs = new HashSet<Tab>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Tab> Tabs { get; set; }
    }
}
