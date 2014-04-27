using UnityEngine;
using System.Collections;

public class CharacterFollowCamera : MonoBehaviourEx {

	public Transform 	Target;
	public float		FollowDamping = 0.1f;

	public override void OnLateUpdate () {
		Vector3 pos = transform.position;
		pos = Vector3.Lerp(pos, new Vector3(Target.position.x, Target.position.y, pos.z), Time.deltaTime * FollowDamping);
		transform.position = pos;
	}
}
