using UnityEngine;
using System.Collections;

public class FullscreenManager : MonoBehaviour {

	public Vector2 startRes;

	private bool _isFullscreen = false;

	void Start()
	{
		_isFullscreen = Screen.fullScreen;
		startRes = new Vector2(Screen.width, Screen.height);
	}

	void Update()
	{
		if (Screen.fullScreen != _isFullscreen) {
			if (Screen.fullScreen) {
				// Get aspect
				Vector2 maxRes = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
				float invAspect = maxRes.y / maxRes.x;
				float modAspect = invAspect * 16f / 6f;
				Camera.main.rect = new Rect(0f, 0.5f - modAspect/2f, 1f, modAspect);
			} else {
				Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
			}
			_isFullscreen = Screen.fullScreen;
		}
	}

}
