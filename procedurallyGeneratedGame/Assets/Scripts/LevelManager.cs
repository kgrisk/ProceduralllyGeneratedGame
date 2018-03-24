using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	[SerializeField]
	private GameObject coinPrfb;

	[SerializeField]
	private int amountOfCoin;
	[SerializeField]
	private Text coinText;
	private static LevelManager instance;

	public int AmountOfCoin {
		get {
			return amountOfCoin;
		}
		set {
			coinText.text = value.ToString ();
			amountOfCoin = value;
		}
	}

	public GameObject CoinPrfb {
		get {
			return coinPrfb;
		}
	}

	public static LevelManager Instance{ 
		get{
			if (instance == null) {
				instance = FindObjectOfType<LevelManager> ();	
			}
			return instance;

		}}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
