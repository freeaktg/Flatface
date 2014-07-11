using UnityEngine;
using System.Collections;

public static class Extensions {

	public static Bounds BoundsOf(Collider2D collider) {
		var bounds = new Bounds();
		
		var bc = collider as BoxCollider2D;
		if (bc) {
			var ext = bc.size * 0.5f;
			bounds.Encapsulate(new Vector3(-ext.x, -ext.y, 0f));
			bounds.Encapsulate(new Vector3(ext.x, ext.y, 0f));
			return bounds;
		}
		
		var cc = collider as CircleCollider2D;
		if (cc) {
			var r = cc.radius;
			bounds.Encapsulate(new Vector3(-r, -r, 0f));
			bounds.Encapsulate(new Vector3(r, r, 0f));
			return bounds;
		}
		
		
		// others :P
		Debug.LogWarning("Unknown type "+bounds);
		
		return bounds;
	}

	public static Bounds GetBounds(this Collider2D col) {
		var bounds = new Bounds(col.transform.position, Vector3.zero);
		

		var blocal = BoundsOf(col);
		var t = col.transform;
		var max = t.TransformPoint(blocal.max);
		bounds.Encapsulate(max);
		var min = t.TransformPoint(blocal.min);
		bounds.Encapsulate(min);
		
		return bounds;
	}
}