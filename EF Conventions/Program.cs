using System;
using System.Linq;
using EF_Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

// using IHost host = Host.CreateDefaultBuilder(args).Build();
// await host.RunAsync();

using var ctx = new SampleContext();

var artists = await ctx.EfcArtists.ToListAsync();
Console.WriteLine(string.Join(
    Environment.NewLine,
    artists.Select(a => $"{a.EfcArtistId} - {a.ArtistName}")));

