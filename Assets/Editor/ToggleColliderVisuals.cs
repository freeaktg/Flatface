using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class ToggleColliderVisuals : EditorWindow {

	[MenuItem("Prancake/Collider Visuals/On")]
	static void ToggleOn() {
		ToggleVisuals(true);
	}


	[MenuItem("Prancake/Collider Visuals/Off")]
	static void ToggleOff() {
		ToggleVisuals(false);
	}

	static void ToggleVisuals(bool on) {
		GameObject[] colliders = 
		GameObject.FindGameObjectsWithTag("Physics");
		foreach (GameObject col in colliders) {
			if (col.renderer)
				col.renderer.enabled = on;
		}
	}
}
