using UnityEngine;

namespace Assets
{
    public class SoulItem : MonoBehaviour
    {
        public void Hearer()
        {
            //Находим компонент Progress и меняем счетчик
            var _progress = FindObjectOfType<Progress>();
            _progress.SoulLabel = _progress.SoulLabel + 1;
            //Самоуничтожаемся
            Destroy(gameObject);
        }
    }
}