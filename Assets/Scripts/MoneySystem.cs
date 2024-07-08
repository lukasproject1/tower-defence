using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem instance;

    public int money = 200;
    public TextMeshProUGUI moneyText;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one MoneySystem in scene!");
            return;
        }
        instance = this;
    }

    void Start()
    {
        UpdateMoneyText();
    }

    void Update()
    {
        // Optional: If you have any other updates related to money, handle them here
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyText();
        Debug.Log("Money: " + money);
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Money: " + money;
    }
}
