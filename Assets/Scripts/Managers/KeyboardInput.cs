using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyboardInput : InputHandler {
	
	public KeyboardInput() {
		axises = new Dictionary<InputManager.AxisName, KeyboardAxis>() {
			{ InputManager.AxisName.Horizontal, new KeyboardAxis() {
					PositiveButton = KeyCode.RightArrow,
					NegativeButton = KeyCode.LeftArrow 
				}
			},
			{ InputManager.AxisName.Vertical, new KeyboardAxis() {
					PositiveButton = KeyCode.UpArrow,
					NegativeButton = KeyCode.DownArrow
				}
			},
			{ InputManager.AxisName.Jump, new KeyboardAxis() {
					PositiveButton = KeyCode.Space
				}
			},
			{ InputManager.AxisName.Touch, new KeyboardAxis() {
					PositiveButton = KeyCode.LeftCommand,
					PositiveButton2 = KeyCode.LeftControl
				}
			}
		};
	}

	Dictionary<InputManager.AxisName, KeyboardAxis> axises;

	public override float GetAxis(InputManager.AxisName axis) {
		if (!axises.ContainsKey(axis))
			return 0f;
		KeyboardAxis kbAxis = axises[axis];
		if (Input.GetKey(kbAxis.PositiveButton) || Input.GetKey(kbAxis.PositiveButton2))
			return 1f;
		if (Input.GetKey(kbAxis.NegativeButton))
			return -1f;
		return 0f;
	}

	public override InputManager.AxisState GetAxisState(InputManager.AxisName axis) {
		if (!axises.ContainsKey(axis))
			return InputManager.AxisState.Idle;
		KeyboardAxis kbAxis = axises[axis];
		if (Input.GetKeyDown(kbAxis.PositiveButton) || Input.GetKeyDown(kbAxis.PositiveButton2) ||
		    Input.GetKeyDown(kbAxis.NegativeButton))
			return InputManager.AxisState.Down;
		if (Input.GetKeyUp(kbAxis.PositiveButton) || Input.GetKeyUp(kbAxis.PositiveButton2) || 
		    Input.GetKeyUp(kbAxis.NegativeButton))
			return InputManager.AxisState.Up;
		if (Input.GetKey(kbAxis.PositiveButton) || Input.GetKey(kbAxis.PositiveButton2) || 
		    Input.GetKey(kbAxis.NegativeButton))
			return InputManager.AxisState.Pressed;
		return InputManager.AxisState.Idle;

	}

	struct KeyboardAxis {
		public KeyCode PositiveButton;
		public KeyCode PositiveButton2;
		public KeyCode NegativeButton;
	}
}
