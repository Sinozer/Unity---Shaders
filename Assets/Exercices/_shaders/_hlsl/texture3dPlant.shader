Shader "Learning/Unlit/texture3dPlant"
{
    Properties
    {   
        _Albedo1("Albedo", 2D) = "white" {}
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

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
                float4 text = tex2D(_Albedo1, input.uv);
                if (text.a < 0.05) discard;
                return text; 
            }
            
            ENDHLSL
        }
    }
}
