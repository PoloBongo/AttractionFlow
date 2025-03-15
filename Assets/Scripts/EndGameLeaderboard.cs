using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

public class EndGameLeaderboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerUsernameInput;

    private void Start()
    {
        LeaderboardCreator.ResetPlayer();
    }
    
    public void Submit()
    {
        LeaderboardCreator.UploadNewEntry(Leaderboard.InstanceLeaderboard._leaderboardPublicKey, _playerUsernameInput.text, Leaderboard.InstanceLeaderboard._playerScore, OnUploadComplete, Debug.Log);
        LeaderboardCreator.ResetPlayer();
    }

    private void OnUploadComplete(bool success)
    {
        if (success) Leaderboard.InstanceLeaderboard.Load();
    }
}
