Shader "HiroshiRyu/Particles/AdditiveUV (Soft)" {
Properties {
	_MainTex ("Particle Texture", 2D) = "white" {}
	_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
	_XSpeed ("X MoveSpeed", FLOAT) = 0.0
	_YSpeed ("Y MoveSpeed", FLOAT) = 0.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend One OneMinusSrcColor
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }

	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _TintColor;
			fixed _XSpeed;
			fixed _YSpeed;
			
			struct appdata_t {
				fixed4 vertex : POSITION;
				fixed4 color : COLOR;
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f {
				fixed4 vertex : POSITION;
				fixed4 color : COLOR;
				fixed2 texcoord : TEXCOORD0;
				#ifdef SOFTPARTICLES_ON
				fixed4 projPos : TEXCOORD1;
				#endif
			};

			fixed4 _MainTex_ST;
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				#ifdef SOFTPARTICLES_ON
				o.projPos = ComputeScreenPos (o.vertex);
				COMPUTE_EYEDEPTH(o.projPos.z);
				#endif
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			sampler2D _CameraDepthTexture;
			fixed _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{
				#ifdef SOFTPARTICLES_ON
				fixed sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				fixed partZ = i.projPos.z;
				fixed fade = saturate (_InvFade * (sceneZ-partZ));
				i.color.a *= fade;
				#endif
				fixed2 texcoordMove = i.texcoord;
				texcoordMove.x += _Time * _XSpeed;
				texcoordMove.y += _Time * _YSpeed;
				half4 prev = i.color * tex2D(_MainTex, texcoordMove);
				prev.rgb *= prev.a;
				return prev;
			}
			ENDCG 
		}
	} 
}
}