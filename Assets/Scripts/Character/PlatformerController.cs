﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformerController : MonoBehaviour {

	public Transform SpriteTransform;

	public int GetFacingDirection() {
		return Mathf.FloorToInt(Mathf.Sign(transform.localScale.x));
	}
}
