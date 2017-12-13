using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject target;
	public Vector2 focusAreaSize;

	public float verticalOffSet;
	public float lookAheadDstX;
	public float lookSmoothTimeX;
	public float verticalSmoothTime;


	FocusArea focusArea;

	float currentLookAheadX;
	float targetLookAheadX;
	float lookAheadDirX;
	float smoothLookVelocityX;
	float smoothVelocityY;

	bool lookAheadStopped;

	public void Start(){
		focusArea = new FocusArea (target.GetComponent<BoxCollider2D> ().bounds, focusAreaSize);
	}

	public void LateUpdate(){
		focusArea.Update (target.GetComponent<BoxCollider2D> ().bounds);

		Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffSet;

		if (focusArea.velocity.x != 0) {
			lookAheadDirX = Mathf.Sign (focusArea.velocity.x);
			if (Mathf.Sign (target.GetComponent<Rigidbody2D> ().velocity.x) == lookAheadDirX && target.GetComponent<Rigidbody2D> ().velocity.x != 0) {

				lookAheadStopped = false;
				targetLookAheadX = lookAheadDstX * lookAheadDirX;
			} else {
				if (!lookAheadStopped) {
					lookAheadStopped = true;
					targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
				}
			}
		}
			


		currentLookAheadX = Mathf.SmoothDamp (currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

		focusPosition.y = Mathf.SmoothDamp (transform.position.y, focusPosition.y, ref smoothVelocityY, verticalSmoothTime);
		focusPosition += + currentLookAheadX * Vector2.right;


		transform.position = (Vector3)(focusPosition) + Vector3.forward *-10;
		
	}
	void OnDrawGizmos() {
		Gizmos.color = new Color (1, 0, 0, .5f);
		Gizmos.DrawCube (focusArea.center, focusAreaSize);
	}

	struct FocusArea{
		public float top, bottom, right, left;
		public Vector2 center, velocity;
		
		public FocusArea(Bounds targetBounds, Vector2 size){
			left = targetBounds.center.x - size.x/2;
			right = targetBounds.center.x + size.x/2;
			top = targetBounds.min.y + size.y;
			bottom = targetBounds.min.y;

			velocity = Vector2.zero;
			center = new Vector2((left + right)/2, (top + bottom)/2);
		}
		public void Update(Bounds targerBounds){
			float shiftX = 0;
			if (targerBounds.min.x < left) {
				shiftX = targerBounds.min.x - left;
			} else if (targerBounds.max.x > right) {
				shiftX = targerBounds.max.x - right;
			}
			left += shiftX;
			right += shiftX;

			float shiftY = 0;
			if (targerBounds.min.y < bottom) {
				shiftY = targerBounds.min.y - bottom;
			} else if (targerBounds.max.y > top) {
				shiftY = targerBounds.max.y - top;
			}
			top += shiftY;
			bottom += shiftY;

			center = new Vector2((left + right)/2, (top + bottom)/2);
			velocity = new Vector2 (shiftX, shiftY);

		}
	}
}
