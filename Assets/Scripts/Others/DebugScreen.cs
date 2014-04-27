using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugScreen : MonoBehaviourEx {

	// Use this for initialization
	void Start () {
		if (!GlobalSettings.Instance.Debug)
			Destroy(gameObject);
	}

	void OnGUI() {
		Matrix4x4 oldmatrix = GUI.matrix;
		GUI.matrix.SetTRS(Vector3.zero, Quaternion.identity, new Vector3(1f / (float)Screen.width, 1f / (float)Screen.height, 1f));
		///

		if (GUI.Button(new Rect(10, 10, 100, 50), "Restart"))
			Application.LoadLevelAsync(Application.loadedLevel);

		///
		GUI.matrix = oldmatrix;
	}
}
