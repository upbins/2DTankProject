using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour {

	//属性值
	public int LifeValue = 3;
	public int playerScore = 0;
	public bool isDead = false;
	public bool isDefeat = false;
	public GameObject bornPrefab;
	public Text playerScoreText;
	public Text playerLifeText;
	public GameObject ImageGameOver;
	public int PlayerNum = 1;
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
	 /// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		ImageGameOver.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(isDefeat)
		{
			ImageGameOver.SetActive(true);
			Invoke("ReturnMenu",2.0f);
			return;
		}
		if(isDead)
		{
			ReBorn();
		}
		playerScoreText.text = playerScore.ToString();
		playerLifeText.text = LifeValue.ToString();
	}
	private void ReturnMenu(){
		SceneManager.LoadScene("MenuScene");
	}
	private void ReBorn()
	{
		if (LifeValue <= 0 ){
			//游戏结束
			isDefeat = true;
			Invoke("ReturnMenu",2.0f);
		
		}else{
			LifeValue--;
			
			GameObject itemGameobj = Instantiate(bornPrefab,new Vector3(-2,-8,0),Quaternion.identity);
			itemGameobj.transform.SetParent(gameObject.transform);
			itemGameobj.GetComponent<Born>().IsCreatePlayer = true;
			isDead = false;
		}
	}
}
