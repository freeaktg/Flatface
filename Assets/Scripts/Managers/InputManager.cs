using UnityEngine;
using System.Collections;

public class InputManager {

	static InputHandler _handler;
	private static InputHandler handler {
		get {
			if (_handler == null) {
				switch (SystemInfo.deviceType) {
					default:
						_handler = new KeyboardInput();
						break;
				}
			}
			return _handler;
		}
	}

	public static bool JumpButtonDown() {
		return handler.JumpButtonDown();
	}

	public static bool Right() {
		return handler.Right();
	}

	public static bool Left() {
		return handler.Left();
	}

	public static bool Up() {
		return handler.Up();
	}

	public static bool Down() {
		return handler.Down();
	}

	public static bool TouchButtonDown() {
		return handler.TouchDown();
	}
}

public abstract class InputHandler {
	public virtual bool JumpButtonDown() { return false; }
	public virtual bool Right() { return false; }
	public virtual bool Left() { return false; }
	public virtual bool Up() { return false; }
	public virtual bool Down() { return false; }
	public virtual bool TouchDown() { return false; }
}