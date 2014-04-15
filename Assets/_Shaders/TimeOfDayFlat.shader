Shader "Cid/TimeOfDay Flat" {
	Properties {
		_MainTex ("Mask (A)", 2D) = "white" {}
		_RampTex ("Ramp (RGB)", 2D) = "white" {}
		_TimeOfDay ("TOD", Range(0,1)) = 0.0
		_ReadChannel("Palette Channel (0-9)", Float) = 0.0
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Unlit alpha
		
		half4 LightingUnlit (SurfaceOutput s, half3 lightDir, half atten) {
			half4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}

		sampler2D _MainTex;
		sampler2D _RampTex;
		float _TimeOfDay;
		float _ReadChannel;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			float palette = (_ReadChannel + 0.5) / 10.0;
			half4 t = tex2D (_RampTex, float2(palette, _TimeOfDay));
			o.Albedo = t.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
