using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoistSkill : Skill {

	public GameObject HighHoistHandTarget;
	public GameObject LowHoistListenerTarget;
	public float MaxHoistGap = 0.1f;
	public float HighHoistTime = 1f;
	public Vector2 HighHoistOffset;

	AnimatorParam<AnimatorTrigger> highHoistTrigger;

	void Start() {
		highHoistTrigger = new AnimatorParam<AnimatorTrigger> (Components.Animator, "HighHoist");
	}

	public void OnHandsListenerEnter(Collider col) {
		float height = col.bounds.max.y;
		if (Mathf.Abs (HighHoistHandTarget.transform.position.y - height) < MaxHoistGap)
			StartCoroutine_Auto (HighHoist (height));
	}

	IEnumerator HighHoist(float height) {
		highHoistTrigger.Set();
		float time = 0;
		Vector3 startPos = transform.position;
		Vector3 off = HighHoistOffset;
		off.x *= Player.GetFacingDirection ();
		Vector3 endPos = startPos + off;
		endPos.y = height + HighHoistOffset.y;
		while (time < HighHoistTime) {
			time += Time.deltaTime;
			float f = time / HighHoistTime;
			transform.position = Vector3.Lerp(startPos, endPos, f);
			yield return 0;
		}
		transform.position = endPos;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube (transform.position + (Vector3)HighHoistOffset, Vector3.one * 0.1f);
	}
}
