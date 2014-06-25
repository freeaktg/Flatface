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
		MeshRenderer[] colliders = 
			MeshRenderer.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
		foreach (MeshRenderer col in colliders) {
			if (col.sharedMaterial && col.sharedMaterial.name.Replace(" (Instance)", "").EndsWith("_p"))
				col.enabled = on;
		}
	}
}
