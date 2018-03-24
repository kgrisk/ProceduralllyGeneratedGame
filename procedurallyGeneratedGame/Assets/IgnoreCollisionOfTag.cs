using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionOfTag : MonoBehaviour {

	[SerializeField]
	private string ignore;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == ignore){
			
			Physics2D.IgnoreCollision (GetComponent<BoxCollider2D>(), other.collider, true);
		}}
}
