using Assets.MainMenu;
using UnityEngine;

namespace Assets
{
    public class PacManController : MonoBehaviour
    {
        public float Speed; //Скорость
        private Vector3 waypoint = Vector3.zero;
        private Progress progress;
        private Direction direction; //направление
        private Point point;
        private bool _alternativeInput;

        void Start()
        {
            var path = Application.dataPath + "/Resources/settings.ini";
            var ini = new IniParser(path);

            if (ini.GetSetting("Input", "AlternativeInput") == "True")
                _alternativeInput = true;
            if (ini.GetSetting("Input", "AlternativeInput") == "False")
                _alternativeInput = false;

            //Получаем глобальный прогресс
            progress = FindObjectOfType<Progress>();
        }

        //При попадании в триггер пола.
        void OnTriggerEnter(Collider other)
        {
            point = other.gameObject.GetComponent<Point>(); // получим компонент Point
            DirectionDefinition();
        }

        // Update is called once per frame
        void Update()
        {
           //Перехватываем нажатие клавиш и передаем направления...

            if (!_alternativeInput)
            {
                if (Input.GetKey(KeyCode.A)) direction = Direction.left;
                if (Input.GetKey(KeyCode.D)) direction = Direction.right;
                if (Input.GetKey(KeyCode.S)) direction = Direction.back;
                if (Input.GetKey(KeyCode.W)) direction = Direction.forward;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftArrow)) direction = Direction.left;
                if (Input.GetKey(KeyCode.RightArrow)) direction = Direction.right;
                if (Input.GetKey(KeyCode.DownArrow)) direction = Direction.back;
                if (Input.GetKey(KeyCode.UpArrow)) direction = Direction.forward;                
            }

            //двигаемся
            if (waypoint != Vector3.zero)
            {
                var s = Speed + progress.CrystalLabel;
                transform.LookAt(waypoint);
                transform.Translate(new Vector3(0.0f, 0.0f, s)*Time.deltaTime);
            }
            else //Если не двигаемся...
            {
                DirectionDefinition();
            }
        }

        /// <summary>
        /// Поиск доступных направлений для движения.
        /// </summary>
        private void DirectionDefinition()
        {
            if (point != null)
            {
                waypoint = Vector3.zero;
                foreach (var transform1 in point.Neighbor)
                {
                    if(transform1 == null) return;
                    //Тут конечно полный отстой, но погрешности transform.Translate не оставляют другого выбора...
                    var X = (int) (transform1.position.x/2);
                    var Z = (int) (transform1.position.z/2);
                    var otherX = (int) (point.transform.position.x/2);
                    var otherZ = (int) (point.transform.position.z/2);
                    //Получаем разность векторов которая равна вектору направления
                    var vector = new Vector3((X - otherX), 0, (Z - otherZ));
                    //Проверяем направление текущего элемента коллекции и переменную содержащую нужное нам направление, если есть одно из совпадений - задаем waypoint
                    if (vector == Vector3.forward && direction == Direction.forward) waypoint = transform1.position;
                    if (vector == Vector3.back && direction == Direction.back) waypoint = transform1.position;
                    if (vector == Vector3.left && direction == Direction.left) waypoint = transform1.position;
                    if (vector == Vector3.right && direction == Direction.right) waypoint = transform1.position;
                }
            }
        }
    }

    /// <summary>
    /// Направления движения
    /// </summary>
    public enum Direction
    {
        left,
        right,
        back,
        forward
    }
}
