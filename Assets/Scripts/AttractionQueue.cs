using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionQueue : ParentQueue
{
    [SerializeField] private ParentQueue queue;
    [SerializeField] private OpenAttraction openAttraction;

    [Header("Gestion de la Queue")]
    [SerializeField] private Transform tempWaypoint;
    [SerializeField] private Transform attractionWaypoint;

    [SerializeField] private float cooldown = 2f;
    private float timer = 0f;

    [SerializeField] private float attractionCooldown = 10f;
    private float attractionTimer = 0f;

    [SerializeField] private float targetRotationPlayer;
    private float rotationSpeed = 5f;

    private void Update()
    {
        ManageAttraction();

        timer += Time.deltaTime;
        if (openAttraction.GetIsOpen() && !IsFull() && !queue.IsEmpty() && timer >= cooldown)
        {
            timer = 0;
            TransferCharacterToAttraction();
        }

        RotateCharacters();
    }

    private void RotateCharacters()
    {
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

        if (character == null)
        {
            Debug.LogWarning("Aucun personnage à transférer.");
            return;
        }

        spawnedCharacters.Add(character);
        queue.RemoveCharacterFromQueue(character);

        int waypointID = spawnedCharacters.Count - 1;
        character.SetWaypoint(tempWaypoint, waypointID);
        character.SetWaypoints(waypoints);

        AssignCharacterMood(character.gameObject, this);
    }

    private void ManageAttraction()
    {
        attractionTimer += Time.deltaTime;
        Character character = GetFirstCharacterInQueue();

        if (!IsEmpty() && attractionTimer >= attractionCooldown && character.transform.position == waypoints[0].position)
        {
            attractionTimer = 0;
            RemoveCharacterFromQueue(character);
            character.SetWaypoint(attractionWaypoint, -1);
            AssignCharacterMood(character.gameObject, null);
        }
    }

    private void AssignCharacterMood(GameObject characterObject, ParentQueue queue = null)
    {
        CharacterMood characterMood = characterObject.GetComponent<CharacterMood>();
        if (characterMood != null)
        {
            characterMood.SetQueue(queue);
        }
    }
}
