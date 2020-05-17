using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Header("Setup Fields")]
    public Transform enemyPrefab;
    public Transform spawnPoint;
    //Надпись отсчёта времени между волнами
    public Text nextWaveTimerText;

    private float timeBetweenWaves = 10;
    //отсчёт для следующей волны
    private float countDown = 3;
    private int waveIndex = 0;

    private void Update()
    {
        if (countDown <= 0)
        {
            //корутину можно применять для длительных операций, которые можно "размазать" по кадрам,
            //от которых главный поток не повиснет. 
            //Для некой отдельной микропрограммки, которая будет работать параллельно 
            //(пример с миганием спрайта), которую сложно зарепитить из-за разности действий.
            StartCoroutine(Spawn());
            countDown = timeBetweenWaves;
        }
        //Отсчёт каждой секунды
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);
        string txt = "Next wave in " + string.Format("{0:00.00}", countDown);
        nextWaveTimerText.text = txt;
    }

    IEnumerator Spawn()
    {
        waveIndex++;
        PlayerStats.Rounds++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            //Задержка перед созданием следующего противника, без зависания основного потока
            yield return new WaitForSeconds(1.5f);
        }
    }

    void SpawnEnemy()
    {
        //Создание шаблона противника со стартовой позиции
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}