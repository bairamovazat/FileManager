using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    class SettingsCachePropertyImpl : ISettingsCache
    {
        public int Width
        {
            get {
                string width = Properties.Settings.Default.Width;
                if (int.TryParse(width, out int value))
                {
                    return value;
                }
                return 0;
            }
            set => Properties.Settings.Default.Width = value.ToString();
        }

        public int XPosition
        {
            get
            {
                string xPosition = Properties.Settings.Default.XPosition;
                if (int.TryParse(xPosition, out int value))
                {
                    return value;
                }
                return 0;
            }
            set => Properties.Settings.Default.XPosition = value.ToString();
        }

        public int YPosition
        {
            get
            {
                string yPosition = Properties.Settings.Default.YPosition;
                if (int.TryParse(yPosition, out int value))
                {
                    return value;
                }
                return 0;
            }
            set => Properties.Settings.Default.YPosition = value.ToString();
        }

        public int Height
        {
            get
            {
                string height = Properties.Settings.Default.Height;
                if (int.TryParse(height, out int value))
                {
                    return value;
                }
                return 0;
            }
            set => Properties.Settings.Default.Height = value.ToString();
        }

        public string LastDriverNameOne()
        {
            return Properties.Settings.Default.LastDriveNameOne;
        }

        public string LastDriverNameTwo()
        {
            return Properties.Settings.Default.LastDriveNameTwo;
        }

        public List<string> LastPathOne()
        {
            string path = Properties.Settings.Default.LastPathOne;
            if (path == null)
            {
                return new List<string>();
            }
            return BreakIntoWords(path);
        }

        public List<string> LastPathTwo()
        {
            string path = Properties.Settings.Default.LastPathTwo;
            if (path == null)
            {
                return new List<string>();
            }
            return BreakIntoWords(path);
        }

        public void SaveLastDriverNameOne(string name)
        {
            Properties.Settings.Default.LastDriveNameOne = name;
            Properties.Settings.Default.Save();

        }

        public void SaveLastDriverNameTwo(string name)
        {
            Properties.Settings.Default.LastDriveNameTwo = name;
            Properties.Settings.Default.Save();
        }

        public void SaveLastPathOne(List<string> pathArray)
        {
            Properties.Settings.Default.LastPathOne = ConcatElements(pathArray).ToString();
            Properties.Settings.Default.Save();
        }

        public void SaveLastPathTwo(List<string> pathArray)
        {
            Properties.Settings.Default.LastPathTwo = ConcatElements(pathArray).ToString();
            Properties.Settings.Default.Save();
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
