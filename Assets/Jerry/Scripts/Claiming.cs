using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

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

	[SerializeField]
	List<Vector3> slots_origin_pos;

	[SerializeField]
	GameObject collectPos;
	[SerializeField]
	Animator ReceiverAnim;

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


		BackUpOriginalPos ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void BackUpOriginalPos(){
		//把slots中的所有卡牌初始位置备份，以便后面移动动画的时候好还原位置
		slots_origin_pos = new List<Vector3>();

		for (int i = 0; i < slots.Count; i++) {
			Vector3 CopyPos = slots [i].transform.position;
			slots_origin_pos.Add(new Vector3 (CopyPos.x, CopyPos.y, CopyPos.z));
		}
		Debug.Log (slots_origin_pos.Count);

	}



	public void PopRewardCards(){

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

		for (int i = 0; i <= num; i++) {
			
			if (i == num) {//在最后一次执行的时候，激活收集宝箱 (这样做是为了避免过早点击宝箱出现bug，所以让宝箱最后出现）
				transform.Find ("ClaimPanel").Find ("Receiver").gameObject.SetActive (true);
				yield return new WaitForSeconds (0.2f);
			} else {
				
			
				// calculate occur rate of each car and set card color
				slots [i].GetComponent<Image> ().color = CalcProb (rarity);

				//activate card, show animation
				slots [i].SetActive (true);
				yield return new WaitForSeconds (0.2f);
			}

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

	public void StartCollecting(){
		//这个function就是开始飞卡
		foreach (var go in slots) {
			//双重筛选要飞得卡
			if (go.activeSelf) {//是被激活的
				//是没有被单独点过的
				if(go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shrinked")!= true){
				LeanTween.move (go, collectPos.transform.position, 0.5f).setOnComplete (PopCollectChest);

				go.GetComponent<Animator> ().SetTrigger ("Shrink");
				Debug.Log ("collected" + go.name);
				}
			}
		}

	}

	public void ComepleteCollecting(){
		for (int i = 0; i < slots.Count; i++) {
			slots [i].transform.position = slots_origin_pos [i];
		}
		Debug.Log ("重置所有卡牌位置");
		//完成收集后，把所有slot位置还原
		//还原位置后，关闭所有的slots
		CloseAllSlots();

		//之后有三个需要调整开关
		//一个需要开启receiver的button component，
		//有三个个go需要关闭：receiver的go，close button的go ,panel的go   需要按照顺序(从下到上）

		GameObject panel = transform.Find("ClaimPanel").gameObject;
		//开启的
		panel.transform.Find ("Receiver").GetComponent<Button>().enabled = true;
		//关闭的
		panel.transform.Find ("Receiver").gameObject.SetActive (false);
		panel.transform.Find ("close").gameObject.SetActive (false);
		panel.SetActive(false);


	}

	void PopCollectChest(){
		ReceiverAnim.SetTrigger ("Pop");

		//最后的check，如果是最后一张卡，则直接自动call completeCollecting
		float activeCount = 0;
		float collectedCount = 0;

		for (int i = 0; i < slots.Count; i++) {
			if (slots [i].activeSelf) {
				activeCount++;
			}
		}
		for (int i = 0; i < slots.Count; i++) {
			if (slots[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shrinked") == true ) {
				collectedCount++;
			}
		}

		if (activeCount == collectedCount) {
			////////这里就相当于帮玩家点击了收集宝箱按钮
			GameObject panel = transform.Find("ClaimPanel").gameObject;
			panel.transform.Find ("Receiver").GetComponent<Button>().enabled = false;
			panel.transform.Find ("close").gameObject.SetActive (true);
			Debug.Log ("没有剩余可点击的卡了");
		}
		Debug.Log (activeCount + "和" + collectedCount);
	}

	//for cards onclicked,not used in methods
	public void CardOnclickFly(GameObject go){
		//和集体飞卡method是一样的，只不过掰开了， 集体飞卡要干什么这个就要干什么

		//飞卡
		LeanTween.move (go, ReceiverAnim.transform.position, 0.5f).setOnComplete (PopCollectChest);
		//缩小动画
		go.GetComponent<Animator> ().SetTrigger ("Shrink");
		Debug.Log (go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shrinked"));


	}
}
