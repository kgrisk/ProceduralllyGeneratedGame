using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFalling : MonoBehaviour {

	public float waitingTime;
	Rigidbody2D rgb;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		//rgb.isKinematic = true;
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.CompareTag("Player")) {
			StartCoroutine (IsFalling());
		}
	}


	IEnumerator IsFalling(){
		yield return new WaitForSeconds (waitingTime);
		rgb.isKinematic = false;
		GetComponent<Collider2D> ().isTrigger = true;

	}
}
