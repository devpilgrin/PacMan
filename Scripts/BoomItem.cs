using UnityEngine;namespace Assets{    public class BoomItem : MonoBehaviour    {        /// <summary>        /// Слушатель... Вызывается для всех компонентов Итемов        /// </summary>        public void Hearer()        {            //var _progress = FindObjectOfType<Progress>();                        //Находим всех приведений            var gH = FindObjectsOfType<AIGHost>();            //И уничтожаем каждого второго            bool del = true;            foreach (var aigHost in gH)            {
                if (del)
                {
                    Destroy(aigHost.gameObject);
                    del = !del;
                }
                else
                {
                    del = !del;
                }
            }            //Самоуничтожаемся            Destroy(gameObject);        }    }}