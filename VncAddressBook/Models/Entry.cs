using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VncAddressBook.Models
{
    public class Entry
    {
        public string Id { get; set; }
        public string Name { get; set; }        // From filename
        public string Host { get; set; }
        public string Port { get; set; } = "5900";  // TightVNC only - RealVNC append non-standard port to "Host" field (192.168.10.27:9065)
        public string Username { get; set; }    // RealVNC only
        public string Password { get; set; }
        public string Scaling { get; set; } = "AspectFit";      // RealVNC only
        public string Comment { get; set; } = "Add comments here";  // Not from standard file
        public string FullScreen { get; set; } = "0";
        public string FitWindow { get; set; } = "1";            // TightVNC only
        public string ScaleDen { get; set; } = "1";             // TightVNC only
        public string ScaleNum { get; set; } = "1";             // TightVNC only
    }
}







/* REALVNC Scaling key
No Scaling -> Scaling=None
Scale to window size -> Scaling=AspectFit
100% x 100% -> Scaling="100%x100%"
*/

/* TIGHTVNC ScaleDen/ScaleNum key
% -> NUM/DEN
25% -> 1/4
50% -> 1/2
75% -> 3/4
90% -> 9/10
100% -> 1/1
125% -> 5/4
150% -> 3/2
OR AUTO via the "fitwindow" property set to 1; num and den set to 1/1 and/or ignored
OR any user-defined whole number within a range (percentage converts to fractional 2 values)
*/

/*
public string EnableToolbar { get; set; } = "1";        // RealVNC only
public string EnableRemotePrinting { get; set; } = "0";         // RealVNC only
public string ChangeServerDefaultPrinter { get; set; } = "0";   // RealVNC only
public string ShareFiles { get; set; } = "1";           // RealVNC only
public string EnableChat { get; set; } = "1";           // RealVNC only
public string PreferredEncoding { get; set; } = "ZRLE"; // RealVNC only
public string ServerCutText { get; set; } = "1";        // RealVNC only
public string ClientCutText { get; set; } = "1";        // RealVNC only
public string SendKeyEvents { get; set; } = "1";        // RealVNC only
public string SendPointerEvents { get; set; } = "1";    // RealVNC only
public string AutoSelect { get; set; } = "1";           // RealVNC only
public string ColorLevel { get; set; } = "full";        // RealVNC only
public string FullColor { get; set; } = "1";            // RealVNC only
public string Shared { get; set; } = "1";
public string UseEncoding { get; set; } = "1";          // TightVNC only
public string CopyRect { get; set; } = "1";             // TightVNC only
public string ViewOnly { get; set; } = "0";             // TightVNC only
public string EightBit { get; set; } = "0";             // TightVNC only
public string BellDeiconify { get; set; } = "0";        // TightVNC only
public string DisableClipboard { get; set; } = "0";     // TightVNC only
public string SwapMouse { get; set; } = "0";            // TightVNC only
public string CursorShape { get; set; } = "1";          // TightVNC only
public string NoRemoteCursor { get; set; } = "0";       // TightVNC only
public string Preferred_Encoding { get; set; } = "7";   // TightVNC only
public string CompressLevel { get; set; } = "-1";       // TightVNC only
public string Quality { get; set; } = "6";              // TightVNC only
public string LocalCursor { get; set; } = "1";          // TightVNC only
public string LocalCursorShape { get; set; } = "1";     // TightVNC only
*/

/*
TIGHTVNC EXAMPLE
[connection]
host=192.168.70.10
port=5900
password=70685069802a5e92
[options]
use_encoding_1 = 1
copyrect=1
viewonly=0
fullscreen=0
8bit=0
shared=1
belldeiconify=0
disableclipboard=0
swapmouse=0
fitwindow=0 // AUTO on = 1, off = 0
cursorshape=1
noremotecursor=0
preferred_encoding=7
compresslevel=-1
quality=6
localcursor=1
scale_den=1 // SCALING DENOMINATOR
scale_num=4 // SCALING NUMERATOR
local_cursor_shape=1
*/

/*
REALVNC EXAMPLE
[Connection]
Host=192.168.70.10
Password=f39e29c47ba01122
[Options]
EnableToolbar=1
EnableRemotePrinting=0
ChangeServerDefaultPrinter=0
ShareFiles=1
EnableChat=1
PreferredEncoding=ZRLE
UserName=bdavis
Scaling=100%x100%, 1920x1080, 1920x, Fit, None
ServerCutText=1
ClientCutText=1
SendKeyEvents=1
SendPointerEvents=1
Shared=1
AutoSelect=1 // Adapt to Network Speed? 
ColorLevel=pal8
FullColor=0
FullScreen=0
*/
