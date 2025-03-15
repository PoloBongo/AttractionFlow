using TMPro;
using UnityEngine;

public class ShowScoreEndGame : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;

    public void ShowText()
    {
        int newVal = MoneyManager.InstanceMoneyManager.GetMoney() - 100;
        tmp.text = "Save Your Score ! (" + newVal.ToString() + ")";
    }
}
