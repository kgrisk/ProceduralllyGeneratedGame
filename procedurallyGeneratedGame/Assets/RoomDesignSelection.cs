using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDesignSelection : MonoBehaviour {
	public GameObject[] roomDesings;
	private static bool[] designUsed;
	// Use this for initialization
	void Start () {
		string random = System.DateTime.Now.Millisecond.ToString();
		System.Random rand = new System.Random (random.GetHashCode());
		designUsed = new bool[roomDesings.Length];
		int room = 0;
		bool selected = false;
		while (!selected) {
			room = rand.Next (0, roomDesings.Length);
			if (designUsed [room] != true) {
				designUsed [room] = true;
				roomDesings [room].SetActive (true);
				selected = true;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
