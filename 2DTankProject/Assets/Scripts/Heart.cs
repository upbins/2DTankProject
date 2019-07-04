using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	// Use this for initialization
	private SpriteRenderer spriteRenderer;
	public GameObject explosionPrefab;
	public Sprite dieSprite;
	public AudioClip dieAudio;
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Die()
	{
		Instantiate(explosionPrefab,transform.position,transform.rotation);
		spriteRenderer.sprite = dieSprite;
		PlayerManager.Instance.isDefeat = true;
		AudioSource.PlayClipAtPoint(dieAudio,transform.position);
	}
}
