using System;
using System.Linq;
using EF_Conventions;
using Microsoft.EntityFrameworkCore;

using var ctx = new SampleContext();

var artists = await ctx.Artists
    .Include(a => a.Albums)
    .ToListAsync();
Console.WriteLine(string.Join(
    Environment.NewLine,
    artists.Select(a => $"{a.Id} - {a.ArtistName} - {a.Albums.Count} albums: {string.Join("; ", a.Albums.Select(b => b.Title))}")));
