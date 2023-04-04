Shader "Learning/Unlit/vertexColors"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			// Data for each vertex
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 vertexColor : COLOR;
            };

			// Vertex to fragment data
			// Each of these variables will be interpolated by the resterizer and passed to the fragment shader
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 vertexColor : COLOR;
            };
			
            v2f vert (vertexInput vertex)
            {
                v2f output;
	            output.vertex = mul(UNITY_MATRIX_MVP, vertex.vertex);
                output.vertexColor = vertex.vertexColor;
                return output;
            }

			// RASTERIZER
			

            float4 frag(v2f input) : SV_Target
            {
                return input.vertexColor; 
            }
            
            ENDHLSL
        }
    }
}
