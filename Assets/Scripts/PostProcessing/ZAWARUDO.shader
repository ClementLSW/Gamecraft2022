Shader "Custom/ZaWarudo"
{
    //https://www.shadertoy.com/view/llj3Dz
    //https://www.shadertoy.com/view/4lSGRw
    Properties
    {
        _MainTex("tex2D", 2D) = "" {}
        iTime("Time", float) = 0
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


            float2 computeUV(float2 uv, float k, float kcube)
            {

                float2 t = uv - .5;
                float r2 = t.x * t.x + t.y * t.y;
                float f = 0.;

                if (kcube == 0.0)
                {
                    f = 1. + r2 * k;
                }
                else
                {
                    f = 1. + r2 * (k + kcube * sqrt(r2));
                }

                float2 nUv = f * t + .5;
                nUv.y = 1. - nUv.y;

                return nUv;

            }

        //half4 ripple()
        //{
        //    //Sawtooth function to pulse from centre.
        //    float offset = (iTime - floor(iTime)) / iTime;
        //    float CurrentTime = (iTime) * (offset);

        //    //z == wavethickness
        //    vec3 WaveParams = vec3(10.0, 0.8, 0.01);

        //    float ratio = iResolution.y / iResolution.x;

        //    //Use this if you want to place the centre with the mouse instead
        //    //vec2 WaveCentre = vec2( iMouse.xy / iResolution.xy );

        //    vec2 WaveCentre = vec2(0.5, 0.5);
        //    WaveCentre.y *= ratio;

        //    vec2 texCoord = fragCoord.xy / iResolution.xy;
        //    texCoord.y *= ratio;
        //    float Dist = distance(texCoord, WaveCentre);


        //    vec4 Color = tex2D(iChannel0, texCoord);

        //    //Only distort the pixels within the parameter distance from the centre
        //    if ((Dist <= ((CurrentTime)+(WaveParams.z))) &&
        //        (Dist >= ((CurrentTime)-(WaveParams.z))))
        //    {
        //        //The pixel offset distance based on the input parameters
        //        float Diff = (Dist - CurrentTime);
        //        float ScaleDiff = (1.0 - pow(abs(Diff * WaveParams.x), WaveParams.y));
        //        float DiffTime = (Diff * ScaleDiff);

        //        //The direction of the distortion
        //        vec2 DiffTexCoord = normalize(texCoord - WaveCentre);

        //        //Perform the distortion and reduce the effect over time
        //        texCoord += ((DiffTexCoord * DiffTime) / (CurrentTime * Dist * 40.0));
        //        Color = tex2D(iChannel0, texCoord);

        //        //Blow out the color and reduce the effect over time
        //        Color += (Color * ScaleDiff) / (CurrentTime * Dist * 40.0);
        //    }

        //    fragColor = Color;

        //}

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

                fixed4 frag(v2f_img i) : SV_Target
                {

                    float2 uv = i.uv;
                    uv.y = 1- uv.y;
                    float k = 2.0 * sin(iTime * .9);
                    float kcube = .5 * sin(iTime);

                    float offset = .1 * sin(iTime * .5);

                    float red = tex2D(_MainTex, computeUV(uv, k + offset, kcube)).r;
                    float green = tex2D(_MainTex, computeUV(uv, k, kcube)).g;
                    float blue = tex2D(_MainTex, computeUV(uv, k - offset, kcube)).b;

                    //fixed4 col = tex2D(_MainTex, uv);
                    //return col;
                    //return fixed4(1, 1, 1, 1);
                    return fixed4(red, green, blue, 1.0);
                }


            ENDCG
            }
        }
}
