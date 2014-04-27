using UnityEngine;
using System.Collections;

public class PlatformerController : MonoBehaviour {

	public int GetFacingDirection() {
		return Mathf.FloorToInt(Mathf.Sign(transform.localScale.x));
	}
}
