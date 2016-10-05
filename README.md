# YouTubeAudioExtractormatic
Allows the user to quickly and easily download whole YouTube playlists and convert them to high quality MP3 files. This is a WIP upgrade of my now defunct YouTubePlaylistDownloader repository.

![Preview Screenshot](http://blog.davidmortiboy.com/wp-content/uploads/2016/09/audioExtractor.png)

##Current Features
* Accepts individual video URLs or YouTube playlist URLs
* Automatically fills in the search bar with whatever is copied onto your clipboard
* Save as raw video or choose from a selection of bitrates to convert to
* Cancel in-progress downloads (not in the current GUI)
* Multithreaded capability - 4 simultaneous downloads at a time

##Upcoming Features
* Pause and resume downloads
* Browse videos with a regular search query
* Custom number of threads
* Change download location
* Change GUI skin

##Info for developers
I have designed this software around a MVC (Model View Controller) pattern to allow for easy retheming and reskinning of the frontend interface. Take a look at the ExampleCLI class for a very basic example usage of how this application can even be turned into a console app. Please feel free to branch off and design your own interface, push it back to me, and I'll merge your work in!

##Libraries
* FFMPeg - Licensed under [LGPLv2.1] (http://www.gnu.org/licenses/old-licenses/lgpl-2.1.html)
* libvideo - Licensed under BSD 2-Clause - [read the libvideo license here] (https://github.com/jamesqo/libvideo/blob/master/bsd.license)
