Shader "Learning/Unlit/TO RENAME"
{
    Properties
    {   
        _Color1 ("Color1", Color) = (0.8,0.1,0.1,1)
        _Color2 ("Color2", Color) = (0.1,0.8,0.1,1)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

			float4 _Color1, _Color2;

            #include "UnityCG.cginc"
			
			struct vertexInput
            {
                float4 vertex : POSITION;						
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 vertexWorld : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.vertexWorld = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float4 frag(v2f input) : SV_Target
            {
                float4 color = input.vertexWorld.x > 0 ? _Color1 : _Color2;
                return color; 
            }
            
            ENDHLSL
        }
    }
}
