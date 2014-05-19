using Assets.MainMenu;
using UnityEngine;

namespace Assets
{
    public class Progress : MonoBehaviour
    {
        //Тестура иконки Point
        public Texture PointTexture;
        //Значение счетчика
        public int PointLabel { get; set; }

        //Тестура иконки Crystal
        public Texture CrystalTexture;
        //Значение счетчика
        public int CrystalLabel{ get; set; }
        //Тестура иконки Soul
        public Texture SoulTexture;
        //Значение счетчика
        public int SoulLabel{ get; set; }
        
        //Флаг перезагрузки сцены
        private bool reload;

        public GUISkin Style;

        private string Level;
        private string Name;
        private int status=2;

        private int points;


        void Update()
        {
            if (PointLabel == points) status = -1;
        }


        //Включаем отрисовку GUI
        public void OnGUI()
        {
            GUILayout.BeginArea(new Rect(5, 5, 250, 50), Style.box);
            GUILayout.BeginHorizontal();
            
            //Отображение иконки и счета съеденых Point            
            GUILayout.Label(PointTexture, Style.label);
            GUILayout.Label(PointLabel.ToString(), Style.label);
            GUILayout.Space(20);

            //Отображение иконки и счета съеденых Crystal
            GUILayout.Label(new GUIContent(CrystalTexture), Style.label);
            GUILayout.Label(new GUIContent(CrystalLabel.ToString()), Style.label);
            GUILayout.Space(20);

            //Отображение иконки и счета оставшихся жизней
            GUILayout.Label(SoulTexture, Style.label);
            GUILayout.Label(SoulLabel.ToString(), Style.label);
            GUILayout.Space(20);
            
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(5, Screen.height - 100, 100, 100), Style.box);
            //Запуск и закрытие компонента Menu (глобальное игровое меню)
            if (GUILayout.Button("Menu", Style.button)) Application.LoadLevel("StartMenu");
            GUILayout.EndArea();

            if (status == 1) GUILayout.Window(0, new Rect(Screen.width / 2f - 150, Screen.height / 2f - 100,100,  100), GameOverWindow, "", Style.window);
            if (status == 0) GUILayout.Window(0, new Rect(Screen.width / 2f - 150, Screen.height / 2f - 100,100,  100), GameOverWindow, "", Style.window);
            if (status == -1) GUILayout.Window(0, new Rect(Screen.width / 2f - 150, Screen.height / 2f - 100,100,  100), GameOverWindow, "", Style.window);

        }


        public void Start()
        {
            var path = Application.dataPath + "/Resources/profile.ini";
            var ini = new IniParser(path);
            Name = ini.GetSetting("User", "Name");
            Level = ini.GetSetting("User", "Level");
            PointLabel = int.Parse(ini.GetSetting("User", "Balls"));
            SoulLabel = int.Parse(ini.GetSetting("User", "Souls"));            
            
            var ts = Time.timeScale;
            Time.timeScale = 0.0f;
            Loader.LoadLevel(Level);
            Time.timeScale = ts;

            points = FindObjectsOfType<PointItem>().Length + PointLabel;
        }

        public void Finish()
        {
            Save(true);
            var ts = Time.timeScale;
            Time.timeScale = 0.0f;
            Loader.LoadLevel((int.Parse(Level) + 1).ToString());
            Time.timeScale = ts;
        }

        /// <summary>
        /// Сохранение данных прогресса игры
        /// </summary>
        /// <param name="nextLevel">Определяет загружать при следующем запуске текущий уровень или следующий</param>
        private void Save(bool nextLevel)
        {
            var path = Application.dataPath + "/Resources/profile.ini";
            var ini = new IniParser(path);
            var levelNext = 0;
            if (nextLevel) levelNext = 1;
            ini.AddSetting("User", "Name", Name);
            ini.AddSetting("User", "Level", (int.Parse(Level) + levelNext).ToString());
            if (nextLevel) ini.AddSetting("User", "Balls", PointLabel.ToString());
            if (nextLevel) ini.AddSetting("User", "Souls", (SoulLabel).ToString());
            else ini.AddSetting("User", "Souls", (SoulLabel-1).ToString());
            ini.SaveSettings();
        }

        public void GameOver()
        {
            if (SoulLabel == 0) status = 0;
            if (SoulLabel >= 1) status = 1;
        }

        public void GameOverWindow(int windowID)
        {
            if (status == 0)
            {
                GUILayout.Label("Pac-Man погиб, Вы проиграли!", Style.label);
                if (GUILayout.Button("Выйти в главное меню", Style.button)) Application.LoadLevel("StartMenu");
            }
            if (status == 1)
            {
                GUILayout.Label("Pac-Man погиб, попробуйте еще раз!", Style.label);
                Save(false);
                if (GUILayout.Button("Выйти в главное меню", Style.button)) Application.LoadLevel("StartMenu");
                if (GUILayout.Button("Играть уровень заново", Style.button)) Application.LoadLevel("0");
            }
            if (status == -1)
            {
                GUILayout.Label("Поздравляем, вы победили", Style.label);
                Save(true);
                if (GUILayout.Button("Выйти в главное меню", Style.button)) Application.LoadLevel("StartMenu");
                if (GUILayout.Button("Продолжить", Style.button)) Application.LoadLevel("0");
            }

        }



    }
}
