Shader "Cid/TimeOfDay Lit Alpha (Manual Normals)" {
	Properties {
		_MainTex ("Mask (A)", 2D) = "white" {}
		_RampTex ("Ramp (RGB)", 2D) = "white" {}
		_HighlightTex ("Highlight (RGB)", 2D) = "white" {}
		_TimeOfDay ("TOD", Range(0,1)) = 0.0
		_ManualNormal ("Manual Normal", Vector) = (0.0, 1.0, 0.0, 0.0)
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Ramp alpha
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _RampTex;
		sampler2D _HighlightTex;
		float _TimeOfDay;
		half4 _ManualNormal;
		
		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (normalize(_ManualNormal.rgb), lightDir);
			half diff = smoothstep(0, 1.0, NdotL);
			half3 ramp = tex2D (_RampTex, float2(_TimeOfDay, 0.5)).rgb;
			half3 hlight = tex2D (_HighlightTex, float2(_TimeOfDay, 0.5)).rgb;
			half4 c;
			c.rgb = lerp(ramp, hlight, diff);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = float3(1.0,1.0,1.0);
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
