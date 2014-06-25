using UnityEngine;
using System.Collections;

public abstract class Skill : MonoBehaviourEx {

	public PlatformerController Player { get; private set; }

	public override void OnAwake() {
		Player = Components.PlatformerController;
	}
}
