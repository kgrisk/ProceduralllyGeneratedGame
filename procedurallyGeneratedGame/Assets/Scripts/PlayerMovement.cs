using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	Rigidbody2D rgb;
	public float speed;
	private bool facingRight = true;
	private Animator anim;

	private bool meleeAttack = false;
	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		HandleInput ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontalMovement = Input.GetAxis ("Horizontal");
		PlayerMovements (horizontalMovement);
		Flip (horizontalMovement);

		AttackHandler ();
		ResetValues ();
	}
	private void PlayerMovements(float horizontal){
		if (!this.anim.GetCurrentAnimatorStateInfo (0).IsName ("MeleeAttack")) {
			Vector2 velocity = new Vector2 (horizontal * speed, rgb.velocity.y);
			rgb.velocity = velocity;
		} else
			rgb.velocity = new Vector2(0,0);


		anim.SetFloat("speed",Mathf.Abs(horizontal));
	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.E)) {
			meleeAttack = true;
		}
	}

	private void AttackHandler(){
		if (meleeAttack) {
			anim.SetTrigger ("meleeAttack");
		}
	}

	private void Flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			transform.localScale = new Vector3(transform.localScale.x * -1,transform.localScale.y,transform.localScale.z);

		}

	}
	private void ResetValues(){
		meleeAttack = false;
	}
}


