using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Type
{
    ENTER,
    LEAVE,
}

public class AttractionTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Type type;

    private void Awake()
    {
        if (GetComponent<BoxCollider>() != null)
        {
            boxCollider = GetComponent<BoxCollider>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<MoneyManager>() != null)
        {
            moneyManager = FindObjectOfType<MoneyManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character") && moneyManager)
        {
            if (type == Type.ENTER)
            {
                moneyManager.AddMoney(10);
            }
            else if (type == Type.LEAVE)
            {
                moneyManager.SubtractMoney(10);
            }
            
        }
    }
}
