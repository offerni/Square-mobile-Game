using GooglePlayGames;
using GooglePlayGames.BasicApi;
using SquareConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameSession : MonoBehaviour {
    private int score;
    public int bestScore { get; set; }
    private int scoreAux;
    public int deadCount = 0;
    private AdManager adManager;
    public int adCount;

    private Dictionary<int, String> normalAchievementsDict = new Dictionary<int, string>() {
        {100,  GPGSIds.achievement_the_longest_journey_starts_with_a_single_step},
        {500,  GPGSIds.achievement_500_points_keep_going},
        {1000,  GPGSIds.achievement_1000_points},
        {3000,  GPGSIds.achievement_3000_points},
        {5000,  GPGSIds.achievement_nobody_really_came_that_far},
        {10000,  GPGSIds.achievement_bot}       
    };

    private Dictionary<int, String> impossibleAchievementsDict = new Dictionary<int, string>() {
        {100,  GPGSIds.achievement_impossible_100_points},
        {300,  GPGSIds.achievement_impossible_300_points},
        {500,  GPGSIds.achievement_impossible_500_points},
        {1000,  GPGSIds.achievement_impossible_you_beat_the_creator_high_score},
        {5000,  GPGSIds.achievement_you_should_stop_now}
    };

    private void Awake() {
        SetUpSingleton();
        RandomizeAdCount();
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int scorePoint) {

        this.score += scorePoint;
        if (this.score > this.bestScore) {
            bestScore = score;
        }
    }

    public void ResetScore() {
        this.score = 0;
    }

    public int GetBestScore() {
        if (Social.localUser.authenticated) {
            if (SceneManager.GetActiveScene().name == "Level1") {
                UnlockNormalAchievements(bestScore);

            } else if (SceneManager.GetActiveScene().name == "Level2") {
                UnlockImpossibleAchievements(bestScore);
            }
        }

        return this.bestScore;
    }

    private void SetUpSingleton() {
        int count = FindObjectsOfType<GameSession>().Length;
        if (count > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLeaderBoardBestScore() {
        bestScore = 0;
    }

    public void ResetDeadCount() {
        deadCount = 0;
    }

    public void RandomizeAdCount() {
        adCount = UnityEngine.Random.Range(4, 8);
    }

    public void SaveScore() {
        if (Social.localUser.authenticated) {
            // Note: make sure to add 'using GooglePlayGames'
            if (SceneManager.GetActiveScene().name == "Level1") {
                PlayGamesPlatform.Instance.ReportScore(score,
                GPGSIds.leaderboard_top_players,
               (bool success) => {
                   Debug.Log("(Square) Leaderboard update success: " + success);
               });
            } else if (SceneManager.GetActiveScene().name == "Level2") {
                Social.ReportScore(score,
                GPGSIds.leaderboard_top_impossible_players,
               (bool success) => {
                   Debug.Log("(Square) Leaderboard update success: " + success);
               });
            }
        }
    }

    private void UnlockNormalAchievements(int achievementKey) {
        if (normalAchievementsDict.ContainsKey(achievementKey) && 
            PlayGamesPlatform.Instance.GetAchievement(normalAchievementsDict[achievementKey]) != null) {
            var achievement = normalAchievementsDict[achievementKey];
            PlayGamesPlatform.Instance.UnlockAchievement(achievement);
        }
    }
    private void UnlockImpossibleAchievements(int achievementKey) {
        if (impossibleAchievementsDict.ContainsKey(achievementKey) &&
            PlayGamesPlatform.Instance.GetAchievement(impossibleAchievementsDict[achievementKey]) != null) {
            var achievement = impossibleAchievementsDict[achievementKey];
            PlayGamesPlatform.Instance.UnlockAchievement(achievement);
        }
    }

    public void GetUserScore(Action<int> googleScore, String leaderboard) {
        var id = PlayGamesPlatform.Instance.GetUserId();
        PlayGamesPlatform.Instance.LoadScores(
             leaderboard,
             LeaderboardStart.PlayerCentered,
             1,
             LeaderboardCollection.Public,
             LeaderboardTimeSpan.AllTime,
         (LeaderboardScoreData data) => {
             if (id == data.PlayerScore.userID) {
                 googleScore((int)data.PlayerScore.value);
             }
         });


        /*
        PlayGamesPlatform.Instance.LoadScores(leaderboard, scores => {
            if (scores.Length > 0) {
                Debug.Log("Retrieved " + scores.Length + " scores");

                //Filter the score with the user name
                for (int i = 0; i < scores.Length; i++) {
                    if (id == scores[i].userID) {
                        googleScore(scores[i]);
                        break;
                    }
                }
            } else
                Debug.Log("Failed to retrieved score");
        });
        */
    }
}
