using SpotifyAPI.Web;
using CommandLine;

namespace SpotifyPlaylistComparer
{
    internal class Program
    {
        public static ArgModel _opts;

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ArgModel>(args)
                .WithParsed(opts =>
                {
                    _opts = opts;

                    ImplicitAuth.Authenticate(opts.ClientId, [
                        Scopes.PlaylistModifyPrivate,
                        Scopes.PlaylistModifyPublic,
                    ], HandleImplicitAuth);

                    Console.WriteLine("Press enter to exit...");
                    Console.ReadLine();
                });
        }

        public static async void HandleImplicitAuth(SpotifyClient client)
        {
            foreach (KeyValuePair<string, Overlaps> kv in await PlaylistComparer.Compare(client, _opts.PlaylistIDs.ToList()))
            {
                if (kv.Value.Playlists.Count > 1)
                {
                    string str = $"{kv.Value.Track.Name} found in playlists: ";

                    foreach (FullPlaylist playlist in kv.Value.Playlists)
                    {
                        str += playlist.Name + ", ";
                    }

                    Console.WriteLine(str);
                }
            }

            if (_opts.PlaylistMerge)
            {
                PlaylistComparer.Create(client, await PlaylistComparer.Compare(client, _opts.PlaylistIDs.ToList()), _opts.PlaylistName, _opts.PlaylistDesc);
            }
        }
    }
}
