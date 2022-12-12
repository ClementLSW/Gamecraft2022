Shader "Instanced/XpShader" {
	Properties {
		_Color ("Colour", Color) = (1,1,1,1)
		_Fire("Fire", Color) = (0, 0, 0, 1)
        _Wind("Wind", Color) = (0, 0, 0, 1)
        _Earth("Earth", Color) = (0, 0, 0, 1)
		_Water("Water", Color) = (0, 0, 0, 1)
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

			StructuredBuffer<float3> positionBuffer;
			StructuredBuffer<uint> colorTypeBuffer;


			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			float3 _Fire;
			float3 _Wind;
			float3 _Earth;
			float3 _Water;

			v2f vert (appdata_full v, uint instanceID : SV_InstanceID)
			{
				float3 data = positionBuffer[instanceID];
				uint colorType = colorTypeBuffer[instanceID];

				float3 localPosition = v.vertex.xyz * data.z;
				float3 worldPosition = float3(data.xy, 0) + localPosition;

				v2f o;
				o.pos = mul(UNITY_MATRIX_VP, float4(worldPosition, 1.0f));
				
				switch(colorType){
					case 0:
					o.color = _Fire;
					break;
					case 1:
					o.color = _Wind;
					break;
					case 2:
					o.color = _Earth;
					break;
					case 3:
					o.color = _Water;
					break;
				}

				
				o.uv = v.texcoord;
				return o;
			}
            sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{

                fixed4 col = tex2D(_MainTex, i.uv);
				//col.rgb = _Color.rgb;
				col.rgb = i.color.rgb;
				return col;
			}

			ENDCG
		}
	}
}