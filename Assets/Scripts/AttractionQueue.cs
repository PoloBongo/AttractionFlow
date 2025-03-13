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
    private int CharacterLimit = 5;         //Nombre max de personnages visibles à l'écran

    [SerializeField]
    private float cooldown = 5f;
    private float timer = 0f;

    [SerializeField]
    List<Character> spawnedCharacters = new List<Character>();      //Personnages actuellement dans la file

    private void Update()
    {
        ManageSpawn();
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
            character.SetWaypoint(Waypoints[waypointID], waypointID);
        }
        else
        {
            Debug.LogWarning("Le prefab instancié ne contient pas de script Characters !");
        }
    }

    public Character RemoveCharacterFromQueue(Character character = null)
    {
        if (spawnedCharacters.Count == 0)
        {
            Debug.LogWarning("Aucun personnage à retirer de la file !");
            return null;
        }

        // Si aucun personnage spécifié, on enlève le premier
        Character characterToRemove = character ?? spawnedCharacters[0];
        spawnedCharacters.Remove(characterToRemove);

        // Déplacer les personnages restants
        int startPoint = characterToRemove.GetWaypointID();
        MoveAllCharactersForward(startPoint);

        return characterToRemove;
    }

    private void MoveAllCharactersForward(int startPoint)
    {

        //Tous les persos à partir de ce point avance au prochain waypoint
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
