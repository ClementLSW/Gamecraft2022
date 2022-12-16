Shader "Custom/Ripple"
{
    //https://www.shadertoy.com/view/llj3Dz
    //https://www.shadertoy.com/view/4lSGRw
    Properties
    {
        _MainTex("tex2D", 2D) = "" {}
        iTime("Time", float) = 0
            WaveCentre("Wave Center", Vector) = (0.5, 0.5, 0,0)
            WaveSize("Wave Size", float) = 0.1
    }

        SubShader
        {
            Pass
            {
                ZTest Always Cull Off ZWrite Off
                //Post processing shaders cant be transparent otherwise it doesnt refresh screen ans tears
        //	Blend SrcAlpha OneMinusSrcAlpha
                CGPROGRAM
            //This is a default for vertex buffer I guess
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

        //the data thats passed from the vertex to the fragment shader and interpolated by the rasterizer
        struct v2f
        {
            float4 position : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        float iTime;

        v2f vert(appdata v)
        {
            v2f o;
            //convert the vertex positions from object space to clip space so they can be rendered correctly
            o.position = UnityObjectToClipPos(v.vertex);
            //apply the texture transforms to the UV coordinates and pass them to the v2f struct
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }
        float2 WaveCentre;
        float WaveSize;

        fixed4 frag(v2f_img i) : SV_Target
        {

            //Sawtooth function to pulse from centre.
            float offset = (iTime - floor(iTime)) / iTime;
            float CurrentTime = (iTime) * (offset);
            float ratio = _ScreenParams.y / _ScreenParams.x;

            //z == wavethickness
            float3 WaveParams = float3(10.0, 0.8, 0.01);


            //Use this if you want to place the centre with the mouse instead
            //float2 WaveCentre = float2( iMouse.xy / iResolution.xy );

            //WaveCentre.y *= ratio;

            float2 uv = i.uv;
            //uv.y *= ratio;
            float Dist = distance(uv, WaveCentre);


            fixed4 Color = tex2D(_MainTex, uv);

            //Only distort the pixels within the parameter distance from the centre
            if ((Dist <= ((CurrentTime)+(WaveSize))) &&
                (Dist >= ((CurrentTime)-(WaveSize))))
            {
                //The pixel offset distance based on the input parameters
                float Diff = (Dist - CurrentTime);
                float ScaleDiff = (1.0 - pow(abs(Diff * WaveParams.x), WaveParams.y));
                float DiffTime = (Diff * ScaleDiff);

                //The direction of the distortion
                float2 DiffTexCoord = normalize(uv - WaveCentre);

                //Perform the distortion and reduce the effect over time
                uv += ((DiffTexCoord * DiffTime) / (CurrentTime * Dist * 40.0));
                Color = tex2D(_MainTex, uv);

                //Blow out the color and reduce the effect over time
                Color += (Color * ScaleDiff) / (CurrentTime * Dist * 40.0);
            }

            return Color;
        }


    ENDCG
    }
        }
}
