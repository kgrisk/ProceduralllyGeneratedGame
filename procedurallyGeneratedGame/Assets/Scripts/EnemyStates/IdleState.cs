using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IEnemyState {

	private Enemy enemy;

	private float idleTimer;

	private float idleDuration ;
	#region IEnemyState implementation

	public void Enter (Enemy enemy)
	{
		idleDuration = UnityEngine.Random.Range (2, 10);
		this.enemy = enemy;
	}

	public void Execute ()
	{
		Idle ();

		if (enemy.Target != null) {
			enemy.ChangeState (new PatrolState());
		}
	}

	public void Exit ()
	{

	}
	public void OnTriggerEnter (Collider2D other)
	{
		if (other.tag == "PlayerBullet") {
			enemy.Target = PlayerMovement.Instance.gameObject;
		}
	}
	#endregion

	private void Idle(){

		enemy.Anim.SetFloat ("speed", 0);

		idleTimer += Time.deltaTime;

		if (idleTimer >= idleDuration) {
			enemy.ChangeState(new PatrolState());
		}

	}
}
