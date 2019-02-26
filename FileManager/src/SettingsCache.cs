using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public interface ISettingsCache
    {
        string LastDriverNameOne();

        string LastDriverNameTwo();

        List<string> LastPathOne();

        List<string> LastPathTwo();

        void SaveLastDriverNameOne(string name);

        void SaveLastDriverNameTwo(string name);

        void SaveLastPathOne(List<string> pathArray);

        void SaveLastPathTwo(List<string> pathArray);

        int Width { get; set; }

        int Height { get; set; }

        int XPosition { get; set; }

        int YPosition { get; set; }

    }

    public interface ISettingCache
    {
        Func<string> LastDriverName { get; set; }

        Func<List<string>> LastPath { get; set; }

        Action<string> SaveLastDriverName { get; set; }

        Action<List<string>> SaveLastPath { get; set; }
    }

    public class SettingCacheImpl : ISettingCache
    {
        public Func<string> LastDriverName { get; set; }

        public Func<List<string>> LastPath { get; set; }

        public Action<string> SaveLastDriverName { get; set; }

        public Action<List<string>> SaveLastPath { get; set; }
    }

    public class SettingCacheFakeImpl : SettingCacheImpl
    {
        public SettingCacheFakeImpl()
        {
            LastDriverName = new Func<string>(() => "");

            LastPath = new Func<List<string>>(() => new List<string>());

            SaveLastDriverName = new Action<string>((name) => { });

            SaveLastPath = new Action<List<string>>((list) => { });
        }
    }
}