using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	
	private float hpAmount;

	[SerializeField]
	private Image content;

	public float MaximumValue{ get; set;}
	[SerializeField]
	private float learpSpeed;
	public float Value{  
		set{
			hpAmount = Map (value, 0, MaximumValue, 0, 1);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateBar ();	
	}
	private void UpdateBar(){
		if(hpAmount != content.fillAmount){
			content.fillAmount = Mathf.Lerp(content.fillAmount, hpAmount, Time.deltaTime * learpSpeed);
}
	}
	private float Map(float value, float minimum, float maximum, float minTranslateTo,float maxTranslateTo){
		return (value - minimum) * (maxTranslateTo - minTranslateTo) / (maximum - minimum) + minTranslateTo;
	}
}
