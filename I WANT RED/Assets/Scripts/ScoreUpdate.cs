using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreUpdate : MonoBehaviour
{	public TMP_Text scoreText;
	public static float scoreAmount;
	float pointIn;
	public static float currentHealthf;
	public int maxHealth = 100;
	public static int currentHealth;
	public HealthBar healthBar;
	public GameObject capsule;
	public Transform enemy;
	public static bool spawned;
    // Start is called before the first frame update
    void Start()
	{	spawned = false;
		scoreAmount = 0f;
		pointIn = 1f;
		currentHealth = maxHealth;
		currentHealthf = (float)maxHealth;
		healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
		scoreText.text = "Score: " + (int)scoreAmount;
		scoreAmount += pointIn * Time.deltaTime;
		currentHealthf -= pointIn * Time.deltaTime;
		currentHealth = (int)currentHealthf;
		healthBar.SetHealth(currentHealth);
		if(PlayerPrefs.GetInt("HighScore",0) < (int)scoreAmount)
			PlayerPrefs.SetInt("HighScore",(int)scoreAmount);
		if((int)scoreAmount%10 == 0 && spawned == false) 
		{	spawned = true;
			Instantiate(capsule,enemy.position,enemy.rotation);}
		
    }
}
