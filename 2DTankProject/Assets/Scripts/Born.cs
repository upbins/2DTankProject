using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

	// Use this for initialization
	public GameObject playerPrefabs;
	public GameObject[] enemyPrefabsList;
	public bool IsCreatePlayer;
 	void Start () {
		Invoke("BornTank",1.0f);
		Destroy(gameObject,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void BornTank()
	{
		if (IsCreatePlayer)
		{
			Instantiate(playerPrefabs,transform.position,Quaternion.identity);
		}else
		{
			int num = Random.Range(0,enemyPrefabsList.Length);
			Instantiate(enemyPrefabsList[num],transform.position,Quaternion.identity);
		}
	
	}
}
 