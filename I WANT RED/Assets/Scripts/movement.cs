using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class movement : MonoBehaviour
{
	public CharacterController controller;
	Animator animator;
	public float mousesense = 100f;
	float xRotation = 0f;
	float yRotation = 0f;
	bool activ = true;
	public ParticleSystem death;
	public ParticleSystem burst;
    // Start is called before the first frame update
    void Start()
    {
		animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if(!PauseMenu.isPaused)
		{
		if (Input.GetKeyDown(KeyCode.UpArrow) && activ)
			{animator.SetBool("isRunning", false);
			animator.SetBool("isWalking", true);}
		else if(Input.GetKeyUp(KeyCode.UpArrow))
			animator.SetBool("isWalking", false);
		if (Input.GetKeyDown(KeyCode.DownArrow) && activ)
			animator.SetBool("isBack", true);
		else if(Input.GetKeyUp(KeyCode.DownArrow))
			animator.SetBool("isBack", false);
		if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("isBack") && activ)
			animator.SetTrigger("jump");
		if (Input.GetKeyDown(KeyCode.R) && activ)
		{
			animator.SetBool("isRunning", true);
			animator.SetBool("isWalking", false);}
		else if(Input.GetKeyUp(KeyCode.R))
			animator.SetBool("isRunning", false);
		
		float mouseX = Input.GetAxis("Mouse X") * mousesense*Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mousesense*Time.deltaTime;
		yRotation -= mouseY;
		xRotation -= mouseX;
		yRotation -= Mathf.Clamp(yRotation, -90f,90f);
		if (activ)
				transform.localRotation = Quaternion.Euler(0f, -xRotation, 0f);
		if(ScoreUpdate.currentHealth ==0 && activ)
			{
				controller.enabled = false;
				animator.SetTrigger("dead");
				activ = false; 
				StartCoroutine(wait());
			}
		}
		
    
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag("enemy"))
		{
			controller.enabled = false;
			Instantiate(burst,hit.gameObject.transform.position, Quaternion.identity);
			Destroy(hit.gameObject);
			animator.SetTrigger("dead");
			activ = false; 
			Time.timeScale = 1f;
			StartCoroutine(wait());


		}
		if (hit.gameObject.CompareTag("med"))
		{	
			if((int)ScoreUpdate.currentHealthf < 90)
			ScoreUpdate.currentHealthf +=10;
			else if((int)ScoreUpdate.currentHealthf > 90)
				ScoreUpdate.currentHealthf = 100;
			ScoreUpdate.spawned = false;
			Instantiate(death,hit.gameObject.transform.position, Quaternion.identity);
			Destroy(hit.gameObject);
		}



	}
	IEnumerator wait()
	{
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MainMenu");
	}
}
