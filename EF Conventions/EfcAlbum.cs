using System;
using System.Collections.Generic;

#nullable disable

namespace EF_Conventions
{
    public partial class EfcAlbum
    {
        public long EfcAlbumId { get; set; }
        public long? EfcArtistId { get; set; }
        public string Title { get; set; }

        public virtual EfcArtist EfcArtist { get; set; }
    }
}
