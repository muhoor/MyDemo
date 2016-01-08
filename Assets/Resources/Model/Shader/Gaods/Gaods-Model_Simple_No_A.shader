Shader "Gaods/Model_Simple_No_A" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_BlendTex ("Alpha Blended (RGBA) ", 2D) = "white" {}
}
SubShader {
	Tags { "Queue" = "Geometry+10" "IgnoreProjector"="True" "RenderType"="Opaque" }
	LOD 100
	//Lighting Off

	Pass {
	//Cull Off
	SetTexture [_MainTex] {
	ConstantColor [_NotVisibleColor] combine texture
	}
	}
}
}