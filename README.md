# SpotifyWrappedChecker [![CodeFactor](https://www.codefactor.io/repository/github/solwynn/SpotifyWrappedChecker/badge)](https://www.codefactor.io/repository/github/solwynn/SpotifyWrappedChecker) 
A program I wrote in a few hours to compare several Spotify playlists. I wrote this to check overlap between multiple years of Spotify Wrapped.
  
# Downloads  
[Download the latest SpotifyWrappedChecker at the Releases page](https://github.com/solwynn/SpotifyWrappedChecker/releases)

# Usage  
SpotifyWrappedChecker is a command-line tool and has 5 command-line options:  
-c, --ClientID (Required) - Your Spotify app's client id, can be found at https://developer.spotify.com/dashboard  
-p, --PlaylistIDs (Required) - A list of playlist IDs to compare against, separated by spaces  
-m, --PlaylistMerge (Optional, Default: false) - Whether to create a combined playlist containing all songs with any overlap between given IDs
-n, --PlaylistName (Optional, Default: "PlaylistComparer") - Name of merged playlist
-d, --PlaylistDesc (Optional, Default: null) - Description of merged playlist -- default is empty

**You NEED a Spotify client app on your developer dashboard for this program to work. That is how you acquire a client ID.**

Comparing three playlists:
```
.\SpotifyWrappedChecker.exe -c 8912398123 -p 4WTOOzIVj3aL1mFpxxxxxx 4K0ywBipbGv2Ggd4xxxxxx 
```  

Merging the overlap between those three playlists:
```
.\SpotifyWrappedChecker.exe -c 8912398123 -p 4WTOOzIVj3aL1mFpxxxxxx 4K0ywBipbGv2Ggd4xxxxxx 4K0yw123bGv2Ggd4xxxxxx -m
```

Merging the overlap between those three playlists, naming it "hi":
```
.\SpotifyWrappedChecker.exe -c 8912398123 -p 4WTOOzIVj3aL1mFpxxxxxx 4K0ywBipbGv2Ggd4xxxxxx 4K0yw123bGv2Ggd4xxxxxx -m -n hi
```

# Setting up a client app on the Spotify dashboard
Go to https://developer.spotify.com/dashboard/create.  
App name can be anything.  
App description can be anything.  
Website can be blank.  
Redirect URIs **MUST** contain: **`http://localhost:5543/callback`** or the software will not work.  
Web API must be checked.  
Your client ID and secret will be at the top of the page.  
![](https://i.imgur.com/t74CfPo.png)



# What is a playlist ID?
`https://open.spotify.com/playlist/`**`4I4bDeaMACVk7m0GJ12345`**`?si=9e07d3f55c12345`  
The second segment in this code block url, bolded \^\^

# Building from Source  
Open the solution and Restore NuGet Packages, then Build as Release.

# License  
This software is released under the MIT software license.