using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SpotifyAPI.Web;
using Swan;
using Microsoft.VisualBasic;

namespace SpotifyPlaylistComparer
{
    public class Overlaps(FullTrack track, List<FullPlaylist> playlists)
    {
        public FullTrack Track { get; set; } = track;
        public List<FullPlaylist> Playlists { get; set; } = playlists;
    }

    public static class PlaylistComparer
    {
        public static async Task<Dictionary<string, Overlaps>> Compare(SpotifyClient client, List<string> playlistIDs)
        {
            Dictionary<string, Overlaps> overlapDict = [];

            foreach (string playlistId in playlistIDs)
            {
                FullPlaylist playlist = await client.Playlists.Get(playlistId);

                if (playlist.Tracks?.Items == null) continue;

                foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items)
                {
                    if (item.Track is FullTrack track)
                    {
                        if (overlapDict.TryGetValue(track.Id, out Overlaps? overlaps))
                        {
                            overlaps.Playlists.Add(playlist);
                        }
                        else
                        {
                            overlapDict.Add(track.Id, new Overlaps(track, [playlist]));
                        }
                    }
                }
            }

            return overlapDict;
        }

        // I've decided that it's much easier to combine any overlaps
        public static async void Create(SpotifyClient client, Dictionary<string, Overlaps> overlapDict, string name = "PlaylistComparer", string? description = null)
        {
            PlaylistCreateRequest pcr = new(name);
            pcr.Description = description;

            PrivateUser privateUser = await client.UserProfile.Current();

            FullPlaylist newPlaylist = await client.Playlists.Create(privateUser.Id, pcr);

            if (newPlaylist == null || newPlaylist.Id == null) return;

            List<string> songIds = new List<string>();
            
            foreach (KeyValuePair<string, Overlaps> kv in overlapDict)
            {
                if (kv.Value.Playlists.Count > 1)
                {
                    songIds.Add(kv.Value.Track.Uri);
                }
            }

            // Spotify enforces 100 songs per batch
            IEnumerable<string[]> batches = songIds.Chunk(100);

            foreach (string[] batch in batches)
            {
                PlaylistAddItemsRequest pair = new PlaylistAddItemsRequest(batch);
                await client.Playlists.AddItems(newPlaylist.Id, pair);
            }
        }
    }
}
