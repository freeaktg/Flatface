using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rope : MonoBehaviour {

	public int		NodeCount = 4;
	public float	Length = 10;
	public float	Width = 1f;
	public float	Mass = 1f;
	public float	LinearDrag = 0.5f;
	public float	AngularDrag = 0.5f;
	public float	AngleLimit = 180f;

	public RopeNode[] Nodes { get; private set; }

	void Awake() {
		CreateNodes();
	}

	void CreateNodes() {
		float nodeLength = Length / (float)NodeCount;
		Nodes = new RopeNode[NodeCount];
		for (int i = 0; i < NodeCount; i++) {
			GameObject go = new GameObject("RopeNode" + i);
			go.transform.parent = transform;
			go.transform.localPosition = -Vector2.up * nodeLength * i;
			go.transform.localScale = new Vector3(Width, nodeLength, 1f);
			go.tag = "Climbable";
			go.layer = LayerMask.NameToLayer("Rope");
			RopeNode node = go.AddComponent<RopeNode>();
			if (i > 0) {
				node.TopNode = Nodes[i-1];
				Nodes[i-1].BottomNode = node;
			}
			Rigidbody2D body = go.AddComponent<Rigidbody2D>();
			body.mass = Mass / (float)NodeCount;
			body.drag = LinearDrag;
			body.angularDrag = AngularDrag;
			BoxCollider2D box = go.AddComponent<BoxCollider2D>();
			box.size = Vector2.one;
			HingeJoint2D hinge = go.AddComponent<HingeJoint2D>();
			hinge.anchor = new Vector2(0f, 0.5f);
			if (i == 0) {
				hinge.connectedBody = rigidbody2D;
				hinge.connectedAnchor = new Vector2(0f, 0f);
			} else {
				hinge.connectedBody = node.TopNode.rigidbody2D;
				hinge.connectedAnchor = new Vector2(0f, -0.5f);
				hinge.useLimits = !true;
				//hinge.limits = new JointAngleLimits2D() {
				//	min = AngleLimit,
				//	max = -AngleLimit
				//};
			}
			node.ParentRope = this;
			Nodes[i] = node;
		}
	}
}
