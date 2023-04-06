Shader "Learning/Unlit/scroll"
{
    Properties
    {   
        _Albedo1("Albedo", 2D) = "white" {}
        _ScrollingSpeed("Scrolling Speed", Vector) = (0,0,0,0)
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
			vector _ScrollingSpeed;

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
                const float time = _Time.x;
                const float2 pos = input.uv + time * _ScrollingSpeed;
                float4 text = tex2D(_Albedo1, pos);
                return text; 
            }
            
            ENDHLSL
        }
    }
}
