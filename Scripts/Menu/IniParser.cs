using System;
using System.Collections;
using System.IO;

namespace Assets.MainMenu
{
    /// <summary>
    /// Класс для чтения и записи Ini файлов
    /// </summary>
    public class IniParser
    {
        private readonly Hashtable keyPairs = new Hashtable();
        private readonly String iniFilePath;

        private struct SectionPair
        {
            public String Section;
            public String Key;
        }

        /// <summary>
        /// Открытие ини файла по полному пути
        /// </summary>
        /// <param name="iniPath">Полный путь к Ini файлу.</param>
        public IniParser(String iniPath)
        {
            TextReader iniFile = null;
            String currentRoot = null;
            iniFilePath = iniPath;

            if (File.Exists(iniPath))
            {
                try
                {
                    iniFile = new StreamReader(iniPath);
                    var strLine = iniFile.ReadLine();

                    while (strLine != null)
                    {
                        strLine = strLine.Trim();

                        if (strLine != "")
                        {
                            if (strLine.StartsWith("[") && strLine.EndsWith("]")) currentRoot = strLine.Substring(1, strLine.Length - 2);
                            
                            else
                            {
                                var keyPair = strLine.Split(new[] { '=' }, 2);

                                SectionPair sectionPair;
                                String value = null;

                                if (currentRoot == null) currentRoot = "ROOT";

                                sectionPair.Section = currentRoot;
                                sectionPair.Key = keyPair[0];

                                if (keyPair.Length > 1) value = keyPair[1];

                                keyPairs.Add(sectionPair, value);
                            }
                        }

                        strLine = iniFile.ReadLine();
                    }

                }
                catch (Exception exception) { throw exception; }
                finally { if (iniFile != null) iniFile.Close(); }
            }
            else throw new FileNotFoundException("Не удалось найти " + iniPath);
        }

        /// <summary>
        /// Возвращает значение для данного раздела, пары ключей.
        /// </summary>
        /// <param name="sectionName">Имя раздела.</param>
        /// <param name="settingName">Имя ключа.</param>
        public String GetSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            return (String)keyPairs[sectionPair];
        }

        /// <summary>
        /// Перебор всех значений раздела.
        /// </summary>
        /// <param name="sectionName">Имя раздела.</param>
        public String[] EnumSection(String sectionName)
        {
            var tmpArray = new ArrayList();
            foreach (SectionPair pair in keyPairs.Keys) if (pair.Section == sectionName) tmpArray.Add(pair.Key);
            return (String[])tmpArray.ToArray(typeof(String));
        }

        /// <summary>
        /// Добавляет или изменяет параметр, который будет сохранен.
        /// </summary>
        /// <param name="sectionName">Имя раздела для добавления.</param>
        /// <param name="settingName">Имя ключа для добавления.</param>
        /// <param name="settingValue">Значение ключа.</param>
        public void AddSetting(String sectionName, String settingName, String settingValue)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            if (keyPairs.ContainsKey(sectionPair)) keyPairs.Remove(sectionPair);
            keyPairs.Add(sectionPair, settingValue);
        }

        /// <summary>
        /// Перегрузка етода для добавление нулевых значений параметра
        /// </summary>
        /// <param name="sectionName">Имя раздела для добавления.</param>
        /// <param name="settingName">Имя ключа для добавления.</param>
        public void AddSetting(String sectionName, String settingName)
        {
            AddSetting(sectionName, settingName, null);
        }

        /// <summary>
        /// Удаление параметра.
        /// </summary>
        /// <param name="sectionName">Имя раздела.</param>
        /// <param name="settingName">Имя ключа для удаления.</param>
        public void DeleteSetting(String sectionName, String settingName)
        {
            SectionPair sectionPair;
            sectionPair.Section = sectionName;
            sectionPair.Key = settingName;
            if (keyPairs.ContainsKey(sectionPair)) keyPairs.Remove(sectionPair);
        }

        /// <summary>
        /// Сохранить ini в новй файл.
        /// </summary>
        /// <param name="newFilePath">Путь к файлу.</param>
        public void SaveSettings(String newFilePath)
        {
            var sections = new ArrayList();
            var strToSave = "";

            foreach (SectionPair sectionPair in keyPairs.Keys) if (!sections.Contains(sectionPair.Section)) sections.Add(sectionPair.Section);
            
            foreach (String section in sections)
            {
                strToSave += ("[" + section + "]\r\n");

                foreach (SectionPair sectionPair in keyPairs.Keys)
                {
                    if (sectionPair.Section == section)
                    {
                        var tmpValue = (String)keyPairs[sectionPair];
                        if (tmpValue != null) tmpValue = "=" + tmpValue;
                        strToSave += (sectionPair.Key + tmpValue + "\r\n");
                    }
                }
                strToSave += "\r\n";
            }

            try
            {
                TextWriter tw = new StreamWriter(newFilePath);
                tw.Write(strToSave);
                tw.Close();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Сохранить настройки в ini файле.
        /// </summary>
        public void SaveSettings()
        {
            SaveSettings(iniFilePath);
        }
    }

    /// <summary>
    /// Пример использования класса (чтение)
    /// </summary>
    public class TestParser
    {
        static readonly IniParser parser = new IniParser(@"C:\test.ini");
        
        public static void Main()
        {
            var newMessage = parser.GetSetting("appsettings", "msgpart1");
            newMessage += parser.GetSetting("appsettings", "msgpart2");
            newMessage += parser.GetSetting("punctuation", "ex");

            Console.WriteLine(newMessage);
            Console.ReadLine();
        }
    }
}
