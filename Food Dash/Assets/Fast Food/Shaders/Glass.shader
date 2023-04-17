Shader "Unlit/Glass"
{
	Properties{
	_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
	_ReflColor("Relection Color", Color) = (0.26,0.19,0.16,0.0)
	_RimColor("Rim Color", Color) = (0.26,0.19,0.16,0.0)
	_RimPower("Rim Strength", Range(0.5,8.0)) = 3.0
	_AlphPower("Alpha Rim Power", Range(0.0,8.0)) = 3.0
	_AlphaMin("Alpha Minimum", Range(0.0,1.0)) = 0.5
	 _SpecColor("Specular Color", Color) = (0.5, 0.5, 0.5, 1)
	_Shininess("Shininess", Range(0.01, 1)) = 0.078125

	}
		SubShader{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }


		CGPROGRAM
		#pragma surface surf BlinnPhong alpha
		struct Input {
		float2 uv_MainTex;

		float3 viewDir;
		};
		fixed4 _Color;
		sampler2D _MainTex;



		float4 _RimColor;
		float4 _ReflColor;
		float _RimPower;
		float _AlphPower;
		float _AlphaMin;
		float _Shininess;


		void surf(Input IN, inout SurfaceOutput o) {
		fixed4 tex = tex2D(_MainTex, IN.uv_MainTex) + _Shininess;
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb + _ReflColor;
		o.Gloss = tex.a;
		half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
		o.Emission = _RimColor.rgb * pow(rim, _RimPower);
		o.Alpha = (pow(rim, _AlphPower)*(1 - _AlphaMin)) + _AlphaMin * tex.a * _Color.a;
		o.Specular = _Shininess;



		}
		ENDCG
	}
		Fallback "VertexLit"
}
