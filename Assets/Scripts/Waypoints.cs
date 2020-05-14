using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //создание массива с путевыми точками
    public static Transform[] points;

    //в самом начале (данный метод вызывается перед Start) получаем все путевые точки и сохраняем их в массив
    //по-сути кэшируем данные
    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
