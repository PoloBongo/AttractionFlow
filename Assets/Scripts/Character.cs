using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Emotion
{
    NEUTRAL,
    IMPATIENT,
}

enum FavouriteAttraction
{
    NEUTRAL,
    ATTRACTION1,
    ATTRACTION2,
    ATTRACTION3
}

public class Character : MonoBehaviour
{
    private Emotion currentEmotion = Emotion.NEUTRAL;
    private FavouriteAttraction favouriteAttraction = FavouriteAttraction.NEUTRAL;

    void Start()
    {
        
    }

    public void SetRandomFavouriteAttraction()
    {
        favouriteAttraction = (FavouriteAttraction)Random.Range(1, 4);
    }
}
