Shader "Learning/Unlit/Blend"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Color2 ("Color2", Color) = (1,1,1,1)
        _Blender("Blender", Range(0,1)) = 0.5
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

            float4 _Color, _Color2;
            float _Blender;

            #include "UnityCG.cginc"

            struct vertexInput
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            v2f vert(vertexInput v)
            {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }

            float4 frag(v2f input) : SV_Target
            {
                float4 blendedCol = lerp(_Color, _Color2, _Blender);
                return blendedCol;
            }
            ENDHLSL
        }
    }
}