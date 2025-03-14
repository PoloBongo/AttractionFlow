using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentQueue : MonoBehaviour
{
    [Header("Gestion de la Queue")]
    [SerializeField]
    protected Transform[] Waypoints;

    [SerializeField]
    protected int CharacterLimit = 5;       // Nombre max de personnages sur l'écran

    [SerializeField]
    protected List<Character> spawnedCharacters = new List<Character>();        // Liste des personnages dans la file

    public Character RemoveCharacterFromQueue(Character character = null)
    {
        if (spawnedCharacters.Count == 0)
        {
            Debug.LogWarning("Aucun personnage à retirer de la file.");
            return null;
        }
    
        // Si aucun personnage spécifié, on enlève le premier personnage
        Character characterToRemove = character ?? spawnedCharacters[0];
        spawnedCharacters.Remove(characterToRemove);

        // Déplacer les personnages restants
        int startPoint = characterToRemove.GetWaypointID();
        MoveAllCharactersForward(startPoint);

        return characterToRemove;
    }

    protected void MoveAllCharactersForward(int startPoint)
    {
        for (int i = startPoint; i < spawnedCharacters.Count; i++)
        {
            Character character = spawnedCharacters[i];
            int currentWaypoint = character.GetWaypointID() - 1;
            character.SetWaypoint(Waypoints[currentWaypoint], currentWaypoint);
        }
    }

    protected bool IsFull()
    {
        return spawnedCharacters.Count >= CharacterLimit;
    }

    public bool IsEmpty()
    {
        return spawnedCharacters.Count == 0;
    }

    public Character GetFirstCharacterInQueue()
    {
        return spawnedCharacters.Count > 0 ? spawnedCharacters[0] : null;
    }
}
