using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomGenerator : MonoBehaviour {
	public GameObject[] roomTeplates;
	GameObject[,] generatedRooms = new GameObject[4,6];
	string random;
	System.Random rand;
	bool ending = true;
	public Text txt;
	enum Rooms{
		ZERO, ONE, TWO,TRHEE
	};
	// Use this for initialization
	int[,] rooms = new int[4,6];
	Transform childPosition;
	void Start () {
		string lastRoom = "";
		string exitTag="";
		random = System.DateTime.Now.Millisecond.ToString();
		rand = new System.Random (random.GetHashCode());
		int startroom = rand.Next (0, 6);
		Debug.Log (startroom + "start Room");
		rooms [0,startroom] = 3;
		generatedRooms[0,startroom] = Instantiate (roomTeplates [0], roomTeplates [0].transform.position, roomTeplates [0].transform.rotation);
		int solutionDir = rand.Next (1, 5);
		int row = 0;
		int rowBefore = row;
		int currentRoom = startroom;
		int roomBefore = currentRoom;
		if (currentRoom == 0) {
			solutionDir = 1;
		} else if (currentRoom == 5) {
			solutionDir = 3;
		}
		while (ending) {
			

			if ((solutionDir == 1 || solutionDir == 2) && currentRoom != 5 && rooms [row, currentRoom +1] == 0) {
				rowBefore = row;
				roomBefore = currentRoom;
				if (rooms [row, currentRoom] == 5) {
					rowBefore--;
					exitTag = "RightUpExit";
					if (currentRoom != 0) {
						rooms [row, currentRoom-1] = 6;
					}
				} else {
					
					exitTag = "RightExit";

				}currentRoom++;
				rooms [row, currentRoom] = 1;
					
				foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
					if(child.CompareTag(exitTag)){

							childPosition = child;
							generatedRooms [row, currentRoom] = Instantiate (roomTeplates [3], childPosition);
							foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
							if(exitChild.CompareTag("LeftExit")){
									Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

									generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

								}
							}
						}
					}
				lastRoom = "ToRight";
					solutionDir = rand.Next (1, 6);
					continue;
				 
			}else if ((solutionDir == 3 || solutionDir == 4) && currentRoom != 0 && rooms [row, currentRoom - 1] == 0) {
				rowBefore = row;
				roomBefore = currentRoom;
				if (rooms [row, currentRoom] == 5) {
					rowBefore--;
					exitTag = "LeftUpExit";
					if (currentRoom != 5) {
						rooms [row, currentRoom+1] = 1;
					}
				} else {
					exitTag = "LeftExit";

				}currentRoom--;
				rooms [row, currentRoom] = 1;
					
				Debug.Log (rooms [row, currentRoom]);
				foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
					if(child.CompareTag(exitTag)){

							childPosition = child;
							generatedRooms [row, currentRoom] = Instantiate (roomTeplates [3], childPosition);
							foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
								if(exitChild.CompareTag("RightExit")){
									Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

									generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

								}
							}
						}
					}
					lastRoom = "ToLeft";
					solutionDir = rand.Next (1, 6);
					continue;
				 
			}else if (solutionDir == 5) {
				if (row != 3) {
					roomBefore = currentRoom;
					rowBefore = row;
					exitTag="";
					if (rooms [row, currentRoom] == 5 && lastRoom == "ToLeft") {
						currentRoom++;
						rowBefore--;
						lastRoom = "ToRight";
					}
					else if (rooms [row, currentRoom] == 5 && lastRoom == "ToRight") {
						currentRoom--;
						rowBefore--;
						lastRoom = "ToLeft";
					}else if (lastRoom == "ToLeft") {
						roomBefore++;
						rowBefore--;
						lastRoom = "ToRight";
					}else if (lastRoom == "ToRight") {
						roomBefore++;
						rowBefore--;
						lastRoom = "ToLeft";
					}
					/*
					if ((currentRoom) != 0 && rooms [row, currentRoom - 1] != 0) {
						

						if (rooms [row, currentRoom - 1] == 5) {
							exitTag = "RightUpExit";
							rowBefore = row - 1;
							currentRoom--;
						} else {
							roomBefore = currentRoom - 1;
							exitTag = "RightExit";
						}

					} else if ((currentRoom) != 5 && rooms [row, currentRoom + 1] != 0) {
						

						if (rooms [row, currentRoom + 1] == 5) {
							exitTag = "LeftUpExit";
							rowBefore = row - 1;

							currentRoom++;
						} else {
							roomBefore = currentRoom + 1;
							exitTag = "LeftExit";
						}
					}*/
					 rooms [row, currentRoom] = 2;
			

					string otherExit = "";
					Debug.Log (rooms [rowBefore, roomBefore]);
					foreach(Transform child in generatedRooms [rowBefore, roomBefore].transform){
						if(child.CompareTag(exitTag)){

							childPosition = child;
							if (generatedRooms [row, currentRoom] != null) {
								Destroy (generatedRooms [row, currentRoom]);
							}
							if (child.CompareTag ("LeftUpExit") || child.CompareTag ("LeftExit")) {
								generatedRooms [row, currentRoom] = Instantiate (roomTeplates [4], childPosition);
								otherExit = "RightExit";
							} else {
								generatedRooms [row, currentRoom] = Instantiate (roomTeplates [5], childPosition);
								otherExit = "LeftExit";
							}

							foreach(Transform exitChild in generatedRooms [row, currentRoom].transform){
								if(exitChild.CompareTag(otherExit)){
									Vector3 difference = generatedRooms [row, currentRoom].transform.position - exitChild.position;

									generatedRooms [row, currentRoom].transform.position = generatedRooms [row, currentRoom].transform.position + difference;

								}
							}
						}
					}
					row++;
					rooms [row, currentRoom] = 5;
					Debug.Log(row +"fgf"+ currentRoom + " rommas" );
					solutionDir = rand.Next (1, 6);
					continue;
				}else {
					if (row == 3 && ((currentRoom != 0 && rooms [row, currentRoom - 1] != 0) || currentRoom == 0) && ((currentRoom != 5 && rooms [row, currentRoom + 1] != 0) || currentRoom == 5)) {
						rooms [row, currentRoom] = 4;
						ending = false;
					} else {
						solutionDir = rand.Next (1, 6);
					}
				}



			}else {
				solutionDir = rand.Next (1, 6);
			}
		
		}
		for (int i = 0; i < 4; i++) {
			for (int a = 0; a < 6; a++) {
				txt.text = txt.text.ToString() + (rooms[i, a] + ", ");
			}
			txt.text = txt.text.ToString () + "\n";
		}
	}
	

}
