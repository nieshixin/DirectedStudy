using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RarityManager : MonoBehaviour {

	float progress;


	Slider _bar;
	// Use this for initialization
	void Start () {
		_bar = GetComponent<Slider> ();

	}

	// Update is called once per frame
	void Update () {
		if (_bar.value >= 1) {
			Upgrade ();
		}
	}

	void Upgrade(){
		//playAnimation

		//clear bar
		_bar.value = 0f;
	}
}
