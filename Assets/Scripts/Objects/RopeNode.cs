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
}
