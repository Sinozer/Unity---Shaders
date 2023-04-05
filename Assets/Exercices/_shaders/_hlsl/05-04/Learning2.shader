Shader "Learning/Unlit/Learning2"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        ColorMap("Gradient Map", 2D) = "white" {}
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

			sampler2D ColorMap;
			
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
                o.vertexWorld = mul(unity_ObjectToWorld,v.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return tex2D(ColorMap,i.vertexWorld); 
            }
            
            ENDHLSL
        }
    }
}
