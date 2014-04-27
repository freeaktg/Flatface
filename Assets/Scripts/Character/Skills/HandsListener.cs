using UnityEngine;
using System.Collections;

public class HandsListener : MonoBehaviour {

	HoistSkill hoistSkill;

	void Awake() {
		hoistSkill = transform.parent.GetComponent<HoistSkill> ();
	}

	void OnTriggerEnter(Collider col) {
		hoistSkill.OnHandsListenerEnter (col);
	}
}
