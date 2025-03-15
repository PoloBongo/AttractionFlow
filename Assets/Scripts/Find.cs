using TMPro;
using UnityEngine;

public class Find : MonoBehaviour
{
    [SerializeField] private TMP_Text tmp;
    private void Start()
    {
        MoneyManager.InstanceMoneyManager.Find(tmp);
    }
}
