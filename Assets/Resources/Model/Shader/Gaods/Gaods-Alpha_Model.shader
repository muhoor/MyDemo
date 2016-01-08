Shader "Gaods/Alpha_Model" {
Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
//	_Cutoff ("Base Alpha cutoff", Range (0,.9)) = .5
}

SubShader {
//	Tags { "RenderType"="Transparent" }
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
//	Cull Off
	LOD 100
	
	Pass {
		//Cull Off  
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			float4 _Color;
//			float _Cutoff;
			
			struct appdata_t {
				float4 vertex : POSITION;
//				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
//				float4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};

			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
//				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			
			fixed4 frag (v2f i) : SV_Target
			{
		//		fixed4 col = tex2D(_MainTex, i.texcoord) * _Color;
				fixed4 col = tex2D(_MainTex, i.texcoord);
				col.a = col.a * _Color.a;
				col.xyz = col.xyz + _Color;
				return col;
//				return i.color * _TintColor * tex2D(_MainTex, i.texcoord);
	//			return _Color * tex2D(_MainTex, i.texcoord);
			}
		ENDCG
	}
}
}