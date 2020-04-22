using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Attributes")]
    public float range = 15f;
    public float turnSpeed = 6f;
    public float fireRate = 1f; // выстрелов в 1 секунду
    private float fireCountDown = 0f;
    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public GameObject bulletPrefab;

    public Transform firePoint;
    public Transform helpFirePoint;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        //Запускаем метод с повторением в полсекунды сразу же при вызове Start()
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //Находим все объекты на карте под тегом
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistanse = Mathf.Infinity;
        GameObject nearestEnemy = null;
        //Для каждого найденного противника находим дистанцию до текущей башни и сравниваем с самой короткой
        foreach (var enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistanse)
            {
                shortestDistanse = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //Если противник найден и дистанция башни позволяет стрелять, захватываем цель
        if (nearestEnemy != null && shortestDistanse <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        
        //Плавный поворот башни за захваченной целью
        //Вектор направления
        Vector3 dir = target.position - transform.position;
        //Создает вращение с указанными направлениями вперед и вверх. В нашем случае направление вверх не используется.
        //Кватернион это конечное вращение, которое получается из исходного положения
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Интерполирует между 1 и 2 по 3 параметрам и нормализует результат впоследствии и возвращает представление угла Эйлера для поворота.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//lookRotation.eulerAngles;
        //Возвращает вращение, которое вращает z градусов вокруг оси z, x градусов вокруг оси x и y градусов вокруг оси y; применяется в этом порядке.
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        

        if (fireCountDown <= 0f && ReadyToShoot())
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;
    }

    bool ReadyToShoot()
    {
        Vector3 dir1 = target.position - firePoint.position;
        Vector3 dir2 = target.position - helpFirePoint.position;

        if (Vector3.Angle(dir1, dir2) < 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        //Отображение дистанции башни в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
