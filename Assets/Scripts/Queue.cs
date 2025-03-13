using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField]
    private GameObject[] characterPrefabs;

    [SerializeField]
    private Transform spawnPoint;

    private List<Characters> spawnedCharacters = new List<Characters>();

    void Start()
    {

    }

    void SpawnCharacter(bool favouriteAttraction = false)
    {
        if (characterPrefabs == null || characterPrefabs.Length == 0)
        {
            Debug.LogWarning("Aucun prefab assign� dans characterPrefabs !");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning("SpawnPoint non assign� !");
            return;
        }

        GameObject characterPrefab = characterPrefabs[Random.Range(0, characterPrefabs.Length)];

        if (characterPrefab != null)
        {
            GameObject newCharacter = Instantiate(characterPrefab, spawnPoint.position, Quaternion.identity);
            Characters characterScript = newCharacter.GetComponent<Characters>();

            if (characterScript != null)
            {
                spawnedCharacters.Add(characterScript);

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
}
