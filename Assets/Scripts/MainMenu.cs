using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject leaderBoardButton;
    void Start() {
        leaderBoardButton.GetComponent<Button>().interactable = false;
        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        // END THE CODE TO PASTE INTO START
            SignIn();
    }

    public void SignIn() {
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                Debug.Log("logged in");
                leaderBoardButton.GetComponent<Button>().interactable = true;
            }
            else {
                Debug.Log("fodeu de novo");
                leaderBoardButton.GetComponent<Button>().interactable = false;
            }
        }
        );
    }

    public void ShowLeaderboards() {
        if (Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        } else {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }
}
