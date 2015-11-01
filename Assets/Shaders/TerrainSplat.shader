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
		
		
		struct vertexInput {
			float4 vertex : POSITION;
			float4 texcoord : TEXCOORD0;
		};
		
		
		struct vertexOutput {
			float4 pos : SV_POSITION;
			float4 tex : TEXCOORD0;
		};
		
		
		vertexOutput vert(vertexInput input){
		
			vertexOutput output;

			output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
			output.tex = input.texcoord;
			
			return output;
		
		}
		
		
		float4 frag(vertexOutput input) : Color{
		
			float4 blendMap = tex2D(_Map, input.tex.xy);
			
			
			float4 colouredOutput = (tex2D(_RedSplat, input.tex.xy * _RedSplat_ST) * blendMap.r) +
			 						(tex2D(_GreenSplat, input.tex.xy * _GreenSplat_ST) * blendMap.g) +
			 						(tex2D(_BlueSplat, input.tex.xy * _BlueSplat_ST) * blendMap.b);
			
			return colouredOutput;
		}
		
		
		ENDCG
		}
	} 

}
