using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [SerializeField]
    InputField username;

    [SerializeField]
    Slider mouseSensitivity;

	// Use this for initialization
	void Start() {
        username.text = PlayerPrefs.GetString("username", "Player");
        mouseSensitivity.value = PlayerPrefs.GetFloat("mouseSensitivity", 0.1f);
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

    public void SaveSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat("mouseSensitivity", slider.value);
    }
}
