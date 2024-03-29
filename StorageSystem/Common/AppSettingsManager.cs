using Newtonsoft.Json;
using System.IO;

namespace StorageSystem.Common
{
    public static class AppSettingsManager
    {
        private static Dictionary<string, object> ConfigDic = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("appsettings.json"));

        public static bool InitEnable
        {
            get
            {
                if (ConfigDic == null)
                {
                    return false;
                }
                else
                {
                    if (ConfigDic.ContainsKey("InitStorage"))
                    {
                        bool b;
                        if (bool.TryParse(ConfigDic["InitStorage"].ToString(), out b))
                        {
                            return b;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static bool IsDarkMode
        {
            get
            {
                if (ConfigDic == null)
                {
                    return false;
                }
                else
                {
                    if (ConfigDic.ContainsKey("IsDarkMode"))
                    {
                        bool b;
                        if (bool.TryParse(ConfigDic["IsDarkMode"].ToString(), out b))
                        {
                            return b;
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public static object Color
        {
            get
            {
                if (ConfigDic == null)
                {
                    return System.Windows.Media.Color.FromArgb(0, 74, 20, 140);
                }
                else
                {
                    if (ConfigDic.ContainsKey("Color"))
                    {
                        byte a = Convert.ToByte(ConfigDic["Color"].ToString().Substring(1, 2), 16);
                        byte r = Convert.ToByte(ConfigDic["Color"].ToString().Substring(3, 2), 16);
                        byte g = Convert.ToByte(ConfigDic["Color"].ToString().Substring(5, 2), 16);
                        byte b = Convert.ToByte(ConfigDic["Color"].ToString().Substring(7, 2), 16);
                        return System.Windows.Media.Color.FromArgb(a, r, g, b);
                    }
                    else
                    {
                        return System.Windows.Media.Color.FromArgb(0, 74, 20, 140);
                    }
                }
            }
        }

        public static List<Dictionary<string, string>> StorageDic
        {
            get
            {
                if (ConfigDic == null)
                {
                    return new List<Dictionary<string, string>>();
                }
                else
                {
                    if (ConfigDic.ContainsKey("StorageInfo"))
                    {
                        return JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(ConfigDic["StorageInfo"].ToString());
                    }
                    else
                    {
                        return new List<Dictionary<string, string>>();
                    }
                }
            }
        }

        private static void SaveToFile()
        {
            File.WriteAllText("appsettings.json", JsonConvert.SerializeObject(ConfigDic, Formatting.Indented));
        }
        public static void SaveInitEnable(bool enable)
        {
            if (ConfigDic != null)
            {
                if (ConfigDic.ContainsKey("InitStorage"))
                {
                    ConfigDic["InitStorage"] = enable;
                }
                else
                {
                    ConfigDic.Add("InitStorage", enable);
                }
            }

            SaveToFile();
        }

        public static void SaveIsDarkMode(bool enable)
        {
            if (ConfigDic != null)
            {
                if (ConfigDic.ContainsKey("IsDarkMode"))
                {
                    ConfigDic["IsDarkMode"] = enable;
                }
                else
                {
                    ConfigDic.Add("IsDarkMode", enable);
                }
            }

            SaveToFile();
        }

        public static void SaveColor(object color)
        {
            if (ConfigDic != null)
            {
                if (ConfigDic.ContainsKey("Color"))
                {
                    ConfigDic["Color"] = color;
                }
                else
                {
                    ConfigDic.Add("Color", color);
                }
            }

            SaveToFile();
        }
    }
}
