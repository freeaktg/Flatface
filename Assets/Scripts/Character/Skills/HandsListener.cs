using UnityEngine;
using System.Collections;

public class HandsListener : MonoBehaviour {

	PlatformerController player;

	void Awake() {
		player = transform.parent.GetComponent<PlatformerController> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		player.SendMessage("OnHandsEnter", col, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit2D(Collider2D col) {
		player.SendMessage("OnHandsExit", col, SendMessageOptions.DontRequireReceiver);
	}
}
