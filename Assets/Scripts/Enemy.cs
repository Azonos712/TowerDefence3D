﻿using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 9;
    private float turnSpeed = 6;

    private Transform currentTarget;

    private int wayPointIndex = 0;
    void Start()
    {
        currentTarget = Waypoints.points[wayPointIndex];
    }

    void Update()
    {
        MoveToTarget();

        //Проверка дистанции между текущей позицией и конечной точкой
        if (Vector3.Distance(transform.position, currentTarget.position) <= 0.4)
        {
            GetNextTarget();
        }
        RotateToTarget();
    }

    void GetNextTarget()
    {
        if (wayPointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        currentTarget = Waypoints.points[++wayPointIndex];
    }

    void MoveToTarget()
    {
        //Вычисляем направление движения противника
        Vector3 dir = currentTarget.position - transform.position;
        //Приводим вектор направления к нормализованному, чтобы не было разных скоростей
        //Далее перемножаем этот вектор на скорость и разность времени между кадрами
        //И перемещаем в мировых координатах при помощи вызванного метода
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    }

    void RotateToTarget()
    {
        Vector3 dir = currentTarget.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}
