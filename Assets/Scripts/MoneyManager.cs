using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [Header("Money")]
    [SerializeField] private int money = 0;
    
    public static MoneyManager InstanceMoneyManager;
    [SerializeField] private TMP_Text moneyText;

    private void Awake()
    {
        Time.timeScale = 1f;
        
        if (InstanceMoneyManager != null && InstanceMoneyManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceMoneyManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyText.text = money.ToString();
    }
    
    public void SubtractMoney(int amount)
    {
        money -= amount;
        moneyText.text = money.ToString();
    }
    
    public int GetMoney()
    {
        return money;
    }
}
