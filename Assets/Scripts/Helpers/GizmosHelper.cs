using UnityEngine;
using System.Collections;

public static class GizmosHelper {

	public static void DrawCross(Vector3 point, Color color = default(Color), float size = 1f, bool debug = true) {
		if (color == default(Color))
			color = Color.red;
		if (debug) {
			Debug.DrawLine(point + Vector3.up * (size / 2f), point - Vector3.up * (size / 2f), color);
			Debug.DrawLine(point + Vector3.right * (size / 2f), point - Vector3.right * (size / 2f), color);
		} else {
			Gizmos.color = color;
			Gizmos.DrawLine(point + Vector3.up * (size / 2f), point - Vector3.up * (size / 2f));
			Gizmos.DrawLine(point + Vector3.right * (size / 2f), point - Vector3.right * (size / 2f));
		}
	}
}
