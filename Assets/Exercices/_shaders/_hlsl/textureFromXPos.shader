Shader "Learning/Unlit/TO RENAME"
{
    Properties
    {   
        _Albedo1("Albedo", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

			sampler2D _Albedo1;

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
                const float2 uv = input.vertexWorld.xy;
                float4 text = tex2D(_Albedo1, uv);
                return text; 
            }
            
            ENDHLSL
        }
    }
}
