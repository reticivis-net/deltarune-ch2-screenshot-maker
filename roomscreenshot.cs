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
using UndertaleModLib.Models;

using UndertaleModTool;
string exportFolder = PromptChooseDirectory("Export room sizes to where");
if (exportFolder == null)
	throw new System.Exception("The export folder was not set.");
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

int width = 1920;
int height = 1080;
Data.Code.ByName("gml_Object_obj_initializer_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);
Data.Code.ByName("gml_Object_obj_initializer2_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);
Data.Code.ByName("gml_Object_obj_initializer_ch1_Create_0").AppendGML($"surface_resize(application_surface, {width}, {height})", Data);

var code = Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.KeyPress, EventSubtypeKey.vk_f2, Data.Strings, Data.Code, Data.CodeLocals);

code.ReplaceGML(@"
screen_save(((working_directory + ""scs/"" + string(room) + ""_"" + room_get_name(room)) + ""_raw.png""))
room_goto_next()
", Data);

var code2 = Data.GameObjects.ByName("obj_time").EventHandlerFor(EventType.KeyPress, EventSubtypeKey.vk_f1, Data.Strings, Data.Code, Data.CodeLocals);

code2.ReplaceGML(@"
room_goto(room_first)
", Data);

/*code.ReplaceGML(@"
var v_vals = room_get_viewport(room, 0)
surface_resize(application_surface, v_vals[3], v_vals[4])
screen_save(((working_directory + room_get_name(room)) + "".png""))
var file = file_text_open_write(((working_directory + room_get_name(room)) + "".txt""))
file_text_write_real(file, v_vals[3])
file_text_writeln(file)
file_text_write_real(file, v_vals[4])
file_text_writeln(file)
file_text_close(file)
", Data);*/
ChangeSelection(code);
