using UnityEngine;
using System.Collections;

public class HandsListener : MonoBehaviour {

	PlatformerController player;

	void Awake() {
		player = transform.parent.GetComponent<PlatformerController> ();
	}

	void OnTriggerEnter(Collider col) {
		player.SendMessage("OnHandsEnter", col, SendMessageOptions.DontRequireReceiver);
	}

	void OnTriggerExit(Collider col) {
		player.SendMessage("OnHandsExit", col, SendMessageOptions.DontRequireReceiver);
	}
}
