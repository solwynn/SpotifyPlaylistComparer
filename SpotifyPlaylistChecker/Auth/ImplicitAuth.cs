using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

namespace SpotifyPlaylistComparer
{
    public static class ImplicitAuth
    {
        private static readonly EmbedIOAuthServer server = new EmbedIOAuthServer(new Uri("http://localhost:5543/callback"), 5543);
        private static Action<SpotifyClient>? _func;


        public static void Authenticate(string clientId, List<string> scopes, Action<SpotifyClient> func)
        {
            _func = func;

            server.Start().Wait();
            server.ImplictGrantReceived += OnImplicitGrantReceived;
            server.ErrorReceived += OnErrorReceived;

            LoginRequest request = new LoginRequest(server.BaseUri, clientId, LoginRequest.ResponseType.Token)
            {
                Scope = scopes  
            };

            BrowserUtil.Open(request.ToUri());
        }
        private static async Task OnImplicitGrantReceived(object sender, ImplictGrantResponse response)
        {
            await server.Stop();

            SpotifyClient client = new SpotifyClient(response.AccessToken);

            _func?.Invoke(client);
        }

        private static async Task OnErrorReceived(object sender, string error, string? state)
        {
            Console.WriteLine($"Aborting authorization, error received: {error}");
            await server.Stop();
        }
    }
}
