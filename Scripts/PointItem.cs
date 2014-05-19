using UnityEngine;

namespace Assets
{
    public class PointItem : MonoBehaviour
    {
        public void Hearer()
        {
            //Находим компонент Progress и меняем счетчик
            var _progress = FindObjectOfType<Progress>();
            _progress.PointLabel = _progress.PointLabel + 1;
            //Самоуничтожаемся
            Destroy(gameObject);
        }
    }
}