using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Claiming : MonoBehaviour {
	[SerializeField]
	GameObject slot_1;
	[SerializeField]
	GameObject slot_2;
	[SerializeField]
	GameObject slot_3;
	[SerializeField]
	GameObject slot_4;
	[SerializeField]
	GameObject slot_5;
	[SerializeField]
	GameObject slot_6;

	List<GameObject> slots;

	[HideInInspector]
	public Color32 whito;
	[HideInInspector]
	public Color32 green;
	[HideInInspector]
	public Color32 blue;
	[HideInInspector]
	public Color32 yellow;
	[HideInInspector]
	public Color32 purple;
	[HideInInspector]
	public Color32 orange;
	[HideInInspector]
	public Color32 red;
	[HideInInspector]
	public static Claiming instance;

	List<Color32> colors;
	// Use this for initialization
	void Start () {
		

		whito = new Color32(255,255,255,255);
		green  = new Color32(206,253,177,255);
		blue  = new Color32(21,197,255,255);
		yellow  = Color.yellow;
		purple  = new Color32(194,115,253,255);
		orange  = new Color32(241,181,23,255);
		red  = new Color32(240,80,53,255);

		slots = new List<GameObject> ();
		slots.Add (slot_1);
		slots.Add (slot_2);
		slots.Add (slot_3);
		slots.Add (slot_4);
		slots.Add (slot_5);
		slots.Add (slot_6);

		colors = new List<Color32> ();
		colors.Add (whito);
		colors.Add (green);
		colors.Add (blue);
		colors.Add (yellow);
		colors.Add (purple);
		colors.Add (orange);
		colors.Add (red);

		instance = this;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GiveReward(){

		Debug.Log ("give reward: " + ProgressBar.instance._rarity);

		switch ((int)ProgressBar.instance._rarity) {
		case 1:
			StartCoroutine (PopSequence(1,(int)ProgressBar.instance._rarity));
			break;
		case 2:
			StartCoroutine (PopSequence(2,(int)ProgressBar.instance._rarity));
			break;
		case 3:
			StartCoroutine (PopSequence(3,(int)ProgressBar.instance._rarity));
			break;
		case 4:
			StartCoroutine (PopSequence(4,(int)ProgressBar.instance._rarity));
			break;
		case 5:
			StartCoroutine (PopSequence(5,(int)ProgressBar.instance._rarity));
			break;
		case 6:
			StartCoroutine (PopSequence(6,(int)ProgressBar.instance._rarity));
			break;
		case 7:
			StartCoroutine (PopSequence(6,(int)ProgressBar.instance._rarity));
			break;
		case 8:
			StartCoroutine (PopSequence(6,(int)ProgressBar.instance._rarity));
			break;
		case 9:
			StartCoroutine (PopSequence(6,(int)ProgressBar.instance._rarity));
			break;

		}
	
	}
		

	public Color32 CalcProb(int rarity){
		//计算每张卡的掉落，修改卡的颜色，然后送到playerCollection
		//暂时用随机代替：
		int index = Random.Range(0, colors.Count);
		return colors [index];
		/*
		switch (rarity) {
		case 1:
			
			break;
		case 2:
			
			break;
		case 3:
			
			break;
		case 4:
			
			break;
		case 5:
			
			break;
		case 6:
			
			break;
		case 7:
			
			break;
		case 8:
			
			break;
		case 9:
			
			break;
		}
*/
		Debug.Log("这次掉落的稀有度为： " + rarity);
	}


	public IEnumerator PopSequence(int num, int rarity){

		for (int i = 0; i < num; i++) {

			// calculate occur rate of each car and set card color
			slots[i].GetComponent<Image>().color = CalcProb(rarity);

			//activate card, show animation
			slots [i].SetActive (true);

			yield return new WaitForSeconds (0.8f);
		}
	}

	public void CloseAllSlots(){
		slot_1.SetActive(false);
		slot_2.SetActive(false);
		slot_3.SetActive(false);
		slot_4.SetActive(false);
		slot_5.SetActive(false);
		slot_6.SetActive(false);
	}
}
