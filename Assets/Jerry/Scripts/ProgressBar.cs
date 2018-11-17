using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour {
	public static ProgressBar instance;

	public float _rarity = 1f;

	float smoothing = 1f;
	float _targetValue;
	float progressionValue = 0.2f;

	float actualProgress;


	Slider _bar;
	// Use this for initialization

	[SerializeField]
		Sprite t1_chest;
	[SerializeField]
		Sprite t2_chest;
	[SerializeField]
		Sprite t3_chest;
	[SerializeField]
		Sprite t4_chest;
	[SerializeField]
		Sprite t5_chest;
	[SerializeField]
		Sprite t6_chest;
	[SerializeField]
		Sprite t7_chest;
	[SerializeField]
		Sprite t8_chest;
	[SerializeField]
		Sprite t9_chest;

	void Start () {
		_bar = GetComponent<Slider> ();
		if (Claiming.instance != null) {
			ResetProgress ();
		}
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		actualProgress = _bar.value;

		if (Input.GetKeyDown (KeyCode.A)) {
			Upgrade ();
		}
	}

	public void AddProgressBar(){
		//do rarity identification here

		//for now just add 5% to all rarity when clicked
		switch ((int)_rarity) {
		case 1:
			progressionValue = 0.2f;
			break;
		case 2:
			progressionValue = 0.15f;
			break;
		case 3:
			progressionValue = 0.10f;
			break;
		case 4:
			progressionValue = 0.10f;
			break;
		case 5:
			progressionValue = 0.065f;
			break;
		case 6:
			progressionValue = 0.035f;
			break;
		case 7:
			progressionValue = 0.015f;
			break;
		case 8:
			progressionValue = 0.010f;
			break;
		case 9:
			progressionValue = 0.005f;
			break;

		}
		//Debug.Log ("Porgression Value is: " + progressionValue);
		 _targetValue = _bar.value + progressionValue;


		if (_targetValue > 1f) {
			_targetValue = 1f;
		}

		//Debug.Log (_targetValue);
			StartCoroutine (AnimateBar (_targetValue));
			
	}

	IEnumerator  AnimateBar(float targetV){

		while(_bar.value < targetV && (targetV - _bar.value) < 0.4f){
			_bar.value = Mathf.Lerp (_bar.value, targetV, Time.deltaTime * smoothing);
			if (1 - _bar.value < 0.001f) {//here is upgrade
				_bar.value = 0;
				targetV= 0f;
				_targetValue = 0f;
				Upgrade ();
			}
			yield return null;
		}
	}

	void Upgrade(){
		if (_rarity < 9f) {
			_rarity += 1f;
		} else {
			_rarity = 9f;
			//pop notification here
		}
		//color animation
		Debug.Log("升级-rarity: "+ _rarity);
		IconChange ();
		BarColorChange ();

		Tutorials.instance.Do_Tut_Upgrade ();
	}


	//change icon based on rarity
	public void IconChange(){
		switch ((int)_rarity) {
			case 1:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t1_chest;
				break;
			case 2:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t2_chest;
				break;
			case 3:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t3_chest;
				break;
			case 4:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t4_chest;
				break;
			case 5:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t5_chest;
				break;
		case 6:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t6_chest;
			break;
		case 7:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t7_chest;
			break;
		case 8:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t8_chest;
			break;
		case 9:
			transform.Find ("Icon").GetComponent<Image> ().sprite = t9_chest;
			break;
		}

		transform.Find ("Icon").GetComponent<Animator> ().SetTrigger ("Pop");
	}




	//change color of bar based on rarity
	public void BarColorChange(){
		switch((int)_rarity){
		case 1:
			
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.whito;
			break;
		case 2:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.green;
			break;
		case 3:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.blue;
			break;
		case 4:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.blue;
			break;
		case 5:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.yellow;
			break;
		case 6:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.purple;
			break;
		case 7:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.orange;
			break;
		case 8:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.red;
			break;
		case 9:
			transform.Find ("Fill Area").Find ("Fill").GetComponent<Image> ().color = Claiming.instance.red;
			break;
		}
	}


	public void StartClaming(){
		Debug.Log ("开始最终结算");
		Claiming.instance.GiveReward ();

		Tutorials.instance.Do_Tut_Cashout ();

		//clearing the bar
		_bar.value = 0;
		StopAllCoroutines ();

		//最后再重置rarity！
		_rarity = 1f;
		BarColorChange ();
		IconChange ();


	}

	public void ResetProgress(){
		_rarity = 1f;
		_bar.value = 0;
		StopAllCoroutines ();

		BarColorChange ();
		IconChange ();
	}

}
