using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerStats  {
	[SerializeField]
	private HealthBar bar;
	[SerializeField]
	private float maximumValue;
	[SerializeField]
	private float currentValue;

	public float MaximumValue {
		get {
			return maximumValue;
		}
		set {

			maximumValue = value;
			bar.MaximumValue = maximumValue;
		}
	}

	public float CurrentValue {
		get {
			return currentValue;
		}
		set {
			currentValue = Mathf.Clamp(value,0,MaximumValue);
			bar.Value = currentValue;

		}
	}
	public void GiveValues(){
		this.MaximumValue = maximumValue;
		this.CurrentValue = currentValue;
	}
}
