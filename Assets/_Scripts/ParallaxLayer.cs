using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {

	public float extentX;
	public float extentY;

	private Vector3 center;

	void Start()
	{
		center = transform.localPosition;
	}

	void Update()
	{
		Vector3 mousePos = Input.mousePosition;

		// Range [-1, 1] all axes
		Vector3 screenPos = new Vector3 (Mathf.Clamp01(mousePos.x / (float)Screen.width) * 2f - 1f, Mathf.Clamp01(mousePos.y / (float)Screen.height) * 2f - 1f, 0f);

		Vector3 desiredPos = center + new Vector3 (screenPos.x * extentX, screenPos.y * extentY, 0f);

		transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPos, 0.02f);
	}

}
