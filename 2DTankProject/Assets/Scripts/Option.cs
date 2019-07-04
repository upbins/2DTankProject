using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Option : MonoBehaviour {

	public int choice = 1;
	public Transform[] posList;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
		{
			choice = 1;
			transform.position = posList[0].position;
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			choice = 2;
			transform.position = posList[1].position;
		}
		if (choice == 1 && Input.GetKeyDown(KeyCode.Space))
		{
			//PlayerManager.Instance.PlayerNum = 1;
			SceneManager.LoadScene("GameScene");
		}
		if (choice == 2 && Input.GetKeyDown(KeyCode.Space))
		{
			//PlayerManager.Instance.PlayerNum = 2;
			SceneManager.LoadScene("GameScene");
		}
	}
}
