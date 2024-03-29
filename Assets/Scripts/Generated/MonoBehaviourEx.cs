//GENERATED BY GENERATEEXTENSIONS DO NOT EDIT
using UnityEngine;
using System.Collections;

public class MonoBehaviourEx : MonoBehaviour {
	
	public static class Components {
		//COMPONENTS START
		public static CharacterFollowCamera CharacterFollowCamera;
		public static PlatformerController PlatformerController;
		public static MoveSkill MoveSkill;
		public static MonoBehaviourEx MonoBehaviourEx;
		public static GameManager GameManager;
		public static GlobalSettings GlobalSettings;
		public static DebugScreen DebugScreen;
		public static OcclusionArea OcclusionArea;
		public static OcclusionPortal OcclusionPortal;
		public static MeshFilter MeshFilter;
		public static SkinnedMeshRenderer SkinnedMeshRenderer;
		public static LensFlare LensFlare;
		public static Renderer Renderer;
		public static Projector Projector;
		public static Skybox Skybox;
		public static TextMesh TextMesh;
		public static ParticleEmitter ParticleEmitter;
		public static ParticleAnimator ParticleAnimator;
		public static TrailRenderer TrailRenderer;
		public static ParticleRenderer ParticleRenderer;
		public static LineRenderer LineRenderer;
		public static GUIElement GUIElement;
		public static GUITexture GUITexture;
		public static GUIText GUIText;
		public static GUILayer GUILayer;
		public static MeshRenderer MeshRenderer;
		public static LODGroup LODGroup;
		public static LightProbeGroup LightProbeGroup;
		public static ParticleSystem ParticleSystem;
		public static ParticleSystemRenderer ParticleSystemRenderer;
		public static SpriteRenderer SpriteRenderer;
		public static Behaviour Behaviour;
		public static Camera Camera;
		public static MonoBehaviour MonoBehaviour;
		public static Component Component;
		public static Light Light;
		public static Transform Transform;
		public static Rigidbody Rigidbody;
		public static Joint Joint;
		public static HingeJoint HingeJoint;
		public static SpringJoint SpringJoint;
		public static FixedJoint FixedJoint;
		public static CharacterJoint CharacterJoint;
		public static ConfigurableJoint ConfigurableJoint;
		public static ConstantForce ConstantForce;
		public static Collider Collider;
		public static BoxCollider BoxCollider;
		public static SphereCollider SphereCollider;
		public static MeshCollider MeshCollider;
		public static CapsuleCollider CapsuleCollider;
		public static WheelCollider WheelCollider;
		public static CharacterController CharacterController;
		public static Cloth Cloth;
		public static InteractiveCloth InteractiveCloth;
		public static SkinnedCloth SkinnedCloth;
		public static ClothRenderer ClothRenderer;
		public static TerrainCollider TerrainCollider;
		public static Rigidbody2D Rigidbody2D;
		public static Collider2D Collider2D;
		public static CircleCollider2D CircleCollider2D;
		public static BoxCollider2D BoxCollider2D;
		public static EdgeCollider2D EdgeCollider2D;
		public static PolygonCollider2D PolygonCollider2D;
		public static Joint2D Joint2D;
		public static SpringJoint2D SpringJoint2D;
		public static DistanceJoint2D DistanceJoint2D;
		public static HingeJoint2D HingeJoint2D;
		public static SliderJoint2D SliderJoint2D;
		public static NavMeshAgent NavMeshAgent;
		public static OffMeshLink OffMeshLink;
		public static NavMeshObstacle NavMeshObstacle;
		public static AudioListener AudioListener;
		public static AudioSource AudioSource;
		public static AudioReverbZone AudioReverbZone;
		public static AudioLowPassFilter AudioLowPassFilter;
		public static AudioHighPassFilter AudioHighPassFilter;
		public static AudioDistortionFilter AudioDistortionFilter;
		public static AudioEchoFilter AudioEchoFilter;
		public static AudioChorusFilter AudioChorusFilter;
		public static AudioReverbFilter AudioReverbFilter;
		public static Animation Animation;
		public static Animator Animator;
		public static Terrain Terrain;
		public static Tree Tree;
		//COMPONENTS END
	}

	bool inited;

