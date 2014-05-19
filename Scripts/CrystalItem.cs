using UnityEngine;

namespace Assets
{
    public class CrystalItem : MonoBehaviour
    {
        public void Hearer()
        {
            //Находим компонент Progress и меняем счетчик
            var _progress = FindObjectOfType<Progress>();
            _progress.CrystalLabel = _progress.CrystalLabel + 1;
            //Самоуничтожаемся
            Destroy(gameObject);
        }
    }
}