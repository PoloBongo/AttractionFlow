using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulling : MonoBehaviour
{
    private List<Character> characterStored = new List<Character>();

    [SerializeField]
    private int maxPull = 10;

    [SerializeField]
    private Transform position;

    public void AddCharacter(Character newCharacter)
    {
        if (characterStored.Count < maxPull)
        {
            characterStored.Add(newCharacter);
            if (position != null)
            {
                newCharacter.transform.position = position.position;
                newCharacter.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            else
            {
                Debug.LogWarning("Position transform is not set in Pulling script.");
            }
            newCharacter.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Pulling is full");
            Destroy(newCharacter);
        }
    }

    public Character RetrieveCharacter()
    {
        if (IsEmpty())
        {
            Debug.LogWarning("Pulling is empty! Cannot retrieve character.");
            return null;
        }

        Character characterToRetrieve = characterStored[0];
        characterStored.RemoveAt(0);
        characterToRetrieve.gameObject.SetActive(true);
        characterToRetrieve.Reset();
        return characterToRetrieve;
    }

    public bool IsEmpty()
    {
        return characterStored.Count == 0;
    }
}
