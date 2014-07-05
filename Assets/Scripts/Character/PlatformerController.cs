using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerController : MonoBehaviour {

	public Transform SpriteTransform;

	public enum Actions {
		Jump,
		ChangeDirection
	}

	List<Skill> allSkills = new List<Skill>();

	public int GetFacingDirection() {
		return Mathf.FloorToInt(Mathf.Sign(transform.localScale.x));
	}

	public bool IsActionBlocked(Actions action) {
		foreach (Skill skill in allSkills) {
			if (skill.IsActionBlocked(action))
				return true;
		}
		return false;
	}

	public float GetMoveSpeed() {
		float multiplier = 1f;
		foreach (Skill skill in allSkills)
			multiplier *= skill.GetMoveSpeed();
		return multiplier;
	}

	public void AddSkill(Skill skill) {
		allSkills.Add(skill);
	}
}
