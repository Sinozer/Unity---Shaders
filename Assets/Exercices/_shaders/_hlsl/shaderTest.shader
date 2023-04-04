// Path to the shader in the project, we mostly use the file name
Shader "Learning/Unlit/shaderTest"
{
    Properties
    {
        _MainColor("Main Color", Color) = (0.8,0.1,0.1,1)
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

            #include "UnityCG.cginc"

            // This is the variable we defined in the Properties section
            float4 _MainColor;

            struct vertexInput
            {
                float4 vertex : POSITION;
            };
            
            struct v2f
            {
                float4 vertex : SV_POSITION;
            };

            // Vertex Shader
            // Will be called 3 times each frame (one for each vertex).
            // i.e : a square on a 60fps rendering will call this function 6*60 = 240 times per second
            v2f vert(vertexInput vertex)
            {
                v2f o;

                // Unity matrix to transform the vertex from local space to world space (MANDATORY)
                o.vertex = mul(UNITY_MATRIX_MVP, vertex.vertex);
                return o;
            }

            // Fragment (or Pixel) Shader
            // Will be called once for each pixel of the screen
            // (i.e : 1920*1080 = 2 073 600 times per second => if we have a 60fps rendering, it will be called 2 073 600*60 = 124 620 800 times per second)
            float4 frag(v2f input) : SV_Target
            {
                // _MainColor is the variable we defined in the Properties section
                return _MainColor;
            }
            ENDHLSL
        }
    }
}