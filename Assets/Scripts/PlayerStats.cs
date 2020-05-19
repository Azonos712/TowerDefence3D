using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 300;

    public static int Lives;
    public int startLives = 10;

    public static int Rounds;

    public Text livesText;
    public Text moneyText;

    private void Start()
    {
        Money = startMoney;
        Lives = startLives;
        Rounds = 0;
    }

    void Update()
    {
        moneyText.text = "$" + PlayerStats.Money.ToString();
        livesText.text = "Lives: " + PlayerStats.Lives.ToString();
    }
}
