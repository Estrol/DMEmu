# DerpSamoJam Singleplayer Server Emulator
A O2-JAM Server Emulator for [DMJam](http://dpjam.net) 
* **Author**: Estrol
* **Email**: support@entrosbot.xyz
* **Version**: alpha-0.5.1

## Server Completion Status
The current completion server emulator:

| State Event | Status |
| -------- | --------|
| STATE_LOGIN | Finished |
| STATE_PLANET | Finished |
| STATE_LOBBY | Partical Finished (95%) | 
| STATE_WAITING | Partical Finished (60%) | 
| STATE_PLAYING | Partical Finished (50%) |
| STATE_FINISH | Broken (0%) |

Partical finished/Broken explained:
* STATE_LOBBY: Back to channel list broken, and client sending weird packets
* STATE_WAITING: Back to lobby, and some untested packets
* STATE_PLAYING: Some untested packets
* STATE_FINISH: Client sending weird packets on song finish, must use exit button to finish the song.

## Runtime requirements
This server will look into these files to make function like normal server:

| Files  | Use for |
| -------- | --------|
| D007.spt | User's character |
| D207.spt | Room list/Login info |
| MusicList.spt | Music list from server | 
| Channel1.spt | List available channels on a planet | 
| LauncherSettings.spt | User config in DMEmu |


## Build requirements
* Visual Studio 2019
* .NET 4.5+

## Building from source
* Open DMEmu.sln in Visual Studio 2019
* Click Build to build the Server Emulator

## Credits
* [djask](https://github.com/djask) - This based on his/her [o2jam-server-emu](https://github.com/djask/o2jam-server-emu)

## License
This software licensed under the [MIT License](License)