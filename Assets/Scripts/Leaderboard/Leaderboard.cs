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
    [SerializeField] private listInputs list;

    public int _playerScore;
    
    private void Start()
    {
        if(list) _entryFields = list._entryFields;
        LeaderboardCreator.ResetPlayer();
        Load();
    }
    
    public void LOad2()
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
        if (!list) return;
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
