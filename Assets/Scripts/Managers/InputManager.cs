using UnityEngine;
using System.Collections;

public class InputManager {

	public static bool JumpButtonDown() {
		return Input.GetKeyDown(KeyCode.Space);
	}

	public static bool RightArrow() {
		return Input.GetKey(KeyCode.RightArrow);
	}

	public static bool LeftArrow() {
		return Input.GetKey(KeyCode.LeftArrow);
	}

	public static bool TouchButtonDown() {
		return Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.LeftControl);
	}
}
