using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (new Vector3 (5f * Time.deltaTime, 0f, 0f));
	}
}
