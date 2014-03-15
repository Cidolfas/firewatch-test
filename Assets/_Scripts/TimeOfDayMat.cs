using UnityEngine;
using System.Collections;

public class TimeOfDayMat : MonoBehaviour {

	public const float CycleTime = 30f;
	public AnimationCurve tweakCurve;
	public Material[] mats;

	void Update()
	{
		float tod = (Time.time % CycleTime) / CycleTime;
		tod = tweakCurve.Evaluate (tod);
		for (int i = 0; i < mats.Length; i++) {
			mats[i].SetFloat ("_TimeOfDay", tod);
		}
	}

	void OnDisable()
	{
		for (int i = 0; i < mats.Length; i++) {
			mats[i].SetFloat ("_TimeOfDay", 0f);
		}
	}

}
