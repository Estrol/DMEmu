# DPEmu
## Documentations
#### Packet structures
Client and server sending the following packet structures
```
[length] [packetID] [data]
```
* length: The first 2 byte represent packet length like `0x04, 0x00` which 4 byte length.
* packetID: second 2 byte represent packet id to handle like `0xfff0` which disconnect packetID.
* data: and last packet until length was just a data.

#### SPT Files
The server will tried to load the following spt files:
```
LauncherSettings.spt
Channel1.spt
MusicList.spt
D007.spt
D207.spt
```
Explaination:
* LauncherSettings.spt - Just addition of mine for storing in-game name, webport, and gameport
* Channel1.spt - Just storing packet of list available room channels
* MusicList.spt - Storing what server send about OJNList.dat to match the song files
* D007.spt - Storing username and Character ID
* D207.spt - Storing available room to join (Yeah this is singleplayer server so it must be empty)

## Changelog
#### Sunday, August 2 2020 (GMT+7)
* Add new length character nickname from 5 to 12 (can't more than that since it will shorter by the game)
* Fixed max buffer size in SocketServer.cs
* Fixed character level changed from level 100 to level 30 after a song play in packet handler ID: `0x0fb5`

#### Initial open source, Friday, August 1 2020 (GMT+7)
* Change from closed source to open source with MIT License
