using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    [Header("Setup Fields")]
    public WaveBlueprint[] waves;

    public Transform spawnPoint;
    //Надпись отсчёта времени между волнами
    public Text nextWaveTimerText;
    public GameManager gameManager;

    private float timeBetweenWaves = 3;
    //отсчёт для следующей волны
    private float countDown = 5;
    private int waveIndex = 0;

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }

        if (countDown <= 0f)
        {
            //корутину можно применять для длительных операций, которые можно "размазать" по кадрам,
            //от которых главный поток не повиснет. 
            //Для некой отдельной микропрограммки, которая будет работать параллельно 
            //(пример с миганием спрайта), которую сложно зарепитить из-за разности действий.
            StartCoroutine(Spawn());
            countDown = timeBetweenWaves;
            return;
        }
        //Отсчёт каждой секунды
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0, Mathf.Infinity);
        nextWaveTimerText.text = "Next wave in " + string.Format("{0:00.00}", countDown);
    }

    IEnumerator Spawn()
    {
        PlayerStats.Rounds++;

        WaveBlueprint wave = waves[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            //Задержка перед созданием следующего противника, без зависания основного потока
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        //Создание шаблона противника со стартовой позиции
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}