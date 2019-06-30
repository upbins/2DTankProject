using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {
	public GameObject[] itemList; //初始化物体对象列表 0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
	public int MaxWallNum = 20;
	
	public int MaxBarriarNum = 20;
	public int MaxRiverNum = 20;
	public int MaxGrassNum = 20;
	private List<Vector3> itemPositionList = new List<Vector3>();
	// Use this for initialization
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		//随机一些数量
		MaxWallNum = Random.Range(20,40);
		MaxBarriarNum = Random.Range(20,40);
		MaxRiverNum = Random.Range(20,40);
		MaxGrassNum = Random.Range(20,40);
		//heart
		CreateSingleItem(itemList[0],new Vector3(0,-8,0),Quaternion.identity);
		//heartoutside
		CreateSingleItem(itemList[1],new Vector3(-1,-8,0),Quaternion.identity);
		CreateSingleItem(itemList[1],new Vector3(1,-8,0),Quaternion.identity);
		CreateSingleItem(itemList[1],new Vector3(-1,-7,0),Quaternion.identity);
		CreateSingleItem(itemList[1],new Vector3(0,-7,0),Quaternion.identity);
		CreateSingleItem(itemList[1],new Vector3(1,-7,0),Quaternion.identity);
		InitOutSideWall();
		InitPlayerTank();
		InitMap();
		InitEnemyTank();
		InvokeRepeating("CreateRandomEnemy",4.0f,5.0f);
	}

	void InitPlayerTank()
	{
		CreateSingleItem(itemList[3],new Vector3(-2,-8,0),Quaternion.identity,true);
	}
	void InitEnemyTank()
	{		
		CreateSingleItem(itemList[3],new Vector3(-10,8,0),Quaternion.identity,false);
		CreateSingleItem(itemList[3],new Vector3(0,8,0),Quaternion.identity,false);
		CreateSingleItem(itemList[3],new Vector3(10,8,0),Quaternion.identity,false);
	}
	void CreateRandomEnemy(){
		int num = Random.Range(0,3);
		Vector3 enemyPos = new Vector3();
		if (num == 0)
		{
			enemyPos = new Vector3(-10,8,0);
		}
		else if (num == 1){
			enemyPos = new Vector3(0,8,0);
		}
		else if(num == 2){
			enemyPos = new Vector3(10,8,0);
		}
		CreateSingleItem(itemList[3],enemyPos,Quaternion.identity,false);
	}
	void InitMap()
	{
		for (int i = 0; i < MaxWallNum; i++)
		{
			CreateSingleItem(itemList[1],GetRandomPosition(),Quaternion.identity);
		}
		
		for (int i = 0; i < MaxBarriarNum; i++)
		{
			CreateSingleItem(itemList[2],GetRandomPosition(),Quaternion.identity);
		}
		for (int i = 0; i < MaxRiverNum; i++)
		{
			CreateSingleItem(itemList[4],GetRandomPosition(),Quaternion.identity);
		}
		for (int i = 0; i < MaxGrassNum; i++)
		{
			CreateSingleItem(itemList[5],GetRandomPosition(),Quaternion.identity);
		}
	}
	void InitOutSideWall()
	{
		for (int i = -11; i < 12; i++)
		{
			CreateSingleItem(itemList[6],new Vector3(i,9,0),Quaternion.identity);
			CreateSingleItem(itemList[6],new Vector3(i,-9,0),Quaternion.identity);
		}
		for (int i = -8; i < 9; i++)
		{
			CreateSingleItem(itemList[6],new Vector3(-11,i,0),Quaternion.identity);
			CreateSingleItem(itemList[6],new Vector3(11,i,0),Quaternion.identity);
		}
	}
	private void CreateSingleItem(GameObject obj,Vector3 position,Quaternion rotation,bool IsPlayer = false)
	{
		GameObject itemGameobj = Instantiate(obj,position,rotation);
		itemGameobj.transform.SetParent(gameObject.transform);
		if (itemGameobj.GetComponent<Born>()) 
		{
			itemGameobj.GetComponent<Born>().IsCreatePlayer = IsPlayer;
		}
		itemPositionList.Add(position);
	}
	//产生随机位置的方法
	private Vector3 GetRandomPosition()
	{
		//不生成x=-10,10两列 不生成y=-8,8两行
		while(true)
		{
			Vector3 randomPosition = new Vector3(Random.Range(-9,10),Random.Range(-7,8),0);
			if (!HasThePosition(randomPosition))
			{
				return randomPosition;
			}
			
		}
	}
	//判断位置是否已经存在
	private bool HasThePosition(Vector3 position)
	{
		for (int i = 0; i < itemPositionList.Count; i++)
		{
			if(position == itemPositionList[i])
			{
				return true;
			}

		}
		return false;
	}
}
