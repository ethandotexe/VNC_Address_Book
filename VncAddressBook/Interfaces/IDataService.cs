using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VncAddressBook.Models;

namespace VncAddressBook.Model
{
    public interface IDataService
    {
        Config LoadConfig();
        void SaveConfig(Config config);
        List<Entry> LoadEntries();
        void SaveEntry(Entry entry);
        void DeleteEntry(Entry entry);
        void OpenVncViewer(Entry entry);
    }
}
