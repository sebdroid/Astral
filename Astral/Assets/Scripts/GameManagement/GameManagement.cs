using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour {

    public int clones;

    int score;

    public bool timer;
    float time;

    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject END;

    const string privateCode = "IE_PVZHSI0GTmfzYKOIYBw7noih_Q5q0yLMyQ6xyqn5w";
    const string publicCode = "5c4f855ab6397e0c24c873d4";
    const string webURL = "http://dreamlo.com/lb/";

    // Update is called once per frame
    void Update () {
        if (timer)
        {
            time += Time.deltaTime;
            inGameUI.transform.Find("Stats").GetComponent<Text>().text = "TIME: " + (int)time + "\n" + "CLONES: " + clones;
        }
    }

    public void EndGame(bool death, string message)
    {
        Time.timeScale = 0f;
        GameObject.Find("Canvas").GetComponent<PauseMenu>().Pausable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        inGameUI.SetActive(false);
        if (!death)
        {
            score = (8000 / (int)time) / clones;
        }
        else
        {
            score = 0;
        }
        END.transform.Find("Score").GetComponent<Text>().text = "SCORE: " + score;
        END.transform.Find("Message").GetComponent<Text>().text = message;
        END.SetActive(true);
        StartCoroutine(uploadScore((SystemInfo.deviceUniqueIdentifier + ".start." + "seb"), score));
    }

    IEnumerator uploadScore(string username, int score)
    {
        UnityWebRequest request = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        using (request)
        {
            yield return request.SendWebRequest();
        }
    }
}
