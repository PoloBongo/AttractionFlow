using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionQueue : ParentQueue
{
    [SerializeField] private Queue queue;
    [SerializeField] private OpenAttraction openAttraction;

    [Header("Gestion de la Queue")]
    [SerializeField] private Transform tempWaypoint;
    [SerializeField] private Transform attractionWaypoint;

    [SerializeField] private float cooldown = 2f;
    private float timer = 0f;

    [SerializeField] private float attractionCooldown = 10f;
    [SerializeField] private float minCooldown = 5f;
    [SerializeField] private float cooldownDiminishingRate = 0.1f;
    private float attractionTimer = 0f;

    [SerializeField] private float targetRotationPlayer;
    private float rotationSpeed = 5f;

    [SerializeField] private UICooldown ui;

    private void Start()
    {
        timer = cooldown;
        attractionTimer = attractionCooldown;
    }
    private void Update()
    {
        ui.UpdateSlider(attractionTimer, attractionCooldown);

        ManageAttraction();

        timer += Time.deltaTime;
        if (openAttraction.GetIsOpen() && !IsFull() && !queue.IsEmpty() && timer >= cooldown && queue.IsPlayerAtEntrance())
        {
            timer = 0;
            TransferCharacterToAttraction();
        }

        RotateCharacters();

        UpdateCooldown();
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
        character.SetWaypoint(waypoints[waypointID], waypointID);
        character.SetWaypoint(tempWaypoint, -1, true);

        AssignCharacterMood(character.gameObject, this);
    }

    private void ManageAttraction()
    {
        attractionTimer += Time.deltaTime;
        Character character = GetFirstCharacterInQueue();

        if (!IsEmpty() && attractionTimer >= attractionCooldown && Vector3.Distance(character.transform.position, waypoints[0].position) <= 0.05)
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
    private void UpdateCooldown()
    {
        attractionCooldown -= cooldownDiminishingRate * Time.deltaTime;
        if (attractionCooldown <= minCooldown)
        {
            attractionCooldown = minCooldown;
        }
    }
}
