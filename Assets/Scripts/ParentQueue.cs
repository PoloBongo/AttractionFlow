using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentQueue : MonoBehaviour
{
    [Header("Gestion de la Queue")]
    [SerializeField] protected Transform[] waypoints;
    [SerializeField] protected int characterLimit = 5;
    [SerializeField] protected List<Character> spawnedCharacters = new List<Character>();

    public Character RemoveCharacterFromQueue(Character character = null)
    {
        if (spawnedCharacters.Count == 0)
        {
            Debug.LogWarning("Aucun personnage à retirer de la file.");
            return null;
        }

        Character characterToRemove = character ?? spawnedCharacters[0];
        spawnedCharacters.Remove(characterToRemove);

        MoveAllCharactersForward(characterToRemove.GetWaypointID());

        return characterToRemove;
    }

    protected void MoveAllCharactersForward(int startPoint)
    {
        for (int i = startPoint; i < spawnedCharacters.Count; i++)
        {
            Character character = spawnedCharacters[i];
            int newWaypointID = character.GetWaypointID() - 1;
            character.SetWaypoint(waypoints[newWaypointID], newWaypointID);
        }
    }

    protected bool IsFull() => spawnedCharacters.Count >= characterLimit;
    public bool IsEmpty() => spawnedCharacters.Count == 0;
    public Character GetFirstCharacterInQueue() => spawnedCharacters.Count > 0 ? spawnedCharacters[0] : null;
}
