Shader "Learning/Unlit/LearningShader"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        //MainColor("HDR Color", Color) = (1,1,1,1)
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

			//Vertex shader
			struct vertexInput
            {
                float4 vertex : POSITION;
			    float4 vertexColor : COLOR;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 vertexColor : COLOR;
            };

			//Vertex to Fragment
            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.vertexColor = v.vertexColor;
                return o;
            }

			// Fragment shader
			// S'exécute pour chaque pixel concerné
			// 1000 px à 60 FPS ça donne 60 000/s
            float4 frag(v2f i) : SV_Target
            {
                return i.vertexColor; 
            }
            
            ENDHLSL
        }
    }
}
