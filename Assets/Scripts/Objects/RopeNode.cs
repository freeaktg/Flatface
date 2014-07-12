using UnityEngine;
using System.Collections;

public class RopeNode : MonoBehaviour {

	public RopeNode TopNode;
	public RopeNode BottomNode;

	public Rope		ParentRope { get; set; }
	public float	Length {
		get {
			if (myLegth == 0)
				myLegth = transform.localScale.y;
			return myLegth;
		}
	}
	float myLegth;

	public Vector2 GetPoint(float f) {
		f -= 0.5f;
		f *= Length;
		return (Vector2)transform.TransformPoint(Vector3.down * f); 
	}
}
