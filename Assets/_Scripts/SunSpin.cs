using UnityEngine;
using System.Collections;

public class SunSpin : MonoBehaviour {

	private Vector3 startSpin;
	public AnimationCurve tiltCurve;

	void Start()
	{
		startSpin = transform.eulerAngles;
	}

	void Update()
	{
		float t = (Time.time % TimeOfDayMat.CycleTime) / TimeOfDayMat.CycleTime;

		transform.eulerAngles = new Vector3(startSpin.x, tiltCurve.Evaluate(t) * 360f + startSpin.y, startSpin.z);
	}

}
