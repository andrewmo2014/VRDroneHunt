using UnityEngine;
using System.Collections;

public class Subliminal : MonoBehaviour {

	public GameObject message;
	private float timeUntilMessage = 3f;
	private int resetMessage = 0;

	// Update is called once per frame
	void Update () {
		resetMessage++;
		if (resetMessage > 2) {
			resetMessage = 0;
			message.SetActive (false);
		}
		timeUntilMessage -= Time.deltaTime;
		if (timeUntilMessage <= 0f) {
			resetMessage = 0;
			message.SetActive (true);
			timeUntilMessage = Random.Range (1f, 5f);
		}

	}
}
