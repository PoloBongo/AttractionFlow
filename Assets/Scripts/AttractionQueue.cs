using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttractionQueue : MonoBehaviour
{
    [SerializeField]
    private Queue queue;

    [SerializeField]
    private OpenAttraction openAttraction;

    [SerializeField]
    [Header("Manage Queue")]
    private Transform[] Waypoints;
    [SerializeField]
    private Transform tempWaypoint;

    [SerializeField]
    private int CharacterLimit = 5;         //Nombre max de personnages visibles � l'�cran

    [SerializeField]
    private float cooldown = 5f;
    private float timer = 0f;

    [SerializeField]
    List<Character> spawnedCharacters = new List<Character>();      //Personnages actuellement dans la file

    [SerializeField]
    private float targetRotationPlayer;
    
    private Quaternion RotationPlayer;
    private float rotationSpeed = 5f;
    
    private void Update()
    {
        ManageSpawn();
        
        foreach (Character character in spawnedCharacters)
        {
            character.transform.rotation = Quaternion.Lerp(
                character.transform.rotation, 
                Quaternion.Euler(0, targetRotationPlayer, 0), 
                Time.deltaTime * rotationSpeed
            );
        }
    }
    private void ManageSpawn()
    {
        //Si il y a un perso hors de la file, qu'elle n'est pas pleine et que la porte est ouverte, on le fait venir
        if (!IsFull() && openAttraction.GetIsOpen() && !queue.IsEmpty())
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;

                GetCharacter();
            }
        }
    }

    private void GetCharacter()
    {
        Character character = queue.GetFirstCharacterInQueue();

        if (character != null)
        {
            //Choisir le waypoint initial
            spawnedCharacters.Add(character);
            queue.RemoveCharacterFromQueue(character);
            int waypointID = spawnedCharacters.Count - 1;
            character.SetWaypoint(tempWaypoint, waypointID);
            character.SetWaypoints(Waypoints);
        }
        else
        {
            Debug.LogWarning("Le prefab instanci� ne contient pas de script Characters !");
        }
    }

    public Character RemoveCharacterFromQueue(Character character = null)
    {
        if (spawnedCharacters.Count == 0)
        {
            Debug.LogWarning("Aucun personnage � retirer de la file !");
            return null;
        }

        // Si aucun personnage sp�cifi�, on enl�ve le premier
        Character characterToRemove = character ?? spawnedCharacters[0];
        spawnedCharacters.Remove(characterToRemove);

        // D�placer les personnages restants
        int startPoint = characterToRemove.GetWaypointID();
        MoveAllCharactersForward(startPoint);

        return characterToRemove;
    }

    private void MoveAllCharactersForward(int startPoint)
    {

        //Tous les persos � partir de ce point avance au prochain waypoint
        for (int i = startPoint; i < spawnedCharacters.Count; i++)
        {
            Character character = spawnedCharacters[i];
            int currentWaypoint = character.GetWaypointID() - 1;
            Debug.Log(currentWaypoint);
            character.SetWaypoint(Waypoints[currentWaypoint], currentWaypoint);
        }
    }

    private bool IsFull()
    {
        return spawnedCharacters.Count >= CharacterLimit;
    }
}
