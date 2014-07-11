using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerController : MonoBehaviour {

	public Transform SpriteTransform;
	public float Height;
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

	void OnDrawGizmosSelected() {
		CircleCollider2D circle = GetComponent<CircleCollider2D>();
		Vector3 d = transform.position + (Vector3)Mul(circle.center, (Vector2)transform.localScale) +
			Vector3.down * circle.radius * transform.localScale.y;
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(d, d + Vector3.up * Height);

	}

	Vector2 Mul(Vector2 v1, Vector2 v2) {
		return new Vector2(v1.x * v2.x, v1.y * v2.y);
	}
}
