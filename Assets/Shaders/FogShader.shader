/*
References:
https://danielilett.com/2019-05-04-tut1-2-smo-silhouette/

Using Depth Texture, I apply it directly onto the incoming color, on a per pixel basis.

*/

Shader"Custom/FogShader"{

    Properties{
        _FogColor ("Fog Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

            sampler2D _MainTex, _CameraDepthTexture;
            float4 _FogColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex Shader
            v2f vert(appdata v)
            {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                //o.depth = o.position.w;
                o.uv = v.uv;
                return o;
            };

            // Fragment Shader (After Rasterizing). Output color per pixel
            float4 frag(v2f i) : SV_TARGET
            {
                float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));
                depth = Linear01Depth(depth);
                depth = pow(Linear01Depth(depth), 0.75);

                // Calculate the Distance from point in World Space to Camera
                //float dist = distance(_WorldSpaceCameraPos, i.position);
    
                // Unity Macro for Fog Calculation
                // UNITY_CALC_FOG_FACTOR_RAW(dist);
    
                fixed4 col = tex2D(_MainTex, i.uv);
                //dist = length(i.position.x - _WorldSpaceCameraPos);
    
                //float linearDepth = 1.0 - saturate((1250 - dists) / (1250 - 330));
    
                //col = lerp(_FogColor, col, linearDepth);
    
                //float dist = length(_WorldSpaceCameraPos - i.depth);

                //col = lerp(_FogColor, col, saturate((dist) / (_ProjectionParams.z - _ProjectionParams.y)));
                //col += lerp(_FogColor, col, ((_ProjectionParams.z + dist) / (_ProjectionParams.z - _ProjectionParams.y)));
                //col = lerp(_FogColor, col, saturate(dist / _ProjectionParams.z));
                //col = lerp(_FogColor, col, saturate(zDepth));
                col -= lerp(col, _FogColor, depth);
    
                return col;
}

            ENDCG

        }
    }
}
