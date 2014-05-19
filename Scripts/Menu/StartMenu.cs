﻿using UnityEngine;namespace Assets.MainMenu{    public class StartMenu : MonoBehaviour    {        public Texture backgroundTexture;        public GUISkin Style;        public int width;        public int height;        private const string quitButton = "Выход";        private const string editorButton = "Редактор";        private const string gameButton = "Играть";        private const string propertyButton = "Настройки";                void OnGUI()        {            GUILayout.BeginArea(new Rect(0f, 0f, Screen.width*2f, Screen.height*2f), backgroundTexture);            GUILayout.BeginArea(new Rect((Screen.width / 2f) - (width / 2f), (Screen.height / 2f) - (height / 2f), width, height), Style.window);            GUILayout.Space(15);            if (GUILayout.Button(editorButton, Style.button)) Application.LoadLevel("EditorScene");            if (GUILayout.Button(gameButton, Style.button)) Application.LoadLevel("0");            if (GUILayout.Button(propertyButton, Style.button)) Application.LoadLevel("Property");            if (GUILayout.Button(quitButton, Style.button)) Application.Quit();            GUILayout.EndArea();            GUILayout.EndArea();        }    }}