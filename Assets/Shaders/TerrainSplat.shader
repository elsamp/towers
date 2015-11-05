#warning Upgrade NOTE: unity_Scale shader variable was removed; replaced 'unity_Scale' with 'float4(1,1,1,1)'
// Upgrade NOTE: commented out 'float3 _WorldSpaceCameraPos', a built-in variable
// Upgrade NOTE: commented out 'float4 unity_LightmapST', a built-in variable
// Upgrade NOTE: commented out 'float4x4 _Object2World', a built-in variable
// Upgrade NOTE: commented out 'float4x4 _World2Object', a built-in variable
// Upgrade NOTE: commented out 'sampler2D unity_Lightmap', a built-in variable

Shader "Custom/TerrainSplat" {
	Properties {
		_Map ("Reflection", 2D) = "white" {}
		_RedSplat ("Red Splat", 2D) = "white" {}
		_GreenSplat ("Green Splat", 2D) = "white" {}
		_BlueSplat ("Blue Splat", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Pass{

		Cull Back
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		
		uniform sampler2D _Map;
		uniform sampler2D _RedSplat;
		uniform sampler2D _GreenSplat;
		uniform sampler2D _BlueSplat;
		
		uniform float4 _RedSplat_ST;
		uniform float4 _GreenSplat_ST;
		uniform float4 _BlueSplat_ST;

		uniform float4 _LightColor0;
		
		struct vertexInput {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
			float4 texcoord1 : TEXCOORD1;
			float3 normal : NORMAL;
		};

		struct vertexOutput {
			float4 pos : SV_POSITION;
			fixed4 col : COLOR;
			float4 tex : TEXCOORD1;
			float2 uv : TEXCOORD2;
		};
		
		
		vertexOutput vert(vertexInput input){

			vertexOutput output;
			
			float3 normalDirection = normalize(float3(mul(float4(input.normal, 0.0), _World2Object).xyz));
			float3 lightDirection = normalize(float3(_WorldSpaceLightPos0.xyz));
			float3 defuseReflection = float3(_LightColor0.xyz)* max(0.0, dot(normalDirection, lightDirection));
			output.col = float4(defuseReflection, 1.0) + float4(UNITY_LIGHTMODEL_AMBIENT);

			output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			output.tex = input.texcoord;
			output.uv = ((input.texcoord1.xy * unity_LightmapST.xy) + unity_LightmapST.zw);
			
			return output;
		
		}
		
		
		float4 frag(vertexOutput input) : Color{
		
			float4 blendMap = tex2D(_Map, input.tex.xy);
			
			
			float4 colouredOutput = (tex2D(_RedSplat, input.tex.xy * _RedSplat_ST) * blendMap.r) +
			 						(tex2D(_GreenSplat, input.tex.xy * _GreenSplat_ST) * blendMap.g) +
			 						(tex2D(_BlueSplat, input.tex.xy * _BlueSplat_ST) * blendMap.b);
			
			return colouredOutput * input.col;
		}
		
		
		ENDCG
		}
	} 

}
