/*
Reference:
https://forum.unity.com/threads/trying-to-create-stencil-lights-but-i-am-hopelessly-lost.575479/
https://www.youtube.com/watch?v=-NB2TR8IjE8
https://www.ronja-tutorials.com/post/022-stencil-buffers/
https://docs.unity3d.com/Manual/SL-Stencil.html
https://stackoverflow.com/questions/6787733/how-do-multiple-pass-shaders-work-in-hlsl

Multi-Pass Shader that writes to the Stencil Buffer first, then REPLACES those pixels in the second PASS.
Pass 1: Write to Stencil Buffer
Pass 2: Compare Stencil Buffer for THAT value, and replace those pixels (in our case, we are just tinting them).

*/

Shader "Custom/StencilFlashlightShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _StencilRef("Stencil Reference Value", Float) = 20
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent-10"}
        LOD 100

        // First Pass to write to Stencil Buffer.    
        Pass
        {
            Ztest Greater
            Zwrite off
            Cull Front
            Colormask 0
            Stencil
            {
                ref [_StencilRef]
                comp always
                pass replace // We're telling it to Replace on successful Compare, which is Always..
            }
        }
        // Second Pass to Read, Replace and empty Stencil Buffer
        Pass
        {
            Blend SrcAlpha One

            Stencil
            {
                ref [_StencilRef]
                comp equal // Setting the Comparison to equal. 
                pass zero // We want to clear the buffer again for the next frame. So set buffer to zero in all cases.
                fail zero
                zfail zero
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * _Color;
            }
            ENDCG
        }
    }
}
