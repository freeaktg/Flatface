using UnityEngine;
using System.Collections;

public class RopeRenderer : MonoBehaviour {

	public int 			NumberOfSegments = 10;
	public Material 	Material;

	Mesh mesh;
	Rope rope;
	int numberOfSegments;

	void Start() {
		rope = GetComponent<Rope>();
		numberOfSegments = NumberOfSegments;
		if (fract(numberOfSegments / (rope.Nodes.Length)) < 0.0001f)
			numberOfSegments++;
		mesh = new Mesh();
		MeshFilter filter = gameObject.AddComponent<MeshFilter>();
		filter.sharedMesh = mesh;
		MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();
		renderer.sharedMaterial = Material;
		Vector3[] vertices = new Vector3[(numberOfSegments + 1) * 2];
		int[] triangles = new int[numberOfSegments * 6];
		Vector2[] UV = new Vector2[(numberOfSegments + 1) * 2];
		Vector3[] normals = new Vector3[(numberOfSegments + 1) * 2];
		for (int i = 0; i < numberOfSegments; i++) {
			triangles[i*6+0] = i * 2;
			triangles[i*6+1] = i * 2 + 1;
			triangles[i*6+2] = i * 2 + 2;

			triangles[i*6+3] = i * 2 + 1;
			triangles[i*6+4] = i * 2 + 3;
			triangles[i*6+5] = i * 2 + 2;

			UV[i*2+0] = new Vector2(0, 0);
			UV[i*2+1] = new Vector2(1, 0);

			normals[i*2+0] = Vector3.back;
			normals[i*2+1] = Vector3.back;
		}
		UV[numberOfSegments*2+0] = new Vector2(0, 0);
		UV[numberOfSegments*2+1] = new Vector2(1, 0);
		normals[numberOfSegments*2+0] = Vector3.back;
		normals[numberOfSegments*2+1] = Vector3.back;
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = UV;
		mesh.normals = normals;
		Vector2 p0 = rope.Nodes[0].GetPoint(0f);
		Vector2 p1 = rope.Nodes[rope.Nodes.Length - 1].GetPoint(1f);
		mesh.bounds = new Bounds((p0 + p1) / 2f - (Vector2)transform.position, new Vector3(5f, p1.y - p0.y, 1f));
		Debug.Log(
		BezierCurve(new Vector2(0, 0), new Vector2(2, 0), new Vector2(1, 1), 0.5f));
	}

	float fract(float f) {
		return f - Mathf.Floor(f);
	}

	void Update() {
		Vector2[] ropePoints = new Vector2[numberOfSegments + 1];
		for (int i = 0; i < numberOfSegments + 1; i++) {
			float f = i / (float)numberOfSegments;
			int nodeIdx = Mathf.FloorToInt(rope.Nodes.Length * Mathf.Clamp(f, 0, 0.999f));
			float fullL = f * rope.Nodes.Length;
			float nodeF = f * rope.Nodes.Length - Mathf.Floor(f * rope.Nodes.Length);
			if (nodeIdx == rope.Nodes.Length - 1 && nodeF == 0)
				nodeF = 1f;
			if (((nodeIdx == 0) && (nodeF < 0.5f)) || ((nodeIdx == rope.Nodes.Length - 1) && (nodeF >= 0.5f)))
				ropePoints[i] = rope.Nodes[nodeIdx].GetPoint(nodeF);
			else {
				float fb = 0f;
				Vector2 p = Vector2.zero;
				if (nodeF < 0.5f) {
					nodeF += 0.5f;
					p = rope.Nodes[nodeIdx].GetPoint(0f);
				} else {
					nodeF -= 0.5f;
					p = rope.Nodes[nodeIdx].GetPoint(1f);
					nodeIdx++;
				}
				ropePoints[i] = BezierCurve(rope.Nodes[nodeIdx-1].GetPoint(0.5f), rope.Nodes[nodeIdx].GetPoint(0.5f), p, nodeF);
				//GizmosHelper.DrawCross(ropePoints[i]);
			}
		}
		Vector2 lastNormal = Vector2.zero;
		for (int i = 0; i < numberOfSegments + 1; i++) {
			Vector2 p = ropePoints[i];
			if (i > 0 && i < numberOfSegments) {
				Vector2 normal = ropePoints[i+1] - ropePoints[i-1];
				SetPoint(i, p, normal);
				lastNormal = normal;
			} else if (i == 0)
				SetPoint(i, p, -Vector2.up);
			else if (i == numberOfSegments)
				SetPoint(i, p, lastNormal);
		}
	}

	Vector2 BezierCurve(Vector2 p0, Vector2 p1, Vector2 pt, float f) {
		return (1f - f)*(p0 + (pt - p0) * f) + f*(p1 + (pt - p1) * (1f - f));
	}

	void SetPoint(int i, Vector2 pos, Vector2 normal) {
		Vector2 right = new Vector2(-normal.normalized.y, normal.normalized.x);
		pos -= (Vector2)transform.position;
		Vector3[] vertices = mesh.vertices;
		vertices[i*2+0] = pos + right * -0.5f * rope.Width;
		vertices[i*2+1] = pos + right * 0.5f * rope.Width;
		mesh.vertices = vertices;
	}
}
