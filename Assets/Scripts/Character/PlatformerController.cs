using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerController : MonoBehaviour {

	public Transform SpriteTransform;
	public Animator Animator { get; private set; }

	public enum Actions {
		Jump,
		ChangeDirection
	}

	List<Skill> allSkills = new List<Skill>();

	void Awake() {
		Animator = GetComponent<Animator>();
	}

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

	public float GetGravity() {
		float multiplier = 1f;
		foreach (Skill skill in allSkills)
			multiplier *= skill.GetGravity();
		return multiplier;
	}

	public float GetVerticalSpeed() {
		float vertical = 0f;
		foreach (Skill skill in allSkills)
			vertical += skill.GetVerticalSpeed();
		return vertical;
	}

	public void AddSkill(Skill skill) {
		allSkills.Add(skill);
	}

	public bool SkillsUpdatePosition() {
		bool positionUpdated = false;
		foreach (Skill skill in allSkills) {
			if (skill.UpdatePosition())
				positionUpdated = true;
		}
		return positionUpdated;
	}
}
