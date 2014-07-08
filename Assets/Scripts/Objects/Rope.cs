using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour {

	public RopeNode FirstNode;
	
	void Awake() {
		RopeNode currentNode = FirstNode;
		do {
			currentNode.ParentRope = this;
			currentNode = currentNode.BottomNode;
		} while (currentNode != null);
	}
}
