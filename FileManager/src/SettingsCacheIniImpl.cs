using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class SettingsCacheIniImpl : ISettingsCache
    {
        private string IniFileName = "settings.ini";

        private IniData data;
        private FileIniDataParser parser = new FileIniDataParser();
        private string section = "settings";
        //LastPathOne
        //LastPathTwo
        //LastDriveNameOne
        //LastDriveNameTwo

        public int Width
        {
            get
            {
                string width = data[section]["Width"];
                if (int.TryParse(width, out int value))
                {
                    return value;
                }
                return 0;
            }
            set
            {
                data[section]["Width"] = value.ToString(); SaveFile();
            }
        }

        public int Height
        {
            get
            {
                string height = data[section]["Height"];
                if (int.TryParse(height, out int value))
                {
                    return value;
                }
                return 0;
            }
            set
            {
                data[section]["Height"] = value.ToString(); SaveFile();
            }
        }

        public int XPosition
        {
            get
            {
                string xPosition = data[section]["XPosition"];
                if (int.TryParse(xPosition, out int value))
                {
                    return value;
                }
                return 0;
            }
            set
            {
                data[section]["XPosition"] = value.ToString(); SaveFile();
            }
        }

        public int YPosition
        {
            get
            {
                string yPosition = data[section]["YPosition"];
                if (int.TryParse(yPosition, out int value))
                {
                    return value;
                }
                return 0;
            }
            set
            {
                data[section]["YPosition"] = value.ToString(); SaveFile();
            }
        }

        public SettingsCacheIniImpl(string fileName)
        {
            //TODO Вариант, если файла нету
            this.IniFileName = fileName;
            loadFile();
        }

        public SettingsCacheIniImpl()
        {
            loadFile();
        }
        private void loadFile()
        {
            FileInfo fileInfo = new FileInfo(IniFileName);

            if (fileInfo.Exists)
            {
                data = parser.ReadFile(IniFileName);
            }
            else
            {
                FileStream fileSteam = File.Create(IniFileName);
                fileSteam.Close();
                data = parser.ReadFile(IniFileName);
            }
        }

        public string LastDriverNameOne()
        {
            return data[section]["LastDriveNameOne"];
        }

        public string LastDriverNameTwo()
        {
            return data[section]["LastDriveNameTwo"];
        }

        public List<string> LastPathOne()
        {
            string path = data[section]["LastPathOne"];
            if (path == null)
            {
                return new List<string>();
            }
            return BreakIntoWords(path);
        }

        public List<string> LastPathTwo()
        {
            string path = data[section]["LastPathTwo"];
            if (path == null)
            {
                return new List<string>();
            }
            return BreakIntoWords(path);
        }

        public void SaveLastDriverNameOne(string name)
        {
            data[section]["LastDriveNameOne"] = name;
            SaveFile();
        }

        public void SaveLastDriverNameTwo(string name)
        {
            data[section]["LastDriveNameTwo"] = name;
            SaveFile();
        }

        public void SaveLastPathOne(List<string> pathArray)
        {
            data[section]["LastPathOne"] = ConcatElements(pathArray).ToString();
            SaveFile();
        }

        public void SaveLastPathTwo(List<string> pathArray)
        {
            data[section]["LastPathTwo"] = ConcatElements(pathArray).ToString();
            SaveFile();
        }

        public void SaveFile()
        {
            parser.WriteFile(IniFileName, data);
        }

        public static string ConcatElements(List<string> pathArray)
        {
            return String.Join(",", pathArray);
        }

        public static List<string> BreakIntoWords(string path)
        {
            List<string> pathArray = new List<string>(path.Split(','));
            return pathArray;
        }
    }
}
