using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    [SerializeField]
    [Header("Manage Queue")]
    private Transform[] Waypoints;

    [SerializeField]
    private int CharacterLimit = 5;             //Nombre max de personnages visibles à l'écran
    private int CharacterOutsideQueue = 0;      //Nombre de personnages hors de l'écran

    [Header("Spawn")]
    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private int MaxCharacterSpawned;        //Nombre de personnages à faire spawn au total
    private int CharacterSpawned = 0;       //Nombre de personnages spawn

    [SerializeField]
    private float cooldown = 5f;
    private float timer = 0f;

    [SerializeField]
    private bool shouldSpawnCharacter = false;

    private List<Character> spawnedCharacters = new List<Character>();      //Personnages actuellement dans la file

    void Start()
    {

    }

    private void Update()
    {
        ManageSpawn();
    }

    private void ManageSpawn()
    {
        //Si il y a un perso hors de la file et qu'elle n'est pas pleine, on le fait spawn
        if (!IsFull() && IsACharacterWaitingOutside())
        {
            CharacterOutsideQueue--;
            SpawnCharacter();
        }

        if (shouldSpawnCharacter)
        {
            timer += Time.deltaTime;
            if (timer >= cooldown)
            {
                timer = 0;

                CharacterSpawned++;

                //Arreter de spawn si on atteint la limite
                if (CharacterSpawned >= MaxCharacterSpawned)
                {
                    shouldSpawnCharacter = false;
                }

                //Si il y a de la place sur l'écran on spawn le personnage, sinon on le stock ailleurs
                if (spawnedCharacters.Count < CharacterLimit)
                {
                    SpawnCharacter();
                }
                else
                {
                    CharacterOutsideQueue++;
                }
            }
        }
    }

    void SpawnCharacter(bool favouriteAttraction = false)
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab assigné dans characterPrefabs !");
            return;
        }

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

                //Choisir une attraction favorite au hasard
                if (favouriteAttraction)
                {
                    characterScript.SetRandomFavouriteAttraction();
                }
            }
            else
            {
                Debug.LogWarning("Le prefab instancié ne contient pas de script Characters !");
            }
        }
        else
        {
            Debug.LogWarning("Le prefab sélectionné est null !");
        }
    }

    private bool IsFull()
    {
        return spawnedCharacters.Count >= CharacterLimit;
    }

    private bool IsACharacterWaitingOutside()
    {
        return CharacterOutsideQueue > 0;
    }
}
