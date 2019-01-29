using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{

    [SerializeField]
    Text leaderboard;

    const string privateCode = "IE_PVZHSI0GTmfzYKOIYBw7noih_Q5q0yLMyQ6xyqn5w";
    const string publicCode = "5c4f855ab6397e0c24c873d4";
    const string webURL = "http://dreamlo.com/lb/";

    public struct playerScore
    {
        public string username;
        public int score;
    }

    // Use this for initializationß
    void Start()
    {
        StartCoroutine(downloadScores());
    }

    IEnumerator downloadScores()
    {
        UnityWebRequest request = UnityWebRequest.Get(webURL + publicCode + "/pipe/5");

        using (request)
        {
            yield return request.SendWebRequest();
            sortLeaderboard(request.downloadHandler.text);
        }
    }

    void sortLeaderboard(string leaderboard)
    {
        string[] records = leaderboard.Split(new char['\n'], System.StringSplitOptions.RemoveEmptyEntries);

        playerScore[] scores = new playerScore[records.Length];
        for (int i = 0; i < records.Length; i++)
        {
            string[] tempScore = records[i].Split('|');
            scores[i].username = tempScore[0].Substring(tempScore[0].IndexOf(".start.") + 7);
            scores[i].score = int.Parse(tempScore[1]);
        }

        Display(scores);
    }

    void Display(playerScore[] scores)
    {
        leaderboard.text = "";
        for(int i = 0; i < scores.Length; i++)
        {
            leaderboard.text += i+1 + ") " + scores[i].username + " - " + scores[i].score;
            if (i < scores.Length - 1)
            {
                leaderboard.text += "\n";
            }
        }
    }

    // Update is called once per frame
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
