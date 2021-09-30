# deltarune chapter 2 screenshot maker

Steps to use
- rename `roomscreenshot.cs` to anything ending in `.csx` and put it in one of the plugin folders of [UndertaleModTool](https://github.com/krzys-h/UndertaleModTool)
- run the plugin
  - this will generate a csv wherever you want
- save modified data.win
- make a `scs` folder in `C:\Users\[YOURUSERNAME]\AppData\Local\DELTARUNE`
- run DELTARUNE in debug mode using [YYToolkit](https://github.com/Archie-osu/YYToolkit)
  - this step is techniucally optional but it allows you to jump around rooms which is needed because many rooms are bugged
- select chapter 2
  - the game will appear distorted, it is being resized to use as much pixel data as it can.
  - this is configurable on line 53-54 of `roomscreenshot.cs`
- jump to room 27 (with `YYToolkit` the keybind is `F3`)
  - do NOT load a save. The code to make the characters (mostly) invisible breaks it. not sure why. just go to the debug room or the load menu and use `F3` to jump.
- press `F2` to take a screenshot and advance to the next room
  - if a room is bugged, relaunch and skip over it with `F3`
  - screenshots will be saved to `C:\Users\[YOURUSERNAME]\AppData\Local\DELTARUNE\scs`
- copy `roomsizes.csv` and `scs` to the same folder as `main.py`
- make an `out` folder in the same directory
- run `main.py`
- `out` will now contained resized screenshots
  - for any rooms bigger than or close to the size of your monitor, the result might not look great.