using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	public float moveSpeed = 10;
	public bool IsPlayerBullet;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.up * moveSpeed* Time.deltaTime,Space.World);
	}
	
	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	private void OnTriggerEnter2D(Collider2D collision)
	{	
		switch(collision.tag)
		{
			case "Tank":
				if(!IsPlayerBullet)
				{
					collision.SendMessage("Die");
					Destroy(gameObject);
				}
				break;
			case "Heart":
				collision.SendMessage("Die");
				Destroy(gameObject);
				break;
			case "Enemy":
				if(IsPlayerBullet)
				{
					collision.SendMessage("Die");
					Destroy(gameObject);
				}
				break;
			case "Wall":
				Destroy(collision.gameObject);
				Destroy(gameObject);
				break;
			case "Barrier":
				Destroy(gameObject);
				break;
			default:
				break;
		}
	}
}
