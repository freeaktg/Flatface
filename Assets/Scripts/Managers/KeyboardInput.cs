using UnityEngine;
using System.Collections;

public class KeyboardInput : InputHandler {
	public override bool JumpButtonDown() {
		return Input.GetKeyDown(KeyCode.Space);
	}
	public override bool Left() {
		return Input.GetKey(KeyCode.LeftArrow);
	}
	public override bool Right() {
		return Input.GetKey(KeyCode.RightArrow);
	}
	public override bool Up() {
		return Input.GetKey(KeyCode.UpArrow);
	}
	public override bool Down() {
		return Input.GetKey(KeyCode.DownArrow);
	}
	public override bool TouchDown() {
		return Input.GetKeyDown(KeyCode.LeftCommand) || Input.GetKeyDown(KeyCode.LeftControl);
	}
}
