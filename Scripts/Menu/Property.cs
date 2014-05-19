using System.IO;
using UnityEngine;

namespace Assets.MainMenu
{
    public class Property : MonoBehaviour {
        
        public Texture backgroundTexture;
        public GUISkin Style;
        public int width;
        public int height;

        //Строковые константы (по идее можно настроить локализацию... Если останется время - сделаю)
        private const string textToggle = "Использовать альтернативное управление";
        private const string backButton = "Назад"; 
        private const string saveButton = "Сохранить";
        private const string descriptionLabel = "Альтернативное управление - это управление с помощью клавиш управления курсором"; 
        
        //Параметры настройки приложения
        private bool toggleTxt;
        private string path;

        // Use this for initialization
        void Start () {
            path = Application.dataPath + "/Resources/settings.ini";

            var ini = new IniParser(path);
            
            if (ini.GetSetting("Input", "AlternativeInput") == "True")
                toggleTxt = true;
            if (ini.GetSetting("Input", "AlternativeInput") == "False")
                toggleTxt = false;

        }
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0f, 0f, Screen.width * 2f, Screen.height * 2f), backgroundTexture);

            GUILayout.Window(0,
                new Rect((Screen.width / 2f) - (width / 2f), (Screen.height / 2f) - (height / 2f), width, height), DoMyWindow, "",
                Style.window);

            GUILayout.EndArea();
        }


        void DoMyWindow(int windowID)
        {

            //Честно говоря не знаю что в этой игре можно настраивать, особенно учитывая настройки самого Unity
            toggleTxt = GUILayout.Toggle(toggleTxt, new GUIContent(textToggle), Style.toggle, GUILayout.Width(300));
            GUILayout.Space(15);
            GUILayout.Label(new GUIContent(descriptionLabel), Style.textArea);



            GUILayout.Space(15);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(backButton, Style.button)) Application.LoadLevel("StartMenu");
            GUILayout.Space(15);
            if (GUILayout.Button(saveButton, Style.button))
            {
                Application.LoadLevel("StartMenu");
                Save();
            }
            GUILayout.EndHorizontal();
        }

        private void Save()
        {
           
            if (File.Exists(path))
            {
                var ini = new IniParser(path);
                ini.AddSetting("Input", "AlternativeInput", toggleTxt.ToString());
                ini.SaveSettings();
            }
            else
            {
                File.Create(path);
                var ini = new IniParser(path);
                ini.AddSetting("Input", "AlternativeInput", toggleTxt.ToString());
                ini.SaveSettings();
            }

        }
    }
}
