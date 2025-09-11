Shader "SmoothPostProcessing/RGBSplit"
{
    Properties
    {
        _BlitTexture ("Texture", 2D) = "white" {}
        _Offset ("Offset", Vector) = (0, 0, 0, 0)
        _Color1 ("Color1", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"}
        LOD 100
        ZWrite Off Cull Off
        Pass
        {
            Name "ColorBlitPass"

            HLSLPROGRAM
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            #pragma vertex Vert
            #pragma fragment frag

            uniform float4 _BlitScaleBias;

            struct Attributes
            {
                uint vertexID : SV_VertexID;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 texcoord   : TEXCOORD0;
            };

            Varyings Vert(Attributes input)
            {
                Varyings output;

                float4 pos = GetFullScreenTriangleVertexPosition(input.vertexID);
                float2 uv  = GetFullScreenTriangleTexCoord(input.vertexID);

                output.positionCS = pos;
                output.texcoord   = uv * _BlitScaleBias.xy + _BlitScaleBias.zw;
                return output;
            }

            TEXTURE2D(_BlitTexture);
            SAMPLER(sampler_BlitTexture);

            float2 _OffsetR;
            float2 _OffsetG;
            float2 _OffsetB;
            float _Vignette;

            float fadeValue (float2 uv, float2 center, float vignette)
			{
                float dist = distance(uv, center);
				float normalizedDist = dist * 1.414; 
				return pow(saturate(normalizedDist), vignette);
			}

            half4 frag (Varyings input) : SV_Target
            {
                float fade = fadeValue(input.texcoord, float2(0.5, 0.5), _Vignette);

                float coLR = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.texcoord + ((_OffsetR / 50) * fade)).r;
                float coLG = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.texcoord + ((_OffsetG / 50) * fade)).g;
                float coLB = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.texcoord + ((_OffsetB / 50) * fade)).b;

                return float4(coLR, coLG, coLB, 1);
            }
            ENDHLSL
        }
    }
}