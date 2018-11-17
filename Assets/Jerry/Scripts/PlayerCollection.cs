using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollection : MonoBehaviour {
	public static PlayerCollection instance;

	//玩家一共有x种可收集卡：
	//白、绿、蓝、紫、橙、红、黑
	int white_collect;
	int green_collect;
	int blue_collect;
	int yellow_collect;
	int purple_collect;
	int orange_collect;
	int red_collect;


	// Use this for initialization
	void Start () {
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AutoFillCollect(){


	}
}
