using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clusters : MonoBehaviour {
	[SerializeField]
	GameObject explode_prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnExplosions(){
		//改成object pooling
		GameObject root = GameObject.Find ("Explosions");

		GameObject go = Instantiate (explode_prefab, transform.localPosition, Quaternion.identity, root.transform);
		go.transform.localPosition = transform.localPosition;
		//消灭自身
		GameObject.Destroy(this.gameObject);
	}
}
