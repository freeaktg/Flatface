using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviourEx {

	public PlatformerController Player { get; private set; }

	public override void OnAwake() {
		Player = Components.PlatformerController;
	}
}
