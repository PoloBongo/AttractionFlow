using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Attraction
{
    Attraction1 = 1,
    Attraction2 = 2,
    Attraction3 = 3
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
    [SerializeField] private GameObject spawnText;

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
            Character character = other.GetComponent<Character>();

            if (type == Type.Enter)
            {
                int bonus = 1;

                if ((int)character.GetFavouriteAttraction() == (int)attraction)
                {
                    bonus = 4;
                }

                moneyManager.AddMoney((int)attraction * 10 * bonus);
                CharacterMood characterMood = other.GetComponent<CharacterMood>();

                if (spawnText != null)
                {
                    DOTweenText.InstanceDOTweenText.PlayText(100, spawnText);
                }
                
                if (characterMood != null)
                {
                    characterMood.BeHappy();
                    
                    if (spawnVFX != null)
                    {
                        GestionVFX.InstanceGestionVFX.PlayVFX(0, spawnVFX);
                    }
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

            character.StartCoroutine(character.WaitAndDelete());

        }
    }
}
