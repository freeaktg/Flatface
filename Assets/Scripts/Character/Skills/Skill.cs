using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviourEx {

	public PlatformerController Player { get; private set; }

	public override void OnAwake() {
		Player = Components.PlatformerController;
		Player.AddSkill(this);
	}

	public virtual bool IsActionBlocked(PlatformerController.Actions action) {
		return false;
	}

	public virtual float GetMoveSpeed() {
		return 1f;
	}

	public virtual float GetGravity() {
		return 1f;
	}

	public virtual float GetVerticalSpeed() {
		return 0f;
	}
}
