using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private static PlayerMovement instance;
	public GameObject bullet;
	public Transform bulletPos;

	public static PlayerMovement Instance{
		get{ 
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PlayerMovement> ();
			}
			return instance;
		}
	}
	public float speed;
	private bool facingRight = true;
	private Animator anim;

	public Transform [] groundPoints;
	public LayerMask groundOverlapMasks;
	public float radiusOverlaps;

	public float jumpForce;

	public Rigidbody2D Rgb{ get; set;}
	public bool Attack{ get; set;}
	public bool Jump{ get; set;}
	public bool OnGround{ get; set;}
	// Use this for initialization
	void Start () {
		Rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	void Update(){
		HandleInput ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		float horizontalMovement = Input.GetAxis ("Horizontal");
		OnGround = IsGrounded ();
		PlayerMovements (horizontalMovement);
		Flip (horizontalMovement);

		LayersHandling ();

	}

	private bool IsGrounded(){

		foreach(Transform point in groundPoints){
			Collider2D[] overalaps = Physics2D.OverlapCircleAll (point.position, radiusOverlaps,  groundOverlapMasks);
			foreach (Collider2D overlaptObj in overalaps) {
				if (gameObject != overlaptObj.gameObject) {
					return true;
				}
			}
		}
		return false;
	}
	private void PlayerMovements(float horizontal){
		if (Rgb.velocity.y < 0) {
			anim.SetBool ("land",true);
		}
		if (!Attack) {
			Rgb.velocity = new Vector2 (horizontal * speed, Rgb.velocity.y);
		}
		if (Jump && Rgb.velocity.y == 0) {
			Rgb.AddForce (new Vector2 (0,jumpForce));
		}
		anim.SetFloat ("speed", Mathf.Abs(horizontal));
			

	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.E)) {
			anim.SetTrigger ("attack");
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("jump");
		}
		if (Input.GetKey (KeyCode.Q)) {
			anim.SetTrigger ("shoot");
		}
	}



	private void Flip(float horizontal){
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
			facingRight = !facingRight;
			transform.localScale = new Vector3 (transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

		}
	}
	private void LayersHandling(){
		if (!OnGround) {
			anim.SetLayerWeight (1, 1);
		} else {
			anim.SetLayerWeight (1, 0);
		}
		}
	public void ShootBullet(int value){
		if ((!OnGround && value == 1) || (OnGround && value == 0)) {
			if (facingRight) {
				GameObject obj = (GameObject)Instantiate (bullet, bulletPos.position, Quaternion.Euler (new Vector3(0,0,-90)));
				obj.GetComponent<Bullet> ().Initalize (Vector2.right);
			} else {

				GameObject obj =(GameObject) Instantiate (bullet, bulletPos.position, Quaternion.Euler(new Vector3(0,0,90)));
				obj.GetComponent<Bullet> ().Initalize (Vector2.left);
			}
		}
	}

	

}


