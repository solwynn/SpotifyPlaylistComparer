using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace SpotifyPlaylistComparer
{
    public class ArgModel
    {
        [Option('c', "ClientID", HelpText = "Your Spotify app's client id, can be found at https://developer.spotify.com/dashboard", Required = true)]
        public required string ClientId { get; set; }

        [Option('p', "PlaylistIDs", HelpText = "A list of playlist IDs to compare against, separated by spaces", Required = true)]
        public required IEnumerable<string> PlaylistIDs { get; set; }

        //[Option('s', "ClientSecret", Default = null, HelpText = "Your Spotify app's client secret, not necessary for all operations", Required = false)]
        //public string? ClientSecret { get; set; }

        [Option('m', "PlaylistMerge", Default = false, HelpText = "Whether to create a combined playlist containing all songs with any overlap between given IDs", Required = false)]
        public bool PlaylistMerge { get; set; }

        [Option('n', "PlaylistName", Default = "PlaylistComparer", HelpText = "Name of merged playlist -- default is \"PlaylistComparer\"", Required = false)]
        public string PlaylistName { get; set; }

        [Option('d', "PlaylistDesc", Default = null, HelpText = "Description of merged playlist -- default is empty", Required = false)]
        public string? PlaylistDesc { get; set; }
    }
}