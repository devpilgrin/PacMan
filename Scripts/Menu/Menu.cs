using UnityEngine;

namespace Assets
{
    public class Menu : MonoBehaviour
    {
        private bool render;
        private const float width = 400.0f;
        private const float height = 400.0f;
        private Rect windowRect;
        void Start()
        {
            windowRect = new Rect((Screen.width / 2.0f) - (width / 2), (Screen.height / 2.0f) - (height / 2), width, height);
        }
        void OnGUI()
        {
            if (render)
            {
                windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Помощь по игре", GUILayout.Width(width));
            }
        }
        public void ToggleWindow()
        {
            render = !render;
        }
        void DoMyWindow(int windowID)
        {
            if (GUILayout.Button("ОК")) ToggleWindow();
            GUILayout.Label("");
        }
    }
}
