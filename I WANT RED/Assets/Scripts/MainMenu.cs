using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
public class MainMenu : MonoBehaviour
{
	public TMP_Text HighScore;
    // Start is called before the first frame update
    void Start()
    {
		HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore",0).ToString();
    }

	public void QuitGame()
	{
		Application.Quit();
	}

	public void Restart()
	{
		PauseMenu.isPaused = false;
		Time.timeScale = 1f;
		SceneManager.LoadScene("Maze");
	}
}
