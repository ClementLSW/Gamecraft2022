Shader "Instanced/DustShader" {
	Properties {
		_Color ("Colour", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
	}
	SubShader {

		Pass {

			Tags {"Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent"}
			ZWrite Off
			Lighting Off
			Fog { Mode Off }

			Blend SrcAlpha OneMinusSrcAlpha 
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			#pragma target 4.5

			#include "UnityCG.cginc"
			#include "UnityLightingCommon.cginc"
			#include "AutoLight.cginc"

			float4 _Color;

		#if SHADER_TARGET >= 45
			StructuredBuffer<float3> positionBuffer;
		#endif

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert (appdata_full v, uint instanceID : SV_InstanceID)
			{
			#if SHADER_TARGET >= 45
				float3 data = positionBuffer[instanceID];
			#else
				float4 data = 0;
			#endif

				float3 localPosition = v.vertex.xyz * data.z;
				float3 worldPosition = float3(data.xy, 0) + localPosition;

				v2f o;
				o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
				o.uv = v.texcoord;
				return o;
			}
            sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				//float2 offsetFromCentre = 0.5 - i.uv;
				//float r = length(offsetFromCentre) * 2;
				//float alpha = smoothstep(1,0.5,r);
				//float alpha = max(0, 1 - (length(offsetFromCentre) * 2));
				//alpha = 1;
                fixed4 col = tex2D(_MainTex, i.uv);
				//fixed4 col = float4(_Color.rgb, alpha * _Color.a);
				col.rgb = _Color.rgb;
				return col;
			}

			ENDCG
		}
	}
}