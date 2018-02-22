using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

	private Enemy enemy;

	private float patrolTimer;

	private float patrolDuration;
	#region IEnemyState implementation

	public void Enter (Enemy enemy)
	{
		patrolDuration = UnityEngine.Random.Range (2, 10);
		this.enemy = enemy;
	}

	public void Execute ()
	{
		Patrol ();
		enemy.Move ();

		 if (enemy.Target != null && enemy.InShootRange) {
			enemy.ChangeState (new ShootState());
		}

	}

	public void Exit ()
	{

	}
	public void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "Edge") {
			Debug.Log ("shit");
			enemy.DirectionChange ();
		}
		if (other.tag == "PlayerBullet") {
			enemy.Target = PlayerMovement.Instance.gameObject;
		}
	}
	#endregion

	private void Patrol(){

		patrolTimer += Time.deltaTime;

		if (patrolTimer >= patrolDuration) {
			enemy.ChangeState(new IdleState());
		}

	}
}
