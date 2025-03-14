using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Attraction
{
    Attraction1 = 10,
    Attraction2 = 20,
    Attraction3 = 30
}

enum Type
{
    Enter,
    Leave,
}

public class AttractionTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MoneyManager moneyManager;
    [SerializeField] private Type type;
    [SerializeField] private Attraction attraction;
    

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
            if (type == Type.Enter)
            {
                moneyManager.AddMoney((int)attraction);
                CharacterMood characterMood = other.GetComponent<CharacterMood>();
                if (characterMood != null)
                {
                    characterMood.BeHappy();
                }
                else
                {
                    Debug.Log("No character mood found");
                }
            }
            else if (type == Type.Leave)
            {
                moneyManager.SubtractMoney((int)attraction);
            }
            
        }
    }
}
