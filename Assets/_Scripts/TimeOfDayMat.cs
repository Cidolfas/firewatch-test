using UnityEngine;
using System.Collections;

public class TimeOfDayMat : MonoBehaviour {

	public const float CycleTime = 30f;

	private Material _mat;

	void Awake()
	{
		_mat = renderer.material;
	}

	void Update()
	{
		_mat.SetFloat ("_TimeOfDay", (Time.time % CycleTime) / CycleTime);
	}

}
