using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [SerializeField]
    InputField username;

	// Use this for initialization
	void Start() {
        username.text = PlayerPrefs.GetString("username", "Player");
        //username.text.
	}

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SaveUsername(Text text)
    {
        PlayerPrefs.SetString("username", text.text);
    }
}
