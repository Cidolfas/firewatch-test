using UnityEngine;
using UnityEditor;
using System.Collections;

public class TimeOfDayGradientAsset {

	[MenuItem("Assets/Create/Time of Day Gradient")]
	public static void CreateAsset ()
	{
		CustomAssetUtility.CreateAsset<TimeOfDayData> ();
	}

}
