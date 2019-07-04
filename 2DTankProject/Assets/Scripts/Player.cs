using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	public float hSpeed = 3;
	public float vSpeed = 3;
	private Vector3 bulletEularAngle;
	private float timeAtttackVal;//cd时间
	private bool isDefend = true;//被保护
	private float defendTime = 3;//保护时间
	private SpriteRenderer spriteRender;
	public Sprite[] tankSprite; //上右下左

	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	public GameObject defendEffectPrefab;
	public AudioSource moveAudio;
	public AudioClip[] tankAudio;
	void Start () 
	{
		spriteRender = GetComponent<SpriteRenderer>();	
		moveAudio  = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//是否处于无敌状态
		if(isDefend)
		{
			defendEffectPrefab.SetActive(true);
			defendTime -= Time.deltaTime;
			if (defendTime <= 0)
			{
				isDefend = false;
				defendEffectPrefab.SetActive(false);
			}
		}
	
	}
	//坦克移动方法
	private void Move()	
	{
		//上下移动
		float v = Input.GetAxisRaw("Vertical");
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
		if(Mathf.Abs(v) >= 0.05)
		{
			moveAudio.clip = tankAudio[1];
			if(!moveAudio.isPlaying){
				moveAudio.Play();
			}
		}
		if(v!=0)
		{
			return;
		}
		//横向移动
		float h = Input.GetAxisRaw("Horizontal");
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
		if(Mathf.Abs(h) >= 0.05)
		{
			moveAudio.clip = tankAudio[1];
			if(!moveAudio.isPlaying){
				moveAudio.Play();
			}
		}else
		{
			moveAudio.clip = tankAudio[0];
			if(!moveAudio.isPlaying){
				moveAudio.Play();
			}
		}
		
	}
	//坦克的攻击方法
	private void Attack()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			//子弹产生的角度：当前坦克的角度+子弹应该旋转的角度
			Instantiate(bulletPrefab,transform.position,Quaternion.Euler(transform.eulerAngles + bulletEularAngle));
			timeAtttackVal = 0;
		}
	}

	//坦克的死亡方法
	private void Die()
	{
		if (isDefend){	return ;}
		//爆炸特效->死亡销毁
		Instantiate(explosionPrefab,transform.position,transform.rotation);
		Destroy(gameObject);
		PlayerManager.Instance.isDead = true;
	}
	private void FixedUpdate()
	{
		if(PlayerManager.Instance.isDefeat){return;}
		Move();
		//子弹的cd时间
		if(timeAtttackVal >= 0.4f)
		{
			Attack();
		}
		else
		{
			timeAtttackVal += Time.deltaTime;
		}
	}
}
