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
    [SerializeField] private GameObject spawnVFX;

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
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character") && moneyManager)
        {
            if (type == Type.Enter)
            {
                moneyManager.AddMoney((int)attraction);
                CharacterMood characterMood = other.GetComponent<CharacterMood>();
                Character character = other.GetComponent<Character>();

                if (characterMood != null)
                {
                    characterMood.BeHappy();
                    if (spawnVFX != null)
                    {
                        GestionVFX.InstanceGestionVFX.PlayVFX(0, spawnVFX);
                    }
                    character.AddToPulling();
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
