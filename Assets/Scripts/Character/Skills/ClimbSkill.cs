using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveSkill))]
[RequireComponent(typeof(HoistSkill))]
public class ClimbSkill : Skill {

	AnimatorParam<bool> climbing;
	MoveSkill 			moveSkill;
	HoistSkill			hoistSkill;
	float				climbableMaxY;

	public override void OnStart() {
		climbing = new AnimatorParam<bool>(this, "Climbing");
		moveSkill = GetComponent<MoveSkill>();
		hoistSkill = GetComponent<HoistSkill>();
	}

	public bool Climbing {
		get {
			return climbing;
		}
		set {
			if (!value)
				Speed = 1f;
			climbing.Set(value);
		}
	}

	private float Speed {
		get {
			return Player.Animator.speed;
		}
		set {
			Player.Animator.speed = value;
		}
	}

	public override float GetVerticalSpeed() {
		return Climbing ? Speed : 0f;
	}

	public override float GetGravity() {
		return Climbing ? 0f : 1f;
	}

	public void OnHandsEnter(Collider col) {
		if (!col.tag.Equals("Climbable"))
			return;
		Climbing = true;
		Player.Animator.Play("climb", 0, 1000);
		moveSkill.velocity = Vector2.zero;
		climbableMaxY = col.bounds.max.y;
	}

	public override void OnUpdate() {
		if (Climbing) {
			if (InputManager.Up())
				Speed = 1f;
			else if (InputManager.Down())
				Speed = -1f;
			else
				Speed = 0f;
			if (InputManager.JumpButtonDown())
				Climbing = false;
			if (hoistSkill.HighHoistHandTarget.transform.position.y > climbableMaxY) {
				Climbing = false;
				hoistSkill.Hoist(climbableMaxY, true);
			}
		}
	}

}
