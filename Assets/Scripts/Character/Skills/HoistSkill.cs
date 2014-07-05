using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoistSkill : Skill {

	public GameObject HighHoistHandTarget;
	public GameObject LowHoistHandTarget;
	public float MaxHoistGap = 0.1f;
	public float HighHoistTime = 1f;
	public float LowHoistTime = 1f;
	public Vector2 HighHoistOffset;
	public Vector2 LowHoistOffset;

	AnimatorParam<AnimatorTrigger> highHoistTrigger;
	AnimatorParam<AnimatorTrigger> lowHoistTrigger;
	Animator animator;

	void Start() {
		highHoistTrigger = new AnimatorParam<AnimatorTrigger> (Components.Animator, "HighHoist");
		lowHoistTrigger = new AnimatorParam<AnimatorTrigger> (Components.Animator, "LowHoist");
		animator = GetComponent<Animator>();
	}

	public void OnHandsEnter(Collider col) {
		if (col.tag != "Hoistable")
			return;
		float height = col.bounds.max.y;
		if (Mathf.Abs (HighHoistHandTarget.transform.position.y - height) < MaxHoistGap)
			Hoist (height, true);
		if (Mathf.Abs (LowHoistHandTarget.transform.position.y - height) < MaxHoistGap)
			Hoist (height, false);
	}

	bool hoisting, high, hoistStarted;
	float hoistEnd;
	void Hoist(float height, bool high) {
		if (high)
			highHoistTrigger.Set();
		else
			lowHoistTrigger.Set();
		hoisting = true;
		this.high = high;
	}

	public void HoistEnd() {
		Debug.Log(Player.SpriteTransform.localPosition);
	}

	Vector3 hoistPos;
	public override void OnUpdate() {
		if (hoisting && hoistStarted) {
			string stateName = high ? "hoist_high" : "hoist_low";
			hoistPos = Player.SpriteTransform.position;
		}
	}

	public override void OnLateUpdate() {
		if (hoisting) {
			string stateName = high ? "hoist_high" : "hoist_low";
			if (!hoistStarted && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
				hoistStarted = true;
			if (hoistStarted) {
				if (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName)) {
					hoisting = false;
					hoistStarted = false;
					transform.position = hoistPos + new Vector3(0f, high ? 0.2f : 0.2f, 0f);
					Player.SpriteTransform.localPosition = Vector2.zero;
				}
			}
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube (transform.position + (Vector3)HighHoistOffset, Vector3.one * 0.1f);
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (transform.position + (Vector3)LowHoistOffset, Vector3.one * 0.1f);
	}
}
