using UnityEngine;

public class Waypoints : MonoBehaviour
{
    //массив с путевыми точками
    public static Transform[] points;

    //при старте получаем все путевые точки и сохраняем их в массив
    private void Awake()
    {
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
