using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	//属性值
	public int LifeValue = 3;
	public int playerScore = 0;
	//单利
	private static PlayerManager instance;
	public static PlayerManager Instance
	{
		get
		{
			return instance;
		}
		set
		{
			instance = value;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
