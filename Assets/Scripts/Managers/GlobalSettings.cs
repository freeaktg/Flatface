using UnityEngine;
using System.Collections;

public class GlobalSettings : MonoBehaviour {

	#region Instance
	static GlobalSettings _instance;
	public static GlobalSettings Instance {
		get {
			if (_instance == null)
				_instance = Resources.Load<GlobalSettings>("GlobalSettings");
			return _instance;
		}
	}
	#endregion

	public bool		Debug;

	public int		FramerateCap = 60;
	public float 	Gravity = -9.81f;
}
