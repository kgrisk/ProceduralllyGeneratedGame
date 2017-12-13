using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShooting : MonoBehaviour {
	public GameObject [] holes;
	public Vector2 dir = new Vector2(-1,0);
	public float rotation;

	float distance = 10;

	bool startShooting = false;
	bool shooted = false;

	public GameObject arrow;
	// Use this for initialization
	void Start () {
		StartCoroutine ("ShootingGun");
	}
	
	// Update is called once per frame
	void Update () {
		if (EnemyOnSight ()) {
			startShooting = true;

		}
	}

	IEnumerator ShootingGun(){
		while (true) {
			if (startShooting) {
				foreach (GameObject obj in holes) {
					Debug.Log ("saunam i subine");
					GameObject obje = (GameObject) Instantiate (arrow, obj.transform.position, Quaternion.Euler (0, 0, rotation));
					obje.GetComponent<Bullet> ().Initalize (Vector2.left);
				}startShooting = false;

			}
				yield return new WaitForSeconds (1);
			
		}
	}

	bool EnemyOnSight(){
		RaycastHit2D ray = Physics2D.Raycast (new Vector2 (transform.position.x + dir.x * 2, transform.position.y + dir.y * 2), dir, distance);
		if (ray.collider != null && ray.collider.gameObject.CompareTag ("Player")) {
			return true;
		}
		return false;
	}
}
