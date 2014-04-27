using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	void Awake() {
		if (Instance)
			Debug.LogError("Only one instance of Game Manager can exist!");
		Instance = this;
		Application.targetFrameRate = GlobalSettings.Instance.FramerateCap;
	}
}
