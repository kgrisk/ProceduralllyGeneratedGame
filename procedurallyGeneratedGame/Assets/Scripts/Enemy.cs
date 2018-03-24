using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public GameObject Target{ get; set;}
	private IEnemyState currentState;

	private Canvas hpCanvas;
	private float meleeRange = 2;
	private float shootRange = 6;
	public bool InMeleeRange
	{
		get
		{
			if (Target != null) {
				return Vector2.Distance (transform.position, Target.transform.position) <= meleeRange;
			}
			return false;
		}
	}

	public bool InShootRange
	{
		get
		{
			if (Target != null) {
				return Vector2.Distance (transform.position, Target.transform.position) <= shootRange;
			}
			return false;
		}
	}
	// Use this for initialization
	public override void Start() {
		base.Start();
		PlayerMovement.Instance.Died +=new DeadEventHandler (RemoveTarget);
		ChangeState (new IdleState());
		hpCanvas = transform.GetComponentInChildren<Canvas> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!IsDead) {
			if (!TakingDemage) {
				currentState.Execute ();
			}
			LookAtTarget ();
		}
	}
	public void RemoveTarget(){
		Target = null;
		ChangeState (new PatrolState());
	}
	public void ChangeState(IEnemyState newState){

		if (currentState != null) 
		{
			currentState.Exit ();
		}

		currentState = newState;
		currentState.Enter(this);

	}
	private void LookAtTarget(){
		if (Target != null) {
			float xDir = Target.transform.position.x - transform.position.x;
			if (xDir < 0 && facingRight || xDir > 0 && !facingRight) {
				DirectionChange ();
			}
		}
	}

	public void Move()
	{
		if (!Attack) {
			Anim.SetFloat ("speed", 1);
			transform.Translate (GetDirection () * speed * Time.deltaTime);
		}
	}
	public Vector2 GetDirection(){
	
		return facingRight ? Vector2.right : Vector2.left;
	}

	public override void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		currentState.OnTriggerEnter (other);
	}

	#region implemented abstract members of Character
	public override void DirectionChange()
	{
		//Makes a reference to the enemys canvas
		Transform tmp = transform.Find("EnemyHealthCanvas").transform;

		//Stores the position, so that we know where to move it after we have flipped the enemy
		Vector3 pos = tmp.position;

		///Removes the canvas from the enemy, so that the health bar doesn't flip with it
		tmp.SetParent(null);

		///Changes the enemys direction
		base.DirectionChange();

		//Puts the health bar back on the enemy.
		tmp.SetParent(transform);

		//Pits the health bar back in the correct position.
		tmp.position = pos;
	}
	public override IEnumerator TakeDemage ()
	{
		if (!hpCanvas.isActiveAndEnabled) {
			hpCanvas.enabled = true;
		}

		healthStat.CurrentValue -= 10;

			if (!IsDead) {
				Anim.SetTrigger ("demage");
				
			} else {
			Instantiate (LevelManager.Instance.CoinPrfb, new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y + 2), Quaternion.identity);
				
				Anim.SetTrigger ("die");
				yield return null;
			}
	}




	#region implemented abstract members of Character

	public override bool IsDead {
		get {
			return healthStat.CurrentValue <= 0;
		}
	}


	#region implemented abstract members of Character

	public override void Dead ()
	{
		Anim.ResetTrigger ("die");
		hpCanvas.enabled = false;
		GameObject.Destroy (gameObject);
	}

	#endregion
	#endregion
	#endregion
}
