Shader "Gaods/PlayerDiffuse" {
    Properties {
        _NotVisibleColor ("NotVisibleColor (RGB)", Color) = (0.3,0.3,0.3,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags { "Queue" = "AlphaTest+9" "IgnoreProjector"="True" "RenderType"="Opaque" }
        LOD 200
 
        Pass {
            ZTest Greater
            Lighting Off
            ZWrite Off
            //Color [_NotVisibleColor]
            Blend SrcAlpha OneMinusSrcAlpha
            SetTexture [_MainTex] { ConstantColor [_NotVisibleColor] combine constant * texture }
            
        }
 
        Pass {
        	Cull Off
            ZTest LEqual
            //Material {
            //    Diffuse (1,1,1,1)
            //    Ambient (1,1,1,1)
            //}
            Lighting Off
            //SetTexture [_MainTex] { combine texture } 
            CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.texcoord);
				clip(col.a - _Cutoff);
				return col;
			}
		ENDCG
        }
 
    } 
    FallBack "Diffuse"
}