Shader "Hiroshiryu/NBWater" {
	Properties {
		_Diffuse ("Diffuse (RGB)", 2D) = "white" {}
		_BumpZ ("BummpZ (RGB)", 2D) = "white" {}
		_BumpZ1 ("BummpZ1 (RGB)", 2D) = "white" {}
		_WaterColor ("WebColor (RGBA)", Color) = (1,1,1,1)
		_EmissionPower ("Emission Power", Range(0.075,5)) =0.075
		_AlphaPower ("Alpha Power", Range(0.075,5)) =0.075
		_WaterSpeed ("Water Speed", Range(0,1)) = 1
		_AlphaPlus ("Alpha Plus", Range(0,0.2)) = 0
	}
	SubShader {
		Tags { "Queue"="Transparent+100" "RenderType"="Opaque" }
		LOD 200
		cull off
		CGPROGRAM
		#pragma surface surf Lambert alpha exclude_path:prepass nolightmap   halfasview

		sampler2D _Diffuse;
		sampler2D _BumpZ;
		sampler2D _BumpZ1;
		fixed4 _WaterColor;
		fixed _EmissionPower;
		fixed _WaterSpeed;
		fixed _AlphaPower;
		fixed _AlphaPlus;
		struct Input {
			fixed2 uv_Diffuse;
			fixed2 uv_BumpZ;
			fixed2 uv_BumpZ1;
		};
		
		void surf (Input IN, inout SurfaceOutput o) {
		
			//float time = (i.color.a * 60 + _Time.x);
			
			fixed2 uvBump = IN.uv_BumpZ;
			//fixed t = fmod( _Time, 1000);
			//fixed t = _Time.w/100*100;
//			fixed t = frac(_Time.x/10000*100)*100;
			fixed t = fmod( _Time.y , 60 )/36;
			//uvBump += sin(_Time.x  );
			uvBump += t*_WaterSpeed*10;//sin(_Time.y ) * sin(_Time.y )*_WaterSpeed;
			fixed2 uvBump1 = IN.uv_BumpZ1;
			//uvBump1 += t * _WaterSpeed * 0.05;
			uvBump1.xy *= -1;
			fixed4 bump = tex2D (_BumpZ, uvBump);
			fixed4 bump1 = tex2D (_BumpZ1, uvBump1);
			fixed2 offsetuv = bump.gb * bump1.gb;
			//fixed2 offsetuv = bump.gb;
			fixed4 c = tex2D (_Diffuse, IN.uv_Diffuse + offsetuv - floor(offsetuv) );
			o.Emission =  _EmissionPower *_WaterColor;
			o.Alpha = c.r *_AlphaPower + _AlphaPlus;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
