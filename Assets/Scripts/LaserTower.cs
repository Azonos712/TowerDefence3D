using UnityEngine;

public class LaserTower : Tower
{
    public float startDamageOverTime = 20;
    private float damageOverTime { get { return startDamageOverTime * level; } }
    public float startSlowAmount = .3f;
    private float slowAmount { get { return startSlowAmount * level; } }
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public AudioSource laserAudio;
    //Enemy oldEnemy;
    void Update()
    {
        //Если нет цели - не поворачиваемся и не стреляем|| oldEnemy!= targetEnemy
        if (targetForShooting == null)
        {
            if (lineRenderer.enabled)
                OffEffects();
            return;
        }

        if (changeEnemy == true)
            if (lineRenderer.enabled)
                OffEffects();

        if (ReadyToShoot())
        {
            Shoot();
        }

        LockOnTarget();
        //oldEnemy = targetEnemy;
    }

    protected override void Shoot()
    {
        changeEnemy = false;
        if (!lineRenderer.enabled)
            OnEffects();

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetForShooting.position);

        Vector3 dir = firePoint.position - targetForShooting.position;

        //смещение и поворот эффекта
        impactEffect.transform.position = targetForShooting.position + dir.normalized;
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);
    }

    void OnEffects()
    {
        laserAudio.Play();
        lineRenderer.enabled = true;
        impactEffect.Play();
        impactLight.enabled = true;
    }

    void OffEffects()
    {
        laserAudio.Stop();
        lineRenderer.enabled = false;
        impactEffect.Stop();
        impactLight.enabled = false;
    }
}
