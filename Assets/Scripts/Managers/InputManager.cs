using UnityEngine;
using System.Collections;

public static class InputManager {

	public enum AxisName {
		Jump,
		Horizontal,
		Vertical,
		Touch
	}

	public enum AxisState {
		Idle,
		Down,
		Pressed,
		Up
	}

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

	public static float GetAxis(AxisName axis) {
		return handler.GetAxis(axis);
	}

	public static AxisState GetAxisState(AxisName axis) {
		return handler.GetAxisState(axis);
	}
}

public abstract class InputHandler {
	public virtual float GetAxis(InputManager.AxisName axis) { return 0f; }
	public virtual InputManager.AxisState GetAxisState(InputManager.AxisName axis) { return InputManager.AxisState.Idle; }
}