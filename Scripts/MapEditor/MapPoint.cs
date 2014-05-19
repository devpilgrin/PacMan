using UnityEngine;namespace Assets.EditorResource{    public class MapPoint : MonoBehaviour    {        /// <summary>        /// Позиция эллемента по X        /// </summary>        public int X        {            get            {                return (int) transform.position.x;                         }        }        /// <summary>        /// Позиция эллемента по Y        /// </summary>        public int Y        {            get            {                return (int)transform.position.y;             }        }        public Type MapPinTupe = Type.wall;

        public int Index = 0;

    }}