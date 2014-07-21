using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	float							jumpHorizontalSpeed;
	const float						MAX_HANG_TIME = 0.1f;
	float							hangTime;
	Rigidbody2D						body;
	Collider2D						circle;
	#endregion

	#region Parameters
	[System.NonSerialized]
	public Vector2 					velocity;
	public float					movementSpeed { get; private set; }
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
		circle = GetComponent<CircleCollider2D>();
		body = rigidbody2D;
	}

	public override void OnUpdate() {
		if (IsGrounded) {
			if (InputManager.GetAxisState(InputManager.AxisName.Jump) == InputManager.AxisState.Down && !Player.IsActionBlocked(PlatformerController.Actions.Jump)) {
				jumpTrigger.Set();
				velocity.y += JumpVelocity.y;
				jumpHorizontalSpeed = JumpVelocity.x * FacingDirection;
				leftParam.Set(false);
				rightParam.Set(false);
				movementSpeed = 0f;
			} else if (InputManager.GetAxis(InputManager.AxisName.Horizontal) < 0f) {
				leftParam.Set(true);
				rightParam.Set(false);
				movementSpeed = -RunVelocity;
				if (!Player.IsActionBlocked(PlatformerController.Actions.ChangeDirection))
					FacingDirection = -1;
			} else if (InputManager.GetAxis(InputManager.AxisName.Horizontal) > 0f) {
				rightParam.Set(true);
				leftParam.Set(false);
				movementSpeed = RunVelocity;
				if (!Player.IsActionBlocked(PlatformerController.Actions.ChangeDirection))
					FacingDirection = 1;
			} else  {
				rightParam.Set(false);
				leftParam.Set(false);
				movementSpeed = 0;
			}
			movementSpeed = movementSpeed * Player.GetMoveSpeed();
		} else {
			rightParam.Set(false);
			leftParam.Set(false);
		}
	}

	List<Collider2D> groundColliders = new List<Collider2D>();

	void OnCollisionEnter2D(Collision2D col) {
		if (col.contacts[0].otherCollider == circle && Mathf.Abs(Vector3.Dot(col.contacts[0].normal, Vector3.up)) > 0.8f) {
			colFlags = CollisionFlags.Below;
			if (!groundColliders.Contains(col.collider))
				groundColliders.Add(col.collider);
			jumpHorizontalSpeed = 0f;
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		//if (col.contacts[0].otherCollider == circle)
		//	colFlags = CollisionFlags.Below;
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.contacts[0].otherCollider == circle && groundColliders.Contains(col.collider)) {
			groundColliders.Remove(col.collider);
			if (groundColliders.Count == 0)
				colFlags = CollisionFlags.None;
		}
	}

	void FixedUpdate() {
		if (Player.SkillsUpdatePosition())
			return;
		velocity.y += Time.deltaTime * GlobalSettings.Instance.Gravity * Player.GetGravity();
		Vector2 realMovement = velocity;
		if ((colFlags & CollisionFlags.Below) != 0)
			realMovement += Vector2.right * movementSpeed;
		else
			realMovement += Vector2.right * jumpHorizontalSpeed;
		jumpHorizontalSpeed *= HorizontalJumpDamping;
		body.velocity = realMovement * 1.5f;
		if (colFlags == CollisionFlags.CollidedBelow) {
			velocity.y = Mathf.Max(0f, velocity.y);
			hangTime = 0f;
		} else
			hangTime += Time.deltaTime;
	}
}
