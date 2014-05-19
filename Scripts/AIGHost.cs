using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public class AIGHost : MonoBehaviour
    {

        public float Speed; //Скорость
        private Transform waypoint;
        public Transform Target;

        void OnTriggerEnter(Collider other)
        {

            transform.position.Normalize();
            
            //посмотрим, нет ли рядом PacMana
            if (FindPacman())
            {
                Speed = 2;
                return;
            }

            var point = other.gameObject.GetComponent<Point>(); // получим компонент Point
            if (point != null)
            {
                //Получим соседние элементы пола...
                //Чтобы соблюсти дух старого Pac-man, просто выберем направление рандомно...
                var a = point.Neighbor.Length;
                var i = Random.Range(0, a);
                // Дурь
                if(point.Neighbor[i]!=null) waypoint = point.Neighbor[i];
            }
            
        }

        /// <summary>
        /// Поиск Пакмана
        /// </summary>
        /// <returns>Нашли/не нашли</returns>
        private bool FindPacman()
        {
            RaycastHit hit;

            // Стреляем на лево... Если подстрелили кого-то...
            var target = Target.position;

            //Debug.DrawLine(new Vector3(transform.position.x, 1, transform.position.z), target, Color.red);

            if(Physics.Raycast(new Vector3(transform.position.x, 1, transform.position.z), target, out hit, 8.0f)){
                // смотрим кого
                if (hit.collider.gameObject.name == "PacmanBody(Clone)")
                {
                    waypoint = hit.collider.gameObject.transform;
                    return true;
                }
            }

            return false;
        }


        // Update is called once per frame
        void Update()
        {
            
            if (waypoint != null)
            {
                transform.LookAt(waypoint);
                transform.Translate(new Vector3(0.0f, 0.0f, Speed)*Time.deltaTime);
            }

            //При старте может не сработать триггер пола
            else
            {
                var point = Point.GetNeighborPositions("floor(Clone)", transform);
                waypoint = point[Random.Range(0, point.Length - 1)];
            }
             
        }
    }
}
