using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public GameObject impactEffect;
    private Transform target;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
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
    }

    void HitTarget()
    {
        GameObject effectIns = Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 2f);
        Destroy(target.gameObject);
        //Destroy(gameObject);
        target = null;
    }
}