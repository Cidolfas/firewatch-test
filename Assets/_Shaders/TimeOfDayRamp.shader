Shader "Cid/TimeOfDay Lit Solid" {
	Properties {
		_RampTex ("Ramp (RGB)", 2D) = "white" {}
		_TimeOfDay ("TOD", Range(0,1)) = 0.0
		_ReadChannel("Palette Channel (0-9)", Float) = 0.0
		_HighChannel("Highlight Channel (0-9)", Float) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Ramp
		#include "UnityCG.cginc"
		
		sampler2D _RampTex;
		sampler2D _HighlightTex;
		float _TimeOfDay;
		float _ReadChannel;
		float _HighChannel;
		
		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = smoothstep(0, 1.0, NdotL);
			float palette = (_ReadChannel + 0.5) / 10.0;
			float palette2 = (_HighChannel + 0.5) / 10.0;
			half3 ramp = tex2D (_RampTex, float2(palette, _TimeOfDay)).rgb;
			half3 hlight = tex2D (_RampTex, float2(palette2, _TimeOfDay)).rgb;
			half4 c;
			c.rgb = lerp(ramp, hlight, diff);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			o.Albedo = float3(1.0,1.0,1.0);
			o.Alpha = 1.0;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
