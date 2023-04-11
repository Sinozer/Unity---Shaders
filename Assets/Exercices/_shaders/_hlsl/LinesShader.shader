Shader "Learning/Unlit/LinesShader"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        LinesTexture("Texture", 2D) = "white" {} 
        Speed("Speed", float) = 1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D LinesTexture;
			float Speed;
			
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

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f input) : SV_Target
            {
                float d = distance(float2(0.5,0.5), input.uv);
                return tex2D(LinesTexture,d + Speed * _Time.x);
            }
            
            ENDHLSL
        }
    }
}
