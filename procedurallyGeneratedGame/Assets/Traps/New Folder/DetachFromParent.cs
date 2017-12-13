using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachFromParent : MonoBehaviour {
	public GameObject[] kids;
	// Use this for initialization
	void Start () {
		foreach(GameObject kid in kids){
			kid.transform.parent = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
