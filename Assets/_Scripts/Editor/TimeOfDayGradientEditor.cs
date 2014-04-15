using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class TimeOfDayGradientEditor : EditorWindow {
	public TimeOfDayData data;
	public Texture2D tex;

	[MenuItem ("Window/Time Of Day Gradient Editor")]
	static void Init ()
	{
		TimeOfDayGradientEditor window = (TimeOfDayGradientEditor)EditorWindow.GetWindow (typeof (TimeOfDayGradientEditor));
		window.minSize = new Vector2(300f, 700f);
	}
	
	void OnGUI ()
	{
		data = (TimeOfDayData)EditorGUI.ObjectField(new Rect(10f, 10f, 100f, 15f), data, typeof(TimeOfDayData), false);
		if (GUI.Button(new Rect(120f, 10f, 100f, 15f), "Generate Tex")) {
			Texture2D tex = new Texture2D (256, 256, TextureFormat.RGB24, false);
			PaintTexture(tex);
			var bytes = tex.EncodeToPNG();
			DestroyImmediate(tex);
			File.WriteAllBytes(Application.dataPath + "/_Textures/todGradient.png", bytes);
		}
		if (data != null) {
			bool isDirty = false;
			float cpwidth = 40f;
			float cpheight = 17f;
			for (int i = 0; i < data.palettes.Length; i++) {
				TimeOfDayPalette pal = data.palettes[i];

				float t = Mathf.Clamp01(pal.time / 24f);

				float h = 50f + Mathf.Floor(t * 600f);

				float cw = 10f;

				float c = pal.time;
				pal.time = EditorGUI.FloatField(new Rect(cw, h, cpwidth, cpheight), pal.time);
				if (c != pal.time) isDirty = true;
				cw += cpwidth;

				Color cache = pal.c0;
				pal.c0 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c0);
				if (pal.c0 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c1;
				pal.c1 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c1);
				if (pal.c1 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c2;
				pal.c2 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c2);
				if (pal.c2 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c3;
				pal.c3 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c3);
				if (pal.c3 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c4;
				pal.c4 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c4);
				if (pal.c4 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c5;
				pal.c5 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c5);
				if (pal.c5 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c6;
				pal.c6 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c6);
				if (pal.c6 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c7;
				pal.c7 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c7);
				if (pal.c7 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c8;
				pal.c8 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c8);
				if (pal.c8 != cache) isDirty = true;
				cw += cpwidth;

				cache = pal.c9;
				pal.c9 = EditorGUI.ColorField(new Rect(cw, h, cpwidth, cpheight), pal.c9);
				if (pal.c9 != cache) isDirty = true;
				cw += cpwidth;
			}
			if (isDirty) {
				EditorUtility.SetDirty(data);
			}
		}
	}

	void PaintTexture(Texture2D tex)
	{
		List<TimeOfDayPalette> ps = new List<TimeOfDayPalette>(data.palettes);

		TimeOfDayPalette early = data.palettes[0];
		TimeOfDayPalette late = data.palettes[0];

		for (int k = 1; k < data.palettes.Length; k++) {
			if (early.time > data.palettes[k].time) early = data.palettes[k];
			if (late.time < data.palettes[k].time) late = data.palettes[k];
		}

		early = early.Copy();
		early.time += 24f;
		ps.Add(early);

		late = late.Copy();
		late.time -= 24f;
		ps.Add(late);

		for (int j = 0; j < 256; j++) {
			float t = j / 256f;
			t *= 24f;
			t += 0.0314159f;

			TimeOfDayPalette under = late;
			TimeOfDayPalette over = early;
			for (int k = 0; k < ps.Count; k++) {
				if (ps[k].time <= t && ps[k].time > under.time) under = ps[k];
				if (ps[k].time >= t && ps[k].time < over.time) over = ps[k];
			}
			
			float f = (t - under.time) / (over.time - under.time);

			for (int i = 0; i < 10; i++) {
				Color c = Color.Lerp(under.Colors[i], over.Colors[i], f);
				for (int k = 0; k < 26; k++) {
					if (i * 26 + k < 256) {
						tex.SetPixel(i * 26 + k, j, c);
					}
				}
			}
		}

		tex.Apply();
	}
}

public class TimeOfDayData : ScriptableObject {

	public TimeOfDayPalette[] palettes;

}

[System.Serializable]
public class TimeOfDayPalette {

	public float time;

	public Color c0;
	public Color c1;
	public Color c2;
	public Color c3;
	public Color c4;
	public Color c5;
	public Color c6;
	public Color c7;
	public Color c8;
	public Color c9;

	public TimeOfDayPalette Copy()
	{
		TimeOfDayPalette p = new TimeOfDayPalette();

		p.time = time;
		p.c0 = c0;
		p.c1 = c1;
		p.c2 = c2;
		p.c3 = c3;
		p.c4 = c4;
		p.c5 = c5;
		p.c6 = c6;
		p.c7 = c7;
		p.c8 = c8;
		p.c9 = c9;

		return p;
	}

	public Color[] Colors
	{
		get {
			return new Color[]{c0, c1, c2, c3, c4, c5, c6, c7, c8, c9};
		}
	}
}
