using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UndertaleModLib.Util;

using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UndertaleModLib;


string exportFolder = PromptChooseDirectory("Export room sizes to where");
if (exportFolder == null)
	throw new System.Exception("The export folder was not set.");
// resize all rooms to have the entire room in view
using (StreamWriter writer = new StreamWriter(exportFolder + System.IO.Path.DirectorySeparatorChar + "roomsizes.csv"))
{
	for (int i = 0; i < Data.Rooms.Count; i++)
	{
		writer.WriteLine($"{Data.Rooms[i].Width},{Data.Rooms[i].Height}");
		for (var j = 0; j < Data.Rooms[i].Views.Count; j++)
		{
			Data.Rooms[i].Views[j].ViewHeight = (int)Data.Rooms[i].Height;
			Data.Rooms[i].Views[j].PortHeight = (int)Data.Rooms[i].Height;
			Data.Rooms[i].Views[j].BorderY = 0;
			Data.Rooms[i].Views[j].ViewWidth = (int)Data.Rooms[i].Width;
			Data.Rooms[i].Views[j].PortWidth = (int)Data.Rooms[i].Width;
			Data.Rooms[i].Views[j].BorderX = 0;
		}
	}
}

// force the camera to be Big
// if your monitor is a different ratio, make it that ratio! resolutions bigger than your monitor wont do anything.
int width = 1920*4;
int height = 1080*4;
Data.Code.ByName("gml_Object_obj_initializer_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);
Data.Code.ByName("gml_Object_obj_initializer2_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);
Data.Code.ByName("gml_Object_obj_initializer_ch1_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);

// make player and party invisible
Data.GameObjects.ByName("obj_mainchara").Visible = false;
Data.GameObjects.ByName("obj_caterpillarchara").Visible = false;

// f2 keybind to save screenshot and advance
var code = Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.KeyPress, EventSubtypeKey.vk_f2, Data.Strings, Data.Code, Data.CodeLocals);
code.ReplaceGML(@"
screen_save(((working_directory + ""scs/"" + string(room) + ""_"" + room_get_name(room)) + ""_raw.png""))
room_goto_next()
", Data);

ChangeSelection(code);