	void InitComponents () {


		inited = true;
		Component[] components = GetComponents<Component>();

		foreach (Component c in components) {
			switch (c.GetType().Name) {
				//ASSIGNS START
				case "CharacterFollowCamera":
					Components.CharacterFollowCamera = c as CharacterFollowCamera;
					break;
				case "PlatformerController":
					Components.PlatformerController = c as PlatformerController;
					break;
				case "MoveSkill":
					Components.MoveSkill = c as MoveSkill;
					break;
				case "MonoBehaviourEx":
					Components.MonoBehaviourEx = c as MonoBehaviourEx;
					break;
				case "GameManager":
					Components.GameManager = c as GameManager;
					break;
				case "GlobalSettings":
					Components.GlobalSettings = c as GlobalSettings;
					break;
				case "DebugScreen":
					Components.DebugScreen = c as DebugScreen;
					break;
				case "OcclusionArea":
					Components.OcclusionArea = c as OcclusionArea;
					break;
				case "OcclusionPortal":
					Components.OcclusionPortal = c as OcclusionPortal;
					break;
				case "MeshFilter":
					Components.MeshFilter = c as MeshFilter;
					break;
				case "SkinnedMeshRenderer":
					Components.SkinnedMeshRenderer = c as SkinnedMeshRenderer;
					break;
				case "LensFlare":
					Components.LensFlare = c as LensFlare;
					break;
				case "Renderer":
					Components.Renderer = c as Renderer;
					break;
				case "Projector":
					Components.Projector = c as Projector;
					break;
				case "Skybox":
					Components.Skybox = c as Skybox;
					break;
				case "TextMesh":
					Components.TextMesh = c as TextMesh;
					break;
				case "ParticleEmitter":
					Components.ParticleEmitter = c as ParticleEmitter;
					break;
				case "ParticleAnimator":
					Components.ParticleAnimator = c as ParticleAnimator;
					break;
				case "TrailRenderer":
					Components.TrailRenderer = c as TrailRenderer;
					break;
				case "ParticleRenderer":
					Components.ParticleRenderer = c as ParticleRenderer;
					break;
				case "LineRenderer":
					Components.LineRenderer = c as LineRenderer;
					break;
				case "GUIElement":
					Components.GUIElement = c as GUIElement;
					break;
				case "GUITexture":
					Components.GUITexture = c as GUITexture;
					break;
				case "GUIText":
					Components.GUIText = c as GUIText;
					break;
				case "GUILayer":
					Components.GUILayer = c as GUILayer;
					break;
				case "MeshRenderer":
					Components.MeshRenderer = c as MeshRenderer;
					break;
				case "LODGroup":
					Components.LODGroup = c as LODGroup;
					break;
				case "LightProbeGroup":
					Components.LightProbeGroup = c as LightProbeGroup;
					break;
				case "ParticleSystem":
					Components.ParticleSystem = c as ParticleSystem;
					break;
				case "ParticleSystemRenderer":
					Components.ParticleSystemRenderer = c as ParticleSystemRenderer;
					break;
				case "SpriteRenderer":
					Components.SpriteRenderer = c as SpriteRenderer;
					break;
				case "Behaviour":
					Components.Behaviour = c as Behaviour;
					break;
				case "Camera":
					Components.Camera = c as Camera;
					break;
				case "MonoBehaviour":
					Components.MonoBehaviour = c as MonoBehaviour;
					break;
				case "Component":
					Components.Component = c as Component;
					break;
				case "Light":
					Components.Light = c as Light;
					break;
				case "Transform":
					Components.Transform = c as Transform;
					break;
				case "Rigidbody":
					Components.Rigidbody = c as Rigidbody;
					break;
				case "Joint":
					Components.Joint = c as Joint;
					break;
				case "HingeJoint":
					Components.HingeJoint = c as HingeJoint;
					break;
				case "SpringJoint":
					Components.SpringJoint = c as SpringJoint;
					break;
				case "FixedJoint":
					Components.FixedJoint = c as FixedJoint;
					break;
				case "CharacterJoint":
					Components.CharacterJoint = c as CharacterJoint;
					break;
				case "ConfigurableJoint":
					Components.ConfigurableJoint = c as ConfigurableJoint;
					break;
				case "ConstantForce":
					Components.ConstantForce = c as ConstantForce;
					break;
				case "Collider":
					Components.Collider = c as Collider;
					break;
				case "BoxCollider":
					Components.BoxCollider = c as BoxCollider;
					break;
				case "SphereCollider":
					Components.SphereCollider = c as SphereCollider;
					break;
				case "MeshCollider":
					Components.MeshCollider = c as MeshCollider;
					break;
				case "CapsuleCollider":
					Components.CapsuleCollider = c as CapsuleCollider;
					break;
				case "WheelCollider":
					Components.WheelCollider = c as WheelCollider;
					break;
				case "CharacterController":
					Components.CharacterController = c as CharacterController;
					break;
				case "Cloth":
					Components.Cloth = c as Cloth;
					break;
				case "InteractiveCloth":
					Components.InteractiveCloth = c as InteractiveCloth;
					break;
				case "SkinnedCloth":
					Components.SkinnedCloth = c as SkinnedCloth;
					break;
				case "ClothRenderer":
					Components.ClothRenderer = c as ClothRenderer;
					break;
				case "TerrainCollider":
					Components.TerrainCollider = c as TerrainCollider;
					break;
				case "Rigidbody2D":
					Components.Rigidbody2D = c as Rigidbody2D;
					break;
				case "Collider2D":
					Components.Collider2D = c as Collider2D;
					break;
				case "CircleCollider2D":
					Components.CircleCollider2D = c as CircleCollider2D;
					break;
				case "BoxCollider2D":
					Components.BoxCollider2D = c as BoxCollider2D;
					break;
				case "EdgeCollider2D":
					Components.EdgeCollider2D = c as EdgeCollider2D;
					break;
				case "PolygonCollider2D":
					Components.PolygonCollider2D = c as PolygonCollider2D;
					break;
				case "Joint2D":
					Components.Joint2D = c as Joint2D;
					break;
				case "SpringJoint2D":
					Components.SpringJoint2D = c as SpringJoint2D;
					break;
				case "DistanceJoint2D":
					Components.DistanceJoint2D = c as DistanceJoint2D;
					break;
				case "HingeJoint2D":
					Components.HingeJoint2D = c as HingeJoint2D;
					break;
				case "SliderJoint2D":
					Components.SliderJoint2D = c as SliderJoint2D;
					break;
				case "NavMeshAgent":
					Components.NavMeshAgent = c as NavMeshAgent;
					break;
				case "OffMeshLink":
					Components.OffMeshLink = c as OffMeshLink;
					break;
				case "NavMeshObstacle":
					Components.NavMeshObstacle = c as NavMeshObstacle;
					break;
				case "AudioListener":
					Components.AudioListener = c as AudioListener;
					break;
				case "AudioSource":
					Components.AudioSource = c as AudioSource;
					break;
				case "AudioReverbZone":
					Components.AudioReverbZone = c as AudioReverbZone;
					break;
				case "AudioLowPassFilter":
					Components.AudioLowPassFilter = c as AudioLowPassFilter;
					break;
				case "AudioHighPassFilter":
					Components.AudioHighPassFilter = c as AudioHighPassFilter;
					break;
				case "AudioDistortionFilter":
					Components.AudioDistortionFilter = c as AudioDistortionFilter;
					break;
				case "AudioEchoFilter":
					Components.AudioEchoFilter = c as AudioEchoFilter;
					break;
				case "AudioChorusFilter":
					Components.AudioChorusFilter = c as AudioChorusFilter;
					break;
				case "AudioReverbFilter":
					Components.AudioReverbFilter = c as AudioReverbFilter;
					break;
				case "Animation":
					Components.Animation = c as Animation;
					break;
				case "Animator":
					Components.Animator = c as Animator;
					break;
				case "Terrain":
					Components.Terrain = c as Terrain;
					break;
				case "Tree":
					Components.Tree = c as Tree;
					break;
//ASSIGNS END
			}
		}
		OnCacheComponents();
	}

	void Awake() {
		InitComponents();
		OnAwake();
	}

	void Start() {
		OnStart();
	}
	
	// Update is called once per frame
	void Update () {
		if (!inited)
			InitComponents();
		OnUpdate();
	}

	void FixedUpdate() {
		OnFixedUpdate();
	}

	void LateUpdate() {
		OnLateUpdate();
	}

	public virtual void OnCacheComponents() { }
	public virtual void OnUpdate() { }
	public virtual void OnStart() { }
	public virtual void OnAwake() { }
	public virtual void OnFixedUpdate() { }
	public virtual void OnLateUpdate() { }
}
