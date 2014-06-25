using UnityEngine;
using System.Collections;

public class PushSkill : Skill {

	public float PushSpeed = 0.5f;
	public float RaycastDistance = 0.5f;
	
	AnimatorParam<bool>				pushing;
	AnimatorParam<bool>				pushIdle;
	Rigidbody						m_pushable;
	float							pushableOffset;
	CharacterController				charController;

	public override void OnStart() {
		pushing = new AnimatorParam<bool>(Components.Animator, "Pushing");
		pushIdle = new AnimatorParam<bool>(Components.Animator, "PushIdle");
		charController = GetComponent<CharacterController>();
	}

	public bool Pushing {
		get {
			return pushing;
		}
		private set {
			pushing.Set(value);
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

	public override void OnUpdate() {
		if (m_pushable) {
			Vector3 pushablePos = m_pushable.transform.position;
			pushablePos.x = transform.position.x + pushableOffset;
			m_pushable.transform.position = pushablePos;
			if (m_pushable.transform.eulerAngles.z > 10 && m_pushable.transform.eulerAngles.z < 350) {
				m_pushable = null;
				Pushing = false;
				PushIdle = false;
			} else if (InputManager.LeftArrow() || InputManager.RightArrow()) {
				PushIdle = false;
				CurrentPushSpeed = PushSpeed;
			} else {
				PushIdle = true;
				CurrentPushSpeed = 0;
			}
		}
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position + Vector3.up * charController.height * transform.localScale.y * 0.5f, 
		                            Vector3.right * Player.GetFacingDirection()), out hit, RaycastDistance, 1 << 9)) {
			OnHandsEnter(hit.collider);
		} else if (m_pushable != null)
			OnHandsExit(hit.collider);
	}

	void OnHandsEnter(Collider col) {
		if (col.tag == "Pushable") {
			Pushing = true;
			m_pushable = col.rigidbody;
			pushableOffset = m_pushable.transform.position.x - transform.position.x;
		}
	}

	void OnHandsExit(Collider col) {
		if (col.tag == "Pushable") {
			Pushing = false;
			m_pushable = null;
		}
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
