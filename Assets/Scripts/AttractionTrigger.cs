using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attraction
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
    [SerializeField] private DOTweenText dotweenText;
    [SerializeField] private Leaderboard leaderboard;

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
        
        Found();
    }

    private void Found()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("fix");
        leaderboard = obj.GetComponent<Leaderboard>();
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

                moneyManager.AddMoney((int)attraction * bonus);
                leaderboard.AddPlayerScore((int)attraction * bonus);
                CharacterMood characterMood = other.GetComponent<CharacterMood>();

                if (spawnText != null)
                {
                    dotweenText.PlayText((int)attraction * bonus, spawnText);
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
