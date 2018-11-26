using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour {

	public static Tutorials instance;
	// Use this for initialization
	public GameObject tut_01;
	public GameObject tut_02;
	public GameObject tut_03;
	public GameObject tut_04;

	public GameObject tut_cashout;
	 bool cashout_tut_showed = false;

	public GameObject tut_upgrade;
	 bool upgrade_tut_showed = false;

	public GameObject tut_key;
	bool key_tut_showed = false;

	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Complete_Tut_01(){
		tut_02.SetActive (true);
	}
	public void Complete_Tut_02(){
		tut_03.SetActive (true);
	}
	public void Complete_Tut_03(){
		tut_04.SetActive (true);
	}

	public void Do_Tut_Cashout(){
		if (!cashout_tut_showed) {
			tut_cashout.SetActive (true);
			cashout_tut_showed = true;
		}
		
	}

	public void Do_Tut_Upgrade(){
		if (!upgrade_tut_showed) {
			tut_upgrade.SetActive (true);
			upgrade_tut_showed = true;
		}
	}

	public void Do_Tut_Key(){
		if (!key_tut_showed) {
			tut_key.SetActive (true);
			key_tut_showed = true;
		}
	}
}
