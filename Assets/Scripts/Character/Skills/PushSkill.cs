using UnityEngine;
using System.Collections;

public class PushSkill : Skill {

	public float PushSpeed = 0.5f;
	public float RaycastDistance = 0.5f;
	
	AnimatorParam<bool>				pushing;
	AnimatorParam<bool>				pushIdle;
	AnimatorParam<bool>				pulling;
	Rigidbody2D						m_pushable;
	Vector3							pushableOffset;
	MoveSkill						moveSkill;

	public override void OnStart() {
		pushing = new AnimatorParam<bool>(Components.Animator, "Pushing");
		pushIdle = new AnimatorParam<bool>(Components.Animator, "PushIdle");
		pulling = new AnimatorParam<bool>(Components.Animator, "Pulling");
		moveSkill = GetComponent<MoveSkill>();
	}

	public bool Pushing {
		get {
			return pushing;
		}
		private set {
			pushing.Set(value);
		}
	}

	public bool Pulling {
		get {
			return pulling;
		}
		set {
			pulling.Set(value);
		}
	}

	public bool PushIdle {
		get {
			return pushIdle;
		}
		set {
			pushIdle.Set(value);
		}
	}

	public float CurrentPushSpeed { get; private set; }

	public override void OnFixedUpdate() {
		if (m_pushable && Pushing && !PushIdle) {
			Vector2 pushablePos = m_pushable.transform.position;
			pushablePos.x = transform.position.x + pushableOffset.x;
			Collider2D[] cols = Physics2D.OverlapCircleAll(pushablePos, m_pushable.transform.localScale.x * 0.4f);
			if (cols.Length <= 1)
			//	m_pushable.MovePosition(pushablePos);
				m_pushable.transform.position = pushablePos;
		}
	}

	public override void OnUpdate() {
		if (m_pushable) {
			if (InputManager.TouchButtonDown()) {
				Pushing = !Pushing;
				if (!Pushing) {
					PushIdle = false;
					Pulling = false;
				}
			}
		}
		if (m_pushable && Pushing) {
			if (isFalling(m_pushable)) {
				m_pushable = null;
				Pushing = false;
				Pulling = false;
				PushIdle = false;
			} else if (InputManager.Left() || InputManager.Right()) {
				PushIdle = false;
				CurrentPushSpeed = PushSpeed;
			} else {
				PushIdle = true;
				CurrentPushSpeed = 0;
			}
			if (m_pushable)
				Pulling = Player.GetFacingDirection() != Mathf.Sign(moveSkill.movementSpeed) && moveSkill.movementSpeed != 0;
		}
		RaycastHit2D hit;
		if ((hit = Physics2D.Raycast((Vector2)transform.position + Vector2.up * Player.Height * transform.localScale.y * 0.5f
		                            ,//+ Vector3.right * Player.GetFacingDirection() * (RaycastDistance * 0.5f), 
		                            Vector2.right * Player.GetFacingDirection(), RaycastDistance * 1f, 1 << 9)).collider != null
		    ) {
			OnHandsEnterRaycast(hit.collider);
		} else if (m_pushable != null && !Pulling)
			OnHandsExitRaycast(m_pushable.collider2D);
	}

	public override bool IsActionBlocked(PlatformerController.Actions action) {
		switch (action) {
			case PlatformerController.Actions.Jump:
				return Pushing;
			case PlatformerController.Actions.ChangeDirection:
				return Pushing;
		}
		return false;
	}

	public override float GetMoveSpeed() {
		if (Pushing)
			return CurrentPushSpeed;
		return 1f;
	}

	bool isFalling(Rigidbody2D body) {
		return false;
		//return Mathf.Abs(body.angularVelocity.z) > 1f && (m_pushable == null || m_pushable.velocity.y < -0.03f);
	}

	void OnHandsEnterRaycast(Collider2D col) {
		if (Pushing)
			return;
		float x_dist = col.transform.position.x - transform.position.x;
		if (col.tag == "Pushable" && !isFalling(col.rigidbody2D) && Mathf.Sign(x_dist) == Player.GetFacingDirection()) {
			m_pushable = col.rigidbody2D;
			pushableOffset = m_pushable.transform.position - transform.position;
		}
	}

	void OnHandsExitRaycast(Collider2D col) {
		if (col.tag == "Pushable") {
			Release();
		}
	}

	public void Release() {
		Pushing = false;
		Pulling = false;
		PushIdle = false;
		m_pushable = null;
	}

	void _OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.tag == "Pushable") {
			Pushing = true;
			if (hit.moveDirection.y < -0.3)
				return;

			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
			hit.rigidbody.velocity += pushDir * 1f;
		}
	}
}
