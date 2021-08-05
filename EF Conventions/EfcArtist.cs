using System;
using System.Collections.Generic;

#nullable disable

namespace EF_Conventions
{
    public partial class EfcArtist
    {
        public EfcArtist()
        {
            EfcAlbums = new HashSet<EfcAlbum>();
        }

        public long EfcArtistId { get; set; }
        public string ArtistName { get; set; }

        public virtual ICollection<EfcAlbum> EfcAlbums { get; set; }
    }
}
