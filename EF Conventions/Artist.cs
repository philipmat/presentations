using System;
using System.Collections.Generic;

#nullable disable

namespace EF_Conventions
{
    public partial class Artist
    {
        public Artist()
        {
            Albums = new HashSet<Album>();
        }

        public long Id { get; set; }
        public string ArtistName { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
