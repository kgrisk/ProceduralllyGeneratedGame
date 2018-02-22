using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState {
	private Enemy enemy;
	private float meleeAttackTimer;
	private float meleeAttackCooldown = 1;
	private bool canMeleeAttack =true;



	#region IEnemyState implementation
	void IEnemyState.Execute ()
	{
		MeleeAttack ();
		if (enemy.InShootRange && !enemy.InMeleeRange) {
			enemy.ChangeState (new ShootState ());
		} else if (enemy.Target == null) {
			enemy.ChangeState (new IdleState());
		}
	}
	public void Enter (Enemy enemy)
	{
		this.enemy = enemy;
	}
	void IEnemyState.Exit ()
	{

	}
	void IEnemyState.OnTriggerEnter (Collider2D other)
	{

	}
	#endregion
	private void MeleeAttack(){
		meleeAttackTimer += Time.deltaTime;
		if (meleeAttackTimer >= meleeAttackCooldown) {
			canMeleeAttack = true;
			meleeAttackTimer = 0;
		}
		if (canMeleeAttack) {
			canMeleeAttack = false;
			enemy.Anim.SetTrigger ("attack");
		}
	}
}
