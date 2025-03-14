using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : ParentQueue
{
    [Header("Gestion de la Queue")]
    [SerializeField]
    private int CharacterOutsideQueue = 0; // Nombre de personnages hors de la file

    [Header("Spawn")]
    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private int MaxCharacterSpawned = 10; // Nombre total de personnages à faire spawn

    private int CharacterSpawned = 0; // Nombre de personnages spawnés

    [SerializeField]
    private float cooldown = 5f; // Temps entre chaque spawn

    private float timer = 0f;

    [SerializeField]
    private bool shouldSpawnCharacter = false;

    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Aucun prefab de personnage assigné. Désactivation du spawn.");
            shouldSpawnCharacter = false;
        }
    }

    void Update()
    {
        if (shouldSpawnCharacter)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;
                ManageCharacterSpawn();
            }
        }
    }

    private void ManageCharacterSpawn()
    {
        // Si la file n'est pas pleine et qu'un personnage attend à l'extérieur, on le fait spawn
        if (!IsFull() && IsACharacterWaitingOutside())
        {
            CharacterOutsideQueue--;
            SpawnCharacter();
        }
        else
        {
            // Si de l'espace est disponible, on fait spawn un personnage
            if (CharacterSpawned < MaxCharacterSpawned && spawnedCharacters.Count < CharacterLimit)
            {
                CharacterSpawned++;
                SpawnCharacter();
            }
            else if (CharacterSpawned >= MaxCharacterSpawned)
            {
                Debug.Log("Limite de spawn atteinte.");
                shouldSpawnCharacter = false;
            }
            else
            {
                // Si la file est pleine, on incrémente le nombre de personnages en attente
                CharacterOutsideQueue++;
            }
        }
    }

    private void SpawnCharacter()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint non assigné !");
            return;
        }

        GameObject characterPrefab = characterPrefabs[Random.Range(0, characterPrefabs.Length)];

        if (characterPrefab != null)
        {
            GameObject newCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            Character characterScript = newCharacter.GetComponent<Character>();

            if (characterScript != null)
            {
                spawnedCharacters.Add(characterScript);
                int waypointID = spawnedCharacters.Count - 1;
                characterScript.SetWaypoint(Waypoints[waypointID], waypointID);
            }
            else
            {
                Debug.LogWarning("Le prefab instancié ne contient pas de script Character !");
            }

            CharacterMood characterMood = newCharacter.GetComponent<CharacterMood>();
            if (characterMood != null)
            {
                characterMood.SetQueue(this);
            }
            else
            {
                Debug.LogWarning("Le prefab instancié ne contient pas de script CharacterMood !");
            }
        }
        else
        {
            Debug.LogWarning("Le prefab sélectionné est null !");
        }
    }

    private bool IsACharacterWaitingOutside()
    {
        return CharacterOutsideQueue > 0;
    }
}
