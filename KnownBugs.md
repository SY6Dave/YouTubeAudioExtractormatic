##Known Bugs
* ~~Shorthand YouTube urls cause exceptions when grabbing download links~~
* GetURI - have to use newer version of libvideo.dll and swap to async geturi method. Done on local machine - will push changes.
* Audio download - file path greater than 160 characters doesn't save and outputs no errors. Maybe warn the user if this is gonna happen and prompt to change download location?
