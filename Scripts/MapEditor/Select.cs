using UnityEngine;

namespace Assets.EditorResource
{
	//Обработка действий мышки...
    public class Select : MonoBehaviour
    {
		//Переменная для хранения оригинального цвета
        private Color OriginColor;
		//Цвет выделенного объекта
        private readonly Color SelectColor = new Color(255,255,255);
        
        
        void Start()
        {
            //Запоминаем оригинальный материал
            OriginColor = renderer.material.color;
        }

        /// <summary>
        /// При наведении указателя курсора на объект
        /// </summary>
        void OnMouseOver()
        {
            //Подкрасим материал
            renderer.material.color = SelectColor;
        }

        /// <summary>
        /// При нажатии кнопки мыши
        /// </summary>
        void OnMouseDown()
        {
            //Получаем  объект и устанавливаем ему текстуру
            GetComponent<MapPoint>().MapPinTupe = FindObjectOfType<EditorGui>().SetType;
            renderer.material.mainTexture = FindObjectOfType<EditorGui>().SetTexture;
        }

        /// <summary>
        /// Если указатель мышки ушел с объекта
        /// </summary>
        void OnMouseExit()
        {
            //Возвращаем оригинальный цвет текстуры
            renderer.material.color = OriginColor;
        }
    }
}
