using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformThrough : MonoBehaviour {
	[SerializeField]
	private BoxCollider2D platformTrigger;

	[SerializeField]
	private BoxCollider2D platformCollider;
	// Use this for initialization
	void Start () {
		Physics2D.IgnoreCollision (platformCollider, platformTrigger, true);
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (platformCollider, other, true);

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (platformCollider, other, false);

		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
