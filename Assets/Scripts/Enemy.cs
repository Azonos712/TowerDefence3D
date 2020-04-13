using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10;
    private Transform target;
    private int wayPointIndex = 0;
    void Start()
    {
        target = Waypoints.points[wayPointIndex];
    }

    void Update()
    {
        //Вычисляем направление движения противника
        Vector3 dir = target.position - transform.position;
        //Приводим вектор направления к нормализованному, чтобы не было разных скоростей
        //Далее перемножаем этот вектор на скорость и разность времени между кадрами
        //И перемещаем в мировых координатах при помощи вызванного метода
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        //Проверка дистанции между текущей позицией и конечной точкой
        if (Vector3.Distance(transform.position, target.position) <= 0.3)
        {
            GetNextTarget();
        }
    }

    void GetNextTarget()
    {
        if (wayPointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        target = Waypoints.points[++wayPointIndex];
    }
}
