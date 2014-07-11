using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MoveSkill))]
[RequireComponent(typeof(HoistSkill))]
public class ClimbSkill : Skill {

	public float		RopeSwingForce = 1f;

	AnimatorParam<bool> climbing;
	MoveSkill 			moveSkill;
	HoistSkill			hoistSkill;
	float				climbableMaxY;
	bool				ropeClimbing;
	Rope				currentRope;
	RopeNode			currentRopeNode;
	float				positionOnRopeNode;
	Transform			handsListener;
	Vector3				handsOffset;
	int					ropeClimbStartFacing;
	Vector3				handsPosition;
	Rope				ignoreRope;

	public override void OnStart() {
		climbing = new AnimatorParam<bool>(this, "Climbing");
		moveSkill = GetComponent<MoveSkill>();
		hoistSkill = GetComponent<HoistSkill>();
		handsListener = transform.FindChild("HandsListener");
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

	public override bool UpdatePosition() {
		if (Climbing && ropeClimbing) {
			handsPosition = Vector3.Lerp(
				currentRopeNode.transform.TransformPoint(Vector3.down * (currentRopeNode.Length / 2f)),
				currentRopeNode.transform.TransformPoint(Vector3.up * (currentRopeNode.Length / 2f)),
				positionOnRopeNode) - currentRopeNode.transform.TransformDirection(handsOffset);
			transform.position = handsPosition;
			transform.rotation = currentRopeNode.transform.rotation;
			return true;
		}
		return false;
	}

	public override float GetGravity() {
		return Climbing ? 0f : 1f;
	}

	public void OnHandsEnter(Collider2D col) {
		if (Climbing || !col.tag.Equals("Climbable"))
			return;
		if (col.gameObject.layer == 11) {
			ropeClimbing = true;
			currentRopeNode = col.GetComponent<RopeNode>();
			currentRope = currentRopeNode.ParentRope;
			CalculateRopePosition();
		} else
			ropeClimbing = false;
		ropeClimbStartFacing = Player.GetFacingDirection();
		Climbing = true;
		Player.Animator.Play("climb", 0, 1000);
		moveSkill.velocity = Vector2.zero;
		climbableMaxY = col.GetBounds().max.y;
	}

	public override void OnUpdate() {
		if (Climbing) {
			if (InputManager.Up())
				Speed = 1f;
			else if (InputManager.Down())
				Speed = -1f;
			else
				Speed = 0f;
			if (ropeClimbing) {
				positionOnRopeNode += 0.05f * Speed;
				if (InputManager.Left())
					currentRopeNode.rigidbody2D.AddForceAtPosition(currentRopeNode.transform.right * -RopeSwingForce, handsPosition);
				else if (InputManager.Right())
					currentRopeNode.rigidbody2D.AddForceAtPosition(currentRopeNode.transform.right * RopeSwingForce, handsPosition);
			}
			if (positionOnRopeNode > 1f) {
				positionOnRopeNode -= 1f;
				currentRopeNode = currentRopeNode.TopNode;
			}
			if (positionOnRopeNode < 0f) {
				positionOnRopeNode += 1f;
				currentRopeNode = currentRopeNode.BottomNode;
			}
			if (InputManager.JumpButtonDown() || currentRopeNode == null) {
				Climbing = false;
				if (ropeClimbing)
					moveSkill.velocity = currentRopeNode.rigidbody2D.velocity;
				ignoreRope = currentRope;
				if (ropeClimbStartFacing == 1f)
					transform.rotation = Quaternion.identity;
				else
					transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
			}
			if (!ropeClimbing && hoistSkill.HighHoistHandTarget.transform.position.y > climbableMaxY) {
				Climbing = false;
				hoistSkill.Hoist(climbableMaxY, true);
			}
		} else if (moveSkill.IsGrounded)
			ignoreRope = null;
	}

	public override bool IsActionBlocked(PlatformerController.Actions action) {
		if (action == PlatformerController.Actions.ChangeDirection)
			return Climbing;
		return false;
	}

	void OnDrawGizmosSelected() {
		if (ropeClimbing) {
			Gizmos.color = Color.red;
			Gizmos.DrawSphere(handsPosition, 0.1f);
		}
	}

	void CalculateRopePosition() {
		Vector3 myPt = handsListener.transform.position;
		Vector3 topPt = currentRopeNode.transform.TransformPoint(Vector3.up * (currentRopeNode.Length / 2f));
		Vector3 bottomPt = currentRopeNode.transform.TransformPoint(Vector3.down * (currentRopeNode.Length / 2f));
		Vector3 rpt = ClosestPoint(topPt, bottomPt, myPt);
		positionOnRopeNode = Mathf.InverseLerp(bottomPt.y, topPt.y, rpt.y);
		handsOffset = myPt - transform.position;
	}

	Vector3 ClosestPoint(Vector3 a1, Vector3 a2, Vector3 p) {
		Vector3 lhs = Vector3.Cross(a2 - a1, p - a1);
		Vector3 rhs = Vector3.Cross(lhs, a2 - a1);
		Vector3 outP;
		if (Intersection(a1, a2, p, p + rhs, out outP))
			return outP;
		return Vector3.zero;
	}

	bool Intersection(Vector3 a1, Vector3 a2, Vector3 b1, Vector3 b2, out Vector3 intersecion) {
		Vector3 da = a2 - a1; 
		Vector3 db = b2 - b1;
		Vector3 dc = b1 - a1;
		intersecion = a1;
		
		//if (Vector3.Dot(dc, Vector3.Cross(da,db)) != 0.0) // lines are not coplanar
			//return false;
		Vector3 cr = Vector3.Cross(da,db);
		float norm2 = cr.x*cr.x + cr.y*cr.y + cr.z*cr.z;
		float s = Vector3.Dot(Vector3.Cross(dc,db), Vector3.Cross(da,db)) / norm2;
		if (s >= 0.0 && s <= 1.0)
		{
			intersecion = a1 + da * s;
			return true;
		}
		
		return false;
	}
}
