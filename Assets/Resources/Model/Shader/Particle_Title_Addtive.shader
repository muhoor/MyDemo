Shader "TianShen/Particle Title Additve" {
    Properties {
        _TintColor ("TintColor", Color) = (0.5,0.5,0.5,1)
        _MainTex ("Main Texture", 2D) = "white" {}
        _Alpha ("Mask", 2D) = "black" {}        
        _Cutout ("Cutout", Float ) = 1        
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers d3d11 xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Cutout;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 uv0 : TEXCOORD0;
                fixed4 color : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 uv0 : TEXCOORD0;
                fixed4 color : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.uv0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				
				o.color = v.color;
                
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {

                float2 node_509 = i.uv0;
                float4 node_4 = tex2D(_MainTex,TRANSFORM_TEX(node_509.rg, _MainTex));
                float3 emissive = (saturate(( node_4.rgb > 0.5 ? (1.0-(1.0-2.0*(node_4.rgb-0.5))*(1.0-_TintColor.rgb)) : (2.0*node_4.rgb*_TintColor.rgb) ))*node_4.a);
                float3 finalColor = emissive;

                return fixed4(finalColor,(tex2D(_Alpha,TRANSFORM_TEX(node_509.rg, _Alpha)).a*_Cutout))*i.color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}