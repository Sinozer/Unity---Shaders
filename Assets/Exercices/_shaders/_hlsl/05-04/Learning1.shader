Shader "Learning/Unlit/Learning1"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        Color1("Color 1", Color) = (1, 1, 1, 1)
        Color2("Color 2", Color) = (1, 1, 1, 1)
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

			float4 Color1, Color2;
			
			struct vertexInput
            {
                float4 vertex : POSITION;					
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 vertexWorld : TEXCOORD0;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.vertexWorld = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return i.vertexWorld.x > 0 ? Color1 : Color2;   
            }
            
            ENDHLSL
        }
    }
}
