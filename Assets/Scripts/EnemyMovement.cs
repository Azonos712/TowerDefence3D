using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform currentTarget;
    private int wayPointIndex = 0;

    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        currentTarget = Waypoints.points[wayPointIndex];
    }

    void Update()
    {
        MoveAndRotateToTarget();

        //Проверка дистанции между текущей позицией и конечной точкой
        if (Vector3.Distance(transform.position, currentTarget.position) <= 0.4)
        {
            GetNextTarget();
        }
    }

    void GetNextTarget()
    {
        if (wayPointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        currentTarget = Waypoints.points[++wayPointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;

        Destroy(gameObject);
    }

    void MoveAndRotateToTarget()
    {
        //Вычисляем направление движения противника к текущей цели
        Vector3 dir = currentTarget.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * enemy.turnSpeed).eulerAngles;//lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //Приводим вектор направления к нормализованному, чтобы не было разных скоростей
        //Далее перемножаем этот вектор на скорость и разность времени между кадрами
        //И перемещаем в мировых координатах при помощи вызванного метода
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        enemy.speed = enemy.startSpeed;
    }
}
