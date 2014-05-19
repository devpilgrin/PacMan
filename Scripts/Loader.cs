using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets
{
    public class Loader : MonoBehaviour
    {
        /// <summary>
        /// Функция загрузки объекта из префаба по имени.
        /// </summary>
        /// <param name="prefabname">Наименование префаба</param>
        /// <param name="position">Позиция в сцене куда будет помещен префаб</param>
        /// <returns>GameObject</returns>
        public static void LoadByName(string prefabname, Vector3 position)
        {
            var arrow = Resources.Load(prefabname);
            Instantiate(arrow, position, Quaternion.identity);
        }

        /// <summary>
        /// Загрузка игрового уровня
        /// </summary>
        /// <param name="name">Наименование игрового уровня без расширения файла</param>
        public static void LoadLevel(string name)
        {
            //задаем путь к файлу (во время редактирования это дирректория Asets/Resources, в скомпилированной игре: ИмяПроектаBuild_Data/Resources)
            var file = Application.dataPath + "/Resources/maps/" + name + ".txt";
            
            //Сразу считаем все строки файла в массив
            var lines = File.ReadAllLines(file);
            
            //В этот лист запишем персонажей, так как они должны размещаться на уровне уже после его загрузки
            var aSends = new List<aSend>();
            
            //Ставим индексатор расположение элементов по оси X = 0
            var x = 0;          
            //Проходим по массиву строк
            foreach (var line in lines)
            {
                int z = 0; //Обнуляем индексатор по оси Y
                //Проходим по каждому символу строки
                foreach (char c in line)
                {
                    //Проверяем условия и выполняем действия над объектами уровня
                    switch (c)
                    {
                        case '0':
                            //Клонируем объект из префаба в игровой уровень, в качестве координат используем переменные индексаторы массива по строкам и символам (x/z)
                            LoadByName("wall", new Vector3(x * 2, 0, z * 2)); 
                            z++; //Увеличиваем значение индексатора оси Z
                            break;
                        case '1':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            LoadByName("Point", new Vector3(x * 2, 0, z * 2));
                            z++;
                            break;
                        case '2':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            aSends.Add(new aSend("Ghost", new Vector3(x * 2, 0, z * 2))); // НПС и ПС грузим отдельно.
                            z++;
                            break;
                        case '3':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            aSends.Add(new aSend("PacmanBody", new Vector3(x * 2, 0, z * 2)));
                            z++;
                            break;
                        case '4':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            LoadByName("Crystal", new Vector3(x * 2, 0, z * 2));
                            z++;
                            break;
                        case '5':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            LoadByName("Soul", new Vector3(x * 2, 0, z * 2));
                            z++;
                            break;
                        case '6':
                            LoadByName("floor", new Vector3(x * 2, 0, z * 2));
                            LoadByName("Boom", new Vector3(x * 2, 0, z * 2));
                            z++;
                            break;
                    }

                }
                x++;
            }

            var com = FindObjectsOfType<Point>();
            foreach (var point in com)
            {
                point.forced();
            }

            //Грузим НПС и ПС
            foreach (var asend in aSends)
            {
                LoadByName(asend.Name, asend.Position);
            }


        }
        // заглушка конструктора Loader (static не подходит, нужно наследовться от MonoBehaviour)
        private Loader() { }
    }

    //Класс для временного хранения данных о НПС и ПС
    class aSend
    {
        public string Name;
        public Vector3 Position;

        public aSend(string name, Vector3 position)
        {
            Name = name;
            Position = position;
        }
    }
}
