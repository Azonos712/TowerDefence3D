using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("General")]
    public float level = 1;
    public float startRange = 15f;
    private float range { get { return startRange * level; } }
    public float startTurnSpeed = 6f;
    private float turnSpeed { get { return startTurnSpeed * level; } }

    [Header("Use Bullets (default)")]
    public float startFireRate = 1f;
    private float fireRate { get { return startFireRate * level; } } // выстрелов в 1 секунду
    private float fireCountDown = 0f;
    public GameObject bulletPrefab;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float startDamageOverTime = 20;
    private float damageOverTime { get { return startDamageOverTime * level; } }
    public float startSlowAmount = .3f;
    private float slowAmount { get { return startSlowAmount * level; } }
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public Transform partToRotate;
    public Transform firePoint;
    public Transform helpFirePoint;

    private Transform targetForShooting;
    private Enemy targetEnemy;

    private bool shotFired = false;
    private bool moveBack = true;
    private Vector3 startHelpFirePoint;

    void Start()
    {
        //Запускаем метод с повторением в секунду сразу же при вызове Start()
        InvokeRepeating("UpdateTarget", 0f, 1f);
        level = 1;
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
            targetForShooting = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            targetForShooting = null;
        }

        if (useLaser)
        {
            if (lineRenderer.enabled)
            {
                OffEffects();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shotFired)
            RecoilAfterShoot();

        fireCountDown -= Time.deltaTime;

        //Если нет цели - не поворачиваемся и не стреляем
        if (targetForShooting == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    OffEffects();
                }
            }

            return;
        }


        if (ReadyToShoot())
        {
            if (useLaser)
            {
                Laser();
            }
            else
            {
                if (fireCountDown <= 0f)
                {
                    Shoot();
                    InitializedBeforeRecoil();
                    fireCountDown = 1f / fireRate;
                }
            }
        }

        LockOnTarget();
    }

    void LockOnTarget()
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

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            OnEffects();
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetForShooting.position);

        Vector3 dir = firePoint.position - targetForShooting.position;

        //смещение и поворот эффекта
        impactEffect.transform.position = targetForShooting.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void OnEffects()
    {
        lineRenderer.enabled = true;
        impactEffect.Play();
        impactLight.enabled = true;
    }

    void OffEffects()
    {
        lineRenderer.enabled = false;
        impactEffect.Stop();
        impactLight.enabled = false;
    }

    bool ReadyToShoot()
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

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(targetForShooting);
    }

    void InitializedBeforeRecoil()
    {
        shotFired = true;
        moveBack = true;
        startHelpFirePoint = helpFirePoint.localPosition;
    }

    void RecoilAfterShoot()
    {
        var dist = Vector3.Distance(startHelpFirePoint, helpFirePoint.localPosition);

        if (moveBack == true)
        {
            if (dist >= 0.4)
            {
                moveBack = false;
                return;
            }
            helpFirePoint.Translate(Vector3.forward * 4 * Time.deltaTime, Space.Self);
        }
        else
        {
            if (dist <= 0.1)
            {
                shotFired = false;
                helpFirePoint.localPosition = startHelpFirePoint;
                return;
            }
            helpFirePoint.Translate(Vector3.back * 1 * Time.deltaTime, Space.Self);
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Отображение дистанции башни в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
