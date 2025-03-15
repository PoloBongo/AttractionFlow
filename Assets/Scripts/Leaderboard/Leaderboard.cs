using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    public string _leaderboardPublicKey =
        "bba6e67ef98d2422fd1508d3ea56b724d51fee23aa4b797b4d3003a1faf1b9f8";

    [SerializeField] private TMP_InputField[] _entryFields;

    public int _playerScore;
    
    public static Leaderboard InstanceLeaderboard; 
    
    private void Awake()
    {
        if (InstanceLeaderboard != null && InstanceLeaderboard != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceLeaderboard = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        LeaderboardCreator.ResetPlayer();
        Load();
    }

    public void AddPlayerScore(int _score)
    {
        _playerScore+=_score;
    }

    public void Load() => LeaderboardCreator.GetLeaderboard(_leaderboardPublicKey, OnLeaderboardLoaded);
    
    private void OnLeaderboardLoaded(Entry[] entries)
    {
        foreach (var entryField in _entryFields)
        {
            entryField.text = "";
        }

        for (int i = 0; i < entries.Length; i++)
        {
            _entryFields[i].text = $"{i + 1}. {entries[i].Username} : {entries[i].Score}";
        }
    }
}
