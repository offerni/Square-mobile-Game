using GooglePlayGames;
using GooglePlayGames.BasicApi;
using SquareConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject leaderBoardButton;
    [SerializeField] GameObject achievementsButton;
    void Start() {
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
        StopAllCoroutines();
        StartCoroutine(CheckLoginStatus());

    }

    public void SignIn() {
        Social.localUser.Authenticate((bool success) => {
            if (success) {
                //Debug.Log("logged in");
                SocialButtonsHandler(true);
            } else {
                //Debug.Log("loginr error");
                SocialButtonsHandler(false);
            }
        }
        );
    }

    private void SocialButtonsHandler(bool status) {
        leaderBoardButton.GetComponent<Button>().interactable = status;
        achievementsButton.GetComponent<Button>().interactable = status;
    }

    public void ShowLeaderboards() {
        if (Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        } else {
            //Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    public void ShowAchievements() {
        if (Social.localUser.authenticated) {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        } else {
            //Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }

    IEnumerator CheckLoginStatus() {
        while (true) {
            if(Social.localUser.authenticated) {
                SocialButtonsHandler(true);
                //print("ok");
            } else {
                SocialButtonsHandler(false);
                //print("not ok");
            }
            yield return new WaitForSeconds(5);
        }
        
    }

}

