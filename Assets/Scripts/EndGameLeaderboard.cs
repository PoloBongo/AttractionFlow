using System;
using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using TMPro;
using UnityEngine;

public class EndGameLeaderboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField _playerUsernameInput;
    [SerializeField] private Leaderboard leaderboard;

    private void Start()
    {
        LeaderboardCreator.ResetPlayer();
        Found();
    }
    
    private void Found()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("fix");
        leaderboard = obj.GetComponent<Leaderboard>();
    }
    
    public void Submit()
    {
        LeaderboardCreator.UploadNewEntry(leaderboard._leaderboardPublicKey, _playerUsernameInput.text, leaderboard._playerScore, OnUploadComplete, Debug.Log);
        LeaderboardCreator.ResetPlayer();
    }

    private void OnUploadComplete(bool success)
    {
        if (success) leaderboard.Load();
    }
}
