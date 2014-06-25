using UnityEngine;
using System.Collections;

public class MoveSkill : Skill {


	#region Settings
	public Vector2 	JumpVelocity;
	public float 	RunVelocity = 10f;
	public float 	HorizontalJumpDamping = 0.9f;
	#endregion

	#region Variables
	AnimatorParam<AnimatorTrigger> 	jumpTrigger;
	AnimatorParam<bool>				leftParam;
	AnimatorParam<bool>				rightParam;
	CollisionFlags					colFlags;
	Vector2 						velocity;
	float							movementSpeed;
	float							jumpHorizontalSpeed;
	const float						MAX_HANG_TIME = 0.1f;
	float							hangTime;
	PushSkill						pushSkill;
	#endregion

	#region Parameters
	public bool IsGrounded {
		get {
			return (colFlags & CollisionFlags.Below) != 0 || hangTime < MAX_HANG_TIME;
		}
	}
	int _facingDirection = 1;
	public int FacingDirection {
		get {
			return _facingDirection;
		}
		set {
			if (_facingDirection != value) {
				_facingDirection = value;
				Vector3 scale = transform.localScale;
				scale.x = Mathf.Abs(scale.x) * _facingDirection;
				transform.localScale = scale;
			}
		}
	}
	#endregion

	public override void OnAwake() {
		base.OnAwake();
		jumpTrigger = new AnimatorParam<AnimatorTrigger>(Components.Animator, "Jumping");
		leftParam = new AnimatorParam<bool>(Components.Animator, "Left");
		rightParam = new AnimatorParam<bool>(Components.Animator, "Right");
	}

	public override void OnStart() {
		pushSkill = GetComponent<PushSkill>();
	}

	public override void OnUpdate() {
		if (IsGrounded) {
			if ((colFlags & CollisionFlags.Below) != 0)
				jumpHorizontalSpeed = 0f;
			if (InputManager.JumpButtonDown()) {
				jumpTrigger.Set();
				velocity.y += JumpVelocity.y;
				jumpHorizontalSpeed = JumpVelocity.x * FacingDirection;
				leftParam.Set(false);
				rightParam.Set(false);
				movementSpeed = 0f;
			} else if (InputManager.LeftArrow()) {
				leftParam.Set(true);
				rightParam.Set(false);
				movementSpeed = -RunVelocity;
				FacingDirection = -1;
			} else if (InputManager.RightArrow()) {
				rightParam.Set(true);
				leftParam.Set(false);
				movementSpeed = RunVelocity;
				FacingDirection = 1;
			} else  {
				rightParam.Set(false);
				leftParam.Set(false);
				movementSpeed = 0;
			}
			if (pushSkill && pushSkill.Pushing)
				movementSpeed = Player.GetFacingDirection() * pushSkill.PushSpeed;
		} else {
			rightParam.Set(false);
			leftParam.Set(false);
		}
	}

	void FixedUpdate() {
		velocity.y += Time.deltaTime * GlobalSettings.Instance.Gravity;
	
		Vector2 realMovement = velocity;
		if ((colFlags & CollisionFlags.Below) != 0)
			realMovement += Vector2.right * movementSpeed;
		else
			realMovement += Vector2.right * jumpHorizontalSpeed;
		jumpHorizontalSpeed *= HorizontalJumpDamping;
		realMovement *= Time.deltaTime;

		colFlags = Components.CharacterController.Move(realMovement);
		if (colFlags == CollisionFlags.CollidedBelow) {
			velocity.y = Mathf.Max(0f, velocity.y);
			hangTime = 0f;
		} else
				hangTime += Time.deltaTime;
	}
}
