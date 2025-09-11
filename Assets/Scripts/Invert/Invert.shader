Shader "PostProcessing/Invert"
{
    Properties
    {
        _BlitTexture ("Texture", 2D) = "white" {}
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
            float4 _BlitTexture_TexelSize;

            TEXTURE2D(_MaskTexture);
            SAMPLER(sampler_MaskTexture);
           
            float _MaskThreshold;
            float _Invert;
            float _Vignette;

            float fadeValue (float2 uv, float2 center, float vignette)
			{
                float dist = distance(uv, center);
				float normalizedDist = dist * 1.414; 
				return pow(saturate(normalizedDist), vignette);
			}

            half4 frag (Varyings input) : SV_Target
            {  
                float mask = SAMPLE_TEXTURE2D(_MaskTexture, sampler_MaskTexture, input.texcoord).r;

                float fade = fadeValue(input.texcoord, float2(0.5, 0.5), _Vignette);

                float4 invertedColor = 1 - SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.texcoord);
                float4 color = SAMPLE_TEXTURE2D(_BlitTexture, sampler_BlitTexture, input.texcoord);
                
                if (mask < _MaskThreshold)
                {
                    return color;  
                }

				return lerp(color, invertedColor, fade * _Invert);
            }
            ENDHLSL
        }
    }
}