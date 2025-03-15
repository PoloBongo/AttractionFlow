using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFavourite : MonoBehaviour
{
    [Header("Property")][SerializeField] private Image handle;
    [Header("Emoji")][SerializeField] private List<Sprite> attractionTexture;

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, this.transform.parent.rotation.z * -1.0f + 90);
    }
    public void UpdateUI(FavouriteAttraction attraction)
    {
        int attractionID = (int)attraction -1;

        handle.sprite = attractionTexture[attractionID];
    }

    public void Reset()
    {
        gameObject.SetActive(false);
    }
}
