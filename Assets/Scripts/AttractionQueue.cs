using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionQueue : ParentQueue
{
    [SerializeField]
    private ParentQueue queue;

    [SerializeField]
    private OpenAttraction openAttraction;

    [Header("Gestion de la Queue")]
    [SerializeField]
    private Transform tempWaypoint;

    [SerializeField]
    private Transform attractionWaypoint;

    [SerializeField]
    private float cooldown = 5f;        //Temps avant que quelqu'un aille dans la queue
    private float timer = 0f;

    [SerializeField]
    private float attractionCooldown = 10f;         //Temps d'attente entre chaque personne
    private float attractionTimer = 0f;

    [SerializeField]
    private float targetRotationPlayer;
    
    private Quaternion RotationPlayer;
    private float rotationSpeed = 5f;
    
    private void Update()
    {
        ManageAttraction();
        if (openAttraction.GetIsOpen() && !IsFull() && !queue.IsEmpty())
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                TransferCharacterToAttraction();
            }
        }
        
        foreach (Character character in spawnedCharacters)
        {
            character.transform.rotation = Quaternion.Lerp(
                character.transform.rotation, 
                Quaternion.Euler(0, targetRotationPlayer, 0), 
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void TransferCharacterToAttraction()
    {
        Character character = queue.GetFirstCharacterInQueue();

        if (character != null)
        {
            // Ajouter le personnage � la queue de l'attraction
            spawnedCharacters.Add(character);
            queue.RemoveCharacterFromQueue(character);
            int waypointID = spawnedCharacters.Count - 1;
            character.SetWaypoint(tempWaypoint, waypointID);
            character.SetWaypoints(Waypoints);

            // Assigner le CharacterMood � la nouvelle queue d'attraction
            CharacterMood characterMood = character.GetComponent<CharacterMood>();
            if (characterMood != null)
            {
                characterMood.SetQueue(this);
            }
            else
            {
                Debug.LogWarning("Le prefab ne contient pas de script CharacterMood !");
            }
        }
        else
        {
            Debug.LogWarning("Aucun personnage � transf�rer.");
        }
    }

    private void ManageAttraction()
    {
        attractionTimer += Time.deltaTime;
        Character character = GetFirstCharacterInQueue();

        if (!IsEmpty() && attractionTimer >= attractionCooldown && character.transform.position == Waypoints[0].transform.position)
        {
            attractionTimer = 0;

            if (character != null)
            {
                // Ajouter le personnage � la queue de l'attraction
                RemoveCharacterFromQueue(character);
                character.SetWaypoint(attractionWaypoint, -1);

                // Assigner le CharacterMood � la nouvelle queue d'attraction
                CharacterMood characterMood = character.GetComponent<CharacterMood>();
                if (characterMood != null)
                {
                    characterMood.SetQueue(null);
                }
                else
                {
                    Debug.LogWarning("Le prefab ne contient pas de script CharacterMood !");
                }
            }
            else
            {
                Debug.LogWarning("Aucun personnage � transf�rer.");
            }
        }
    }
}
