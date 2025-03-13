using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    [SerializeField]
    [Header("Manage Queue")]
    private Transform[] Waypoints;

    [SerializeField]
    private int CharacterLimit = 5;             //Nombre max de personnages visibles � l'�cran
    private int CharacterOutsideQueue = 0;      //Nombre de personnages hors de l'�cran

    [Header("Spawn")]
    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private int MaxCharacterSpawned;        //Nombre de personnages � faire spawn au total
    private int CharacterSpawned = 0;       //Nombre de personnages spawn

    [SerializeField]
    private float cooldown = 5f;
    private float timer = 0f;

    [SerializeField]
    private bool shouldSpawnCharacter = false;

    [SerializeField]
    List<Character> spawnedCharacters = new List<Character>();      //Personnages actuellement dans la file

    void Start()
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogError("Aucun prefab assign� dans characterPrefabs ! D�sactivation du spawn.");
            shouldSpawnCharacter = false;
        }
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

                //Arreter de spawn si on atteint la limite
                if (CharacterSpawned >= MaxCharacterSpawned)
                {
                    shouldSpawnCharacter = false;
                    Debug.Log("Fin du spawn : tous les personnages ont �t� g�n�r�s.");
                    return;
                }
                CharacterSpawned++;

                //Si il y a de la place sur l'�cran on spawn le personnage, sinon on le stock ailleurs
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

    private void SpawnCharacter(bool favouriteAttraction = false)
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint non assign� !");
            return;
        }

        GameObject characterPrefab = characterPrefabs[Random.Range(0, characterPrefabs.Length)];

        if (characterPrefab != null)
        {
            GameObject newCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            Character characterScript = newCharacter.GetComponent<Character>();

            if (characterScript != null)
            {
                //Choisir le waypoint initial
                spawnedCharacters.Add(characterScript);
                int waypointID = spawnedCharacters.Count - 1;
                characterScript.SetWaypoint(Waypoints[waypointID], waypointID);

                //Choisir une attraction favorite au hasard
                if (favouriteAttraction)
                {
                    characterScript.SetRandomFavouriteAttraction();
                }
            }
            else
            {
                Debug.LogWarning("Le prefab instanci� ne contient pas de script Characters !");
            }
        }
        else
        {
            Debug.LogWarning("Le prefab s�lectionn� est null !");
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
            int currentWaypoint = character.GetWaypointID() -1;
            Debug.Log(currentWaypoint);
            character.SetWaypoint(Waypoints[currentWaypoint], currentWaypoint);
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

    public bool IsEmpty()
    {
        return spawnedCharacters.Count == 0;
    }

    public Character GetFirstCharacterInQueue()
    {
        return spawnedCharacters[0];
    }

}
