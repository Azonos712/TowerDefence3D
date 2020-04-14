using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 7;
    public Text nextWaveTimerText;

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
        nextWaveTimerText.text = Mathf.Round(countDown).ToString();
    }

    IEnumerator Spawn()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            //Задержка перед созданием следующего противника, без зависания основного потока
            yield return new WaitForSeconds(0.9f);
        }
    }

    void SpawnEnemy()
    {
        //Создание шаблона противника со стартовой позиции
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}