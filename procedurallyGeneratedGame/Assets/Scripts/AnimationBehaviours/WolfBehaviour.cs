using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfBehaviour : MonoBehaviour {

	public Vector2 speed;
	public float jumpForce;

	Rigidbody2D rgb;
	Animator anim;

	/*variable for defining the distance before player where you want to check if its ground*/
	float checkFrontForGap;

	/*variable for defining the distance from which player will be noticed by enemy*/
	float checkFrontForEnemy;

	bool enemyFound = false;

	/*variables for jumping and landing*/

	bool startJump = false;
	bool landed = false;

	/*variable for checking enemy movement direction*/
	float direction = 1;
	bool coroutineIsRunning = false;

	GameObject chasedObject;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		checkFrontForGap = Mathf.Abs (gameObject.GetComponent<Collider2D> ().bounds.max.x -gameObject.GetComponent<Collider2D> ().bounds.min.x );
		checkFrontForEnemy = 10;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enemyFound) {
			if (!coroutineIsRunning) {
				StartCoroutine (StopLookingForEnemy ());
			}
			if (startJump) {
				Jump ();
			} else {
				Chase ();
			}

		} else if(!enemyFound) {
			
			Idle ();
			LookForEnemy ();

		}
	} 

	void Jump(){
		if (rgb.velocity == Vector2.zero && !landed) {
			rgb.velocity = new Vector2 (speed.x * direction, jumpForce);
			landed = true;
		} else if (rgb.velocity == Vector2.zero && landed) {
			startJump = false;
			landed = false;
		}
	}


	void Chase(){
		
		if (GroundAtFront ()) {
			
			direction = Mathf.Sign (chasedObject.GetComponent<Rigidbody2D> ().position.x - transform.position.x);
		
			rgb.velocity = new Vector2 (speed.x * direction, rgb.velocity.y);

		} else {
			startJump = true;
			rgb.velocity = Vector2.zero;
			}
	}

	void LookForEnemy(){
		RaycastHit2D ray = Physics2D.Raycast (new Vector2(transform.position.x  + checkFrontForGap * Mathf.Sign(rgb.velocity.x),transform.position.y), new Vector2(Mathf.Sign(rgb.velocity.x),0),checkFrontForEnemy);

		if (ray.collider != null) {
			if (ray.collider.gameObject.CompareTag ("Player")) {
				enemyFound = true;
				chasedObject = ray.collider.gameObject;


			}

		}
	}


	IEnumerator StopLookingForEnemy(){
		coroutineIsRunning = true;
		int timeToLook = 10;
		Debug.Log (enemyFound);

		while (timeToLook != 0) {
			if (enemyFound) {
				Debug.Log(timeToLook);
				timeToLook--;
			}
			yield return new WaitForSeconds (1);

		}enemyFound = false;
		coroutineIsRunning = false;
	}
	void Idle(){
		if (GroundAtFront()) {
			rgb.velocity = new Vector2 (speed.x * direction, rgb.velocity.y);
		} else {
			direction *= -1;
			rgb.velocity = new Vector2( speed.x * direction,rgb.velocity.y);
		}
	}
	bool GroundAtFront(){
		RaycastHit2D ray = Physics2D.Raycast (new Vector2((transform.position.x + checkFrontForGap * Mathf.Sign(rgb.velocity.x)),transform.position.y), Vector2.down,2); 
		return (ray.collider != null);
	}

}
