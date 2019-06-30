using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

// Use this for initialization
	public float hSpeed = 3;
	public float vSpeed = 3;
	private Vector3 bulletEularAngle;
	private float timeAtttackVal;//cd时间
	private float changeDirTime = 0;
	private float v = -1;
	private float h;
	private SpriteRenderer spriteRender;
	public Sprite[] tankSprite; //上右下左
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	
	void Start () 
	{
		spriteRender = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () 
	{

		//子弹的cd时间
		if(timeAtttackVal >= 3.0f)
		{
			Attack();
		}
		else
		{
			timeAtttackVal += Time.deltaTime;
		}
	}
	//坦克移动方法
	private void Move()	
	{
		if (changeDirTime >= 4.0f)
		{
			int Dir = Random.Range(0,8);
			if (Dir > 5)//向下走
			{
				v = -1.0f;
				h = 0.0f;
			}
			else if(Dir == 0) //往回走
			{
				v = 1.0f;
				h = 0.0f;
			}
			else if(Dir > 0 && Dir <= 2) //往左走
			{
				v = 0.0f;
				h = -1.0f;
			}
			else if(Dir > 2 && Dir <= 4) //往右走
			{
				v = 0.0f;
				h = 1.0f;
			}
			changeDirTime = 0;
		}else
		{
			changeDirTime += Time.fixedDeltaTime;
		}
		//上下移动
		//v = Input.GetAxisRaw("Vertical");
		transform.Translate(Vector3.up * v * vSpeed * Time.fixedDeltaTime,Space.World);
		if (v<0)
		{
			spriteRender.sprite = tankSprite[2];
			bulletEularAngle = new Vector3(0,0,-180);
		}
		else if(v>0)
		{
			spriteRender.sprite = tankSprite[0];
			bulletEularAngle = new Vector3(0,0,0);
		}
		if(v!=0)
		{
			return;
		}
		//横向移动
		//h = Input.GetAxisRaw("Horizontal");
		transform.Translate(Vector3.right * h * hSpeed * Time.fixedDeltaTime,Space.World);
		if (h<0)
		{
			spriteRender.sprite = tankSprite[3];
			bulletEularAngle = new Vector3(0,0,90);
		}
		else if(h>0)
		{
			spriteRender.sprite = tankSprite[1];
			bulletEularAngle = new Vector3(0,0,-90);
		}
		
	}
	//坦克的攻击方法
	private void Attack()
	{
		//子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
		Instantiate(bulletPrefab,transform.position,Quaternion.Euler(transform.eulerAngles + bulletEularAngle));
		timeAtttackVal = 0;
	}

	//坦克的死亡方法
	private void Die()
	{
		//爆炸特效->死亡销毁
		Instantiate(explosionPrefab,transform.position,transform.rotation);
		Destroy(gameObject);
	}
	private void FixedUpdate()
	{
		Move();
	}
	/// <summary>
	/// OnCollisionEnter2D is called when this collider/rigidbody has begun
	/// touching another rigidbody/collider.
	/// </summary>
	/// <param name="other">The Collision data associated with this collision.</param>
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barrier")
		{
			changeDirTime = 1;
		}
	}
}
