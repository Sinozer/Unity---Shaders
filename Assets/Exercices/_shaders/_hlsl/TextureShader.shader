Shader "Learning/Unlit/Texture"
{
    Properties
    {
        _Albedo("Albedo", 2D) = "white" {}
        _Albedo2("Albedo2", 2D) = "white" {}
        _Blend("Blend", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _Albedo;
            sampler2D _Albedo2;
            float _Blend;

            #include "UnityCG.cginc"

            struct vertexInput
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(const vertexInput v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(const v2f input) : SV_Target
            {
                const float4 tex1 = tex2D(_Albedo, input.uv);
                const float4 tex2 = tex2D(_Albedo2, input.uv);
                return lerp(tex1, tex2, _Blend);
            }
            ENDHLSL
        }
    }
}