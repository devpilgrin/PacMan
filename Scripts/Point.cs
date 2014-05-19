using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    class Point : MonoBehaviour
    {
        private Transform[] neighbor;
        /// <summary>
        /// Соседи данного элемента...
        /// </summary>
        public Transform[] Neighbor
        {
            get { return neighbor ?? (neighbor = GetNeighborPositions(gameObject.name, transform)); }
        }

        public void forced()
        {
            neighbor = GetNeighborPositions(gameObject.name, transform);
        }

        /// <summary>
        /// Получение всех соседей данного объекта по имени, получаем только соседей к которым можно пройти по прямой. 
        /// </summary>
        /// <param name="objectName">Наименование объекта</param>
        /// <param name="objecttransform"></param>
        public static Transform[] GetNeighborPositions(string objectName, Transform objecttransform)
        {
            //так как соседями могут оказаться только 4 ближайших эллемента:
            var _transform = new List<Transform>();
            RaycastHit hit;

            //Атакуем их лучами и посмотирим что за дичь подстрелили...
            // Стреляем на лево... Если подстрелили кого-то...
            Physics.Raycast(objecttransform.position, Vector3.left, out hit, 2.0f);
            // смотрим кого
            if (hit.collider.gameObject.name != null && hit.collider.gameObject.name == objectName) _transform.Add(hit.collider.gameObject.transform);
            
            //Среляем вправо 
            Physics.Raycast(objecttransform.position, Vector3.right, out hit, 2.0f);
            if (hit.collider.gameObject.name != null && hit.collider.gameObject.name == objectName) _transform.Add(hit.collider.gameObject.transform);
            
            //Среляем на назад 
            Physics.Raycast(objecttransform.position, Vector3.back, out hit, 2.0f);
            if (hit.collider.gameObject.name != null && hit.collider.gameObject.name == objectName) _transform.Add(hit.collider.gameObject.transform);

            //Среляем вперед 
            Physics.Raycast(objecttransform.position, Vector3.forward, out hit, 2.0f);
            if (hit.collider.gameObject.name != null && hit.collider.gameObject.name == objectName) _transform.Add(hit.collider.gameObject.transform);
          
            //Зпишем все в массив соседей
            return  _transform.ToArray();
        }
    }
}