using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : ParentQueue
{
    [Header("Gestion de la Queue")]
    [SerializeField] private int characterOutsideQueue = 0;

    [Header("Spawn")]
    [SerializeField] private GameObject[] characterPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int maxCharacterSpawned = 10;
    private int characterSpawned = 0;

    [SerializeField] private float startCooldown = 5f;
    [SerializeField] private float minCooldown = 0.5f;
    [SerializeField] private float cooldownDiminishingRate = 0.2f;
    private float timer = 0f;
    private float globalTimer = 0f;

    [SerializeField] private bool shouldSpawnCharacter = false;
    [SerializeField] private Pulling pulling;
    [SerializeField] private Transform leaveWaypoint;

    [Header("Attraction Favorite")]
    [SerializeField] private int favouriteAttractionCooldown = 5;
    [SerializeField] private float favouriteAttractionOdds = 0.2f;
    private int favouriteAttractionTimer = 0;

    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Aucun prefab de personnage assigné. Désactivation du spawn.");
            shouldSpawnCharacter = false;
        }

        if (pulling == null)
        {
            Debug.LogWarning("Pulling non assigné.");
        }
    }

    void Update()
    {
        if (shouldSpawnCharacter)
        {
            ManageCharacterSpawn();
        }

        UpdateCooldown();
    }

    private void ManageCharacterSpawn()
    {
        timer += Time.deltaTime;

        if (!IsFull() && IsACharacterWaitingOutside())
        {
            characterOutsideQueue--;
            SpawnCharacter();

            return;
        }

        if (timer >= startCooldown)
        {
            if (characterSpawned < maxCharacterSpawned && spawnedCharacters.Count < characterLimit)
            {
                characterSpawned++;
                SpawnCharacter();
            }
            else if (characterSpawned >= maxCharacterSpawned)
            {
                Debug.Log("Limite de spawn atteinte.");
                shouldSpawnCharacter = false;
            }
            else
            {
                characterOutsideQueue++;
            }
            timer = 0;
        }
    }

    private void SpawnCharacter()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint non assigné !");
            return;
        }

        if (Pull()) return;

        GameObject characterPrefab = characterPrefabs[Random.Range(0, characterPrefabs.Length)];

        if (characterPrefab == null)
        {
            Debug.LogWarning("Le prefab sélectionné est null !");
            return;
        }

        GameObject newCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.Euler(0, -90, 0));
        InitializeCharacter(newCharacter);
    }

    private void InitializeCharacter(GameObject characterObject)
    {
        Character characterScript = characterObject.GetComponent<Character>();

        if (characterScript == null)
        {
            Debug.LogWarning("Le prefab instancié ne contient pas de script Character !");
            return;
        }

        spawnedCharacters.Add(characterScript);
        int waypointID = spawnedCharacters.Count - 1;
        characterScript.SetWaypoint(waypoints[waypointID], waypointID);
        characterScript.SetLeaveWaypoint(leaveWaypoint);

        if (ManageFavouriteAttraction())
        {
            characterScript.SetRandomFavouriteAttraction();
        }

        AssignCharacterMood(characterObject);
    }

    private void AssignCharacterMood(GameObject characterObject)
    {
        CharacterMood characterMood = characterObject.GetComponent<CharacterMood>();
        if (characterMood != null)
        {
            characterMood.SetQueue(this);
        }
        else
        {
            Debug.LogWarning("Le prefab instancié ne contient pas de script CharacterMood !");
        }
    }

    private bool IsACharacterWaitingOutside() => characterOutsideQueue > 0;

    private bool Pull()
    {
        if (pulling == null || pulling.IsEmpty()) return false;

        Character characterScript = pulling.RetrieveCharacter();
        if (characterScript == null)
        {
            Debug.LogWarning("Retrieved character is null !");
            return false;
        }

        characterScript.transform.position = spawnPoint.position;
        spawnedCharacters.Add(characterScript);
        int waypointID = spawnedCharacters.Count - 1;
        characterScript.SetWaypoint(waypoints[waypointID], waypointID);

        if (ManageFavouriteAttraction())
        {
            characterScript.SetRandomFavouriteAttraction();
        }

        AssignCharacterMood(characterScript.gameObject);
        return true;
    }

    private bool ManageFavouriteAttraction()
    {
        favouriteAttractionTimer++;
        if (favouriteAttractionTimer >= favouriteAttractionCooldown)
        {
            if (Random.value <= favouriteAttractionOdds)
            {
                favouriteAttractionTimer = 0;
                return true;
            }
        }
        return false;
    }

    private void UpdateCooldown()
    {
        globalTimer += Time.deltaTime;
        startCooldown -= cooldownDiminishingRate * Time.deltaTime;
        if (startCooldown <= minCooldown)
        {
            startCooldown = minCooldown;
        }
    }

    public bool IsPlayerAtEntrance()
    {
        if (Vector3.Distance(spawnedCharacters[0].transform.position, waypoints[0].transform.position) < 0.2f)
        {
            return true;
        }
        return false;
    }
}
