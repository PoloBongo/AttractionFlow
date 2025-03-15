using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private string _leaderboardPublicKey =
        "bba6e67ef98d2422fd1508d3ea56b724d51fee23aa4b797b4d3003a1faf1b9f8";

    [SerializeField] private TMP_Text _playerScoreText;
    [SerializeField] private TMP_InputField[] _entryFields;
    [SerializeField] private TMP_InputField _playerUsernameInput;

    private int _playerScore;
    
    private void Start()
    {
        LeaderboardCreator.ResetPlayer();
        Load();
    }

    public void AddPlayerScore()
    {
        _playerScore++;
        _playerScoreText.text = "Your score: " + _playerScore;
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
    
    public void Submit()
    {
        LeaderboardCreator.UploadNewEntry(_leaderboardPublicKey, _playerUsernameInput.text, _playerScore, OnUploadComplete, Debug.Log);
        LeaderboardCreator.ResetPlayer();
    }

    private void OnUploadComplete(bool success)
    {
        if (success) Load();
    }
}
