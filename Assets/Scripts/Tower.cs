using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [Header("General")]
    public float level = 1;
    public float startRange = 15f;
    [HideInInspector]
    public float range { get { return startRange * level; } }
    public float startTurnSpeed = 6f;
    private float turnSpeed { get { return startTurnSpeed * level; } }

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public Transform firePoint;
    public Transform helpFirePoint;

    protected Transform targetForShooting;
    protected Enemy targetEnemy;
    protected bool changeEnemy = false;

    protected void Start()
    {
        //Запускаем метод с повторением в секунду сразу же при вызове Start()
        InvokeRepeating("UpdateTarget", 0f, 0.6f);
        level = 1;
    }

    protected void UpdateTarget()
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
            targetForShooting = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
            changeEnemy = true;
        }
        else
        {
            targetForShooting = null;
        }
    }

    protected void LockOnTarget()
    {
        //Плавный поворот башни за захваченной целью
        //Вектор направления
        Vector3 dir = targetForShooting.position - transform.position;
        //Создает вращение с указанными направлениями вперед и вверх. В нашем случае направление вверх не используется.
        //Кватернион это конечное вращение, которое получается из исходного положения
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Интерполирует между 1 и 2 по 3 параметрам и нормализует результат впоследствии и возвращает представление угла Эйлера для поворота.
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;//lookRotation.eulerAngles;
        //Возвращает вращение, которое вращает z градусов вокруг оси z, x градусов вокруг оси x и y градусов вокруг оси y; применяется в этом порядке.
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    protected bool ReadyToShoot()
    {
        Vector3 dir1 = targetForShooting.position - firePoint.position;
        Vector3 dir2 = targetForShooting.position - helpFirePoint.position;

        if (Vector3.Angle(dir1.normalized, dir2.normalized) <= 7)
        {
            if (dir1.magnitude <= dir2.magnitude)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    protected abstract void Shoot();
}
