using UnityEngine;

public class StandartTower : Tower
{
    public float startFireRate = 1f;
    private float fireRate { get { return startFireRate * level; } } // выстрелов в 1 секунду
    protected float fireCountDown = 0f;
    public GameObject bulletPrefab;

    [Header("Unity Setup Fields")]
    protected Vector3 startHelpFirePoint;

    private bool shotFired = false;
    private bool moveBack = true;

    void Update()
    {
        if (shotFired)
            RecoilAfterShoot();

        fireCountDown -= Time.deltaTime;

        //Если нет цели - не поворачиваемся и не стреляем
        if (targetForShooting == null)
        {
            return;
        }


        if (ReadyToShoot())
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                InitializedBeforeRecoil();
                fireCountDown = 1f / fireRate;
            }
        }

        LockOnTarget();
    }

    protected override void Shoot()
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
}
