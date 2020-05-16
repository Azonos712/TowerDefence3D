using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 70f;
    public float explosionRadius = 0f;
    [Header("Unity Setup Fields")]
    public GameObject impactEffect;
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //Вычисляем направление пули
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //Проверка на длину расстояния полета
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        //Приводим вектор направления к нормализованному, чтобы не было разных скоростей
        //Далее перемножаем этот вектор на скорость и разность времени между кадрами
        //И перемещаем в мировых координатах при помощи вызванного метода
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        target = null;
    }

    void Explode()
    {
        //получаем список объектов задетых сферой
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var obj in hitObjects)
        {
            if (obj.tag == "Enemy")
            {
                Damage(obj.transform);
            }
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRadius);
    }
}