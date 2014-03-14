Shader "Cid/TimeOfDay Ramp" {
	Properties {
		_MainTex ("Mask (A)", 2D) = "white" {}
		_RampTex ("Ramp (RGB)", 2D) = "white" {}
		_TimeOfDay ("TOD", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Ramp alpha
		#include "UnityCG.cginc"
		
		sampler2D _MainTex;
		sampler2D _RampTex;
		float _TimeOfDay;
		
		half4 LightingRamp (SurfaceOutput s, half3 lightDir, half atten) {
			half NdotL = dot (s.Normal, lightDir);
			half diff = NdotL * 0.5 + 0.5;
			half3 ramp = tex2D (_RampTex, float2(diff, _TimeOfDay)).rgb;
			half4 c;
			c.rgb = _LightColor0.rgb * ramp * (atten * 2);
			c.a = s.Alpha;
			return c;
		}

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			half4 t = tex2D (_RampTex, float2(_TimeOfDay, 0.5));
			o.Albedo = ceil(c.a) * t;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
