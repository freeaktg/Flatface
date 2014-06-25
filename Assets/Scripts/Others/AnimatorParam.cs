using UnityEngine;
using System.Collections;

public class AnimatorParam<TParam> {

	Animator myAnimator;
	string myName;
	int type = -1;
	TParam currentValue = default(TParam);

	public AnimatorParam(Animator animator, string name) {
		myAnimator = animator;
		myName = name;
		SetType();
	}

	public AnimatorParam(MonoBehaviour behaviour, string name) {
		myAnimator = behaviour.GetComponent<Animator>();
		myName = name;
		SetType();
	}

	void SetType() {
		if (typeof(TParam) == typeof(bool))
			type = 0;
		else if (typeof(TParam) == typeof(int))
			type = 1;
		else if (typeof(TParam) == typeof(float))
			type = 2;
		else if (typeof(TParam) == typeof(AnimatorTrigger))
			type = 3;
	}

	public void Set() {
		if (type == 3)
			Set(default(TParam));
	}

	public void Set(TParam value) {
		if (type != 3 && value.Equals(currentValue))
			return;
		currentValue = value;
		object val = (object)value;
		switch (type) {
		case 0:
			myAnimator.SetBool(myName, (bool)val);
			break;
		case 1:
			myAnimator.SetInteger(myName, (int)val);
			break;
		case 2:
			myAnimator.SetFloat(myName, (float)val);
			break;
		case 3:
			myAnimator.SetTrigger(myName);
			break;

		}
	}

	public static implicit operator TParam(AnimatorParam<TParam> animatorParam) {
		return animatorParam.currentValue;
	}
}

public class AnimatorTrigger { }