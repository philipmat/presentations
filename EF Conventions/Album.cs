using System;
using System.Collections.Generic;

#nullable disable

namespace EF_Conventions
{
    public partial class Album
    {
        public long Id { get; set; }
        public long? ArtistId { get; set; }
        public string Title { get; set; }

        public virtual Artist Artist { get; set; }
    }
}
