﻿using UnityEngine;namespace Assets{    //Кушаем объекты с триггерами    public class EatingItems : MonoBehaviour    {        //Если триггер        void OnTriggerEnter(Collider other)        {            //Проверяем объект триггера на то, чтобы был не пол            if (other.gameObject.name != "floor(Clone)" && other.gameObject.name != "Ghost(Clone)") other.gameObject.SendMessageUpwards("Hearer"); //Передаем сообщение компоненту с триггером            if (other.gameObject.name == "Ghost(Clone)")            {                                GameOver();            }        }        private void GameOver()        {            var GO = FindObjectOfType<Progress>();            GO.SendMessage("GameOver");            Destroy(gameObject);        }    }}