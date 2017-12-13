using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	private Rigidbody2D rgb;

	public float speed;
	private Vector2 direction;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void FixedUpdate(){
		rgb.velocity = direction * speed;
	}
	public void Initalize(Vector2 direction){
		this.direction = direction;
	}
	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
