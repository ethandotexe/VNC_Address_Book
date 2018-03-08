using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Text;
using VncAddressBook.Models;

namespace VncAddressBook.Model
{
    public class DataService : IDataService
    {
        readonly string vncEntriesPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\XProduct\VNC Address Book\";

        public void SaveConfig(Config currentConfig)
        {
            using (var stringWriter = new Utf8StringWriter())
            {
                string json = JsonConvert.SerializeObject(currentConfig);
                stringWriter.Write(json);
                string jsonText = stringWriter.ToString();
                File.WriteAllText(vncEntriesPath + @"\Configuration\ProgramConfig.config", jsonText);
            }
        }
        
        public Config LoadConfig()
        {
            string configFile = vncEntriesPath + @"\Configuration\ProgramConfig.config";
            Config storedConfig = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configFile));

            return storedConfig;
        }

        public void SaveEntry(Entry entry)
        {
            Config currentConfig = LoadConfig();
            try
            {
                using (var stringWriter = new Utf8StringWriter())
                {
                    stringWriter.WriteLine("[connection]");
                    if (currentConfig.UsingRealVnc == true && entry.Port != "5900")
                    {
                        stringWriter.WriteLine("host=" + entry.Host + ":" + entry.Port);
                    }
                    else
                    {
                        stringWriter.WriteLine("host=" + entry.Host);
                    }
                    stringWriter.WriteLine("port=" + entry.Port);
                    stringWriter.WriteLine("password=" + entry.Password);
                    stringWriter.WriteLine("[options]");
                    stringWriter.WriteLine("username=" + entry.Username);
                    stringWriter.WriteLine("scaling=" + entry.Scaling);
                    stringWriter.WriteLine("fullscreen=" + entry.FullScreen);
                    stringWriter.WriteLine("fitwindow=" + entry.FitWindow);
                    stringWriter.WriteLine("scale_den=" + entry.ScaleDen);
                    stringWriter.WriteLine("scale_num=" + entry.ScaleNum);
                    string vncText = stringWriter.ToString(); // Text to save in .vnc file
                    File.WriteAllText(vncEntriesPath + entry.Name + ".vnc", vncText);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: SaveEntry() failed.", e.ToString());
            }
        }

        public List<Entry> LoadEntries()
        {
            List<Entry> vncEntries = new List<Entry>();
            string line;
            string lineData;
            int position;
            
            try
            {
                var vncFiles = Directory.EnumerateFiles(vncEntriesPath, "*.vnc", SearchOption.AllDirectories);
                foreach (string currentFile in vncFiles)
                {
                    using (StreamReader file = new StreamReader(currentFile))
                    {
                        Entry loadResult = new Entry();
                        while ((line = file.ReadLine()) != null)
                        {
                            System.Console.WriteLine(line);
                            if (line.StartsWith("["))
                            {
                                continue;
                            }
                            position = line.IndexOf("=");
                            if (position < 0)
                            {
                                continue;
                            }
                            lineData = line.Substring(position + 1);
                            lineData = lineData.Trim();
                            if (line.StartsWith("host", true, null))
                            {
                                loadResult.Host = lineData;
                            }
                            else if (line.StartsWith("port", true, null))
                            {
                                loadResult.Port = lineData;
                            }
                            else if (line.StartsWith("password", true, null))
                            {
                                loadResult.Password = lineData;
                            }
                            else if (line.StartsWith("username", true, null))
                            {
                                loadResult.Username = lineData;
                            }
                            else if (line.StartsWith("fullscreen", true, null))
                            {
                                loadResult.FullScreen = lineData;
                            }
                            else if (line.StartsWith("fitwindow", true, null))
                            {
                                loadResult.FitWindow = lineData;
                            }
                            else if (line.StartsWith("scale_den", true, null))
                            {
                                loadResult.ScaleDen = lineData;
                            }
                            else if (line.StartsWith("scale_num", true, null))
                            {
                                loadResult.ScaleNum = lineData;
                            }
                        }
                        loadResult.Name = Path.GetFileNameWithoutExtension(currentFile);
                        vncEntries.Add(loadResult);
                    }
                }
            }
            catch (FileNotFoundException f)
            {
                Console.WriteLine("Exception: A file was not found - LoadEntries().", f.ToString());
            }
            catch (DirectoryNotFoundException d)
            {
                Console.WriteLine("Exception: Directory not found - LoadEntries().", d.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: LoadEntries() failed.", e.ToString());
            }

            return vncEntries;
        }

        public void OpenVncViewer(Entry entry)
        {
            Config currentConfig = LoadConfig();
            if (currentConfig.UsingTightVnc == true)
            {
                string tightVncPath = GetTightVncPath();
                if (tightVncPath != "invalidpath")
                {
                    System.Diagnostics.Process.Start(tightVncPath, " -optionsfile = " + vncEntriesPath + entry.Name + ".vnc");
                }
                else
                {
                    string message = "TightVNC program file(s) not found. Possible issue with TightVNC installation.";
                    string caption = "TightVNC Not Found";
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    // Displays the MessageBox.
                    MessageBoxResult result = MessageBox.Show(message, caption, buttons, MessageBoxImage.Error);
                }
            }
            else if (currentConfig.UsingRealVnc == true)
            {
                string realVncPath = GetRealVncPath();
                if (realVncPath != "invalidpath")
                {
                    System.Diagnostics.Process.Start(realVncPath, " -config " + vncEntriesPath + entry.Name + ".vnc");
                }
                else
                {
                    string message = "RealVNC program file(s) not found. Possible issue with RealVNC installation.";
                    string caption = "RealVNC Not Found";
                    MessageBoxButton buttons = MessageBoxButton.OK;
                    // Displays the MessageBox.
                    MessageBoxResult result = MessageBox.Show(message, caption, buttons, MessageBoxImage.Error);
                }
            }
        }

        public string GetTightVncPath()
        {
            string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\TightVNC\";
            string progFilesX86 = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\TightVNC\";
            string tightVncPath;
            if (Directory.Exists(progFiles) && File.Exists(progFiles + @"tvnviewer.exe"))
            {
                tightVncPath = progFiles + @"tvnviewer.exe";
            }
            else if (Directory.Exists(progFilesX86) && File.Exists(progFilesX86 + @"tvnviewer.exe"))
            {
                tightVncPath = progFilesX86 + @"tvnviewer.exe";
            }
            else
            {
                tightVncPath = "invalidpath";
            }

            return tightVncPath;
        }

        public string GetRealVncPath()
        {
            string progFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\RealVNC\VNC Viewer";
            string realVncPath;
            if (Directory.Exists(progFiles) && File.Exists(progFiles + @"vncviewer.exe"))
            {
                realVncPath = progFiles + @"vncviewer.exe";
            }
            else
            {
                realVncPath = "invalidpath";
            }

            return realVncPath;
        }
    }

    #region Helper Methods

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }

    #endregion
}