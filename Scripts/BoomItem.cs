using UnityEngine;namespace Assets{    public class BoomItem : MonoBehaviour    {        /// <summary>        /// ���������... ���������� ��� ���� ����������� ������        /// </summary>        public void Hearer()        {            //var _progress = FindObjectOfType<Progress>();                        //������� ���� ����������            var gH = FindObjectsOfType<AIGHost>();            //� ���������� ������� �������            bool del = true;            foreach (var aigHost in gH)            {
                if (del)
                {
                    Destroy(aigHost.gameObject);
                    del = !del;
                }
                else
                {
                    del = !del;
                }
            }            //����������������            Destroy(gameObject);        }    }}