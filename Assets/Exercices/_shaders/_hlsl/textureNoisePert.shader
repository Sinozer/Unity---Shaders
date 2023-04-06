Shader "Learning/Unlit/textureNoisePert"
{
    Properties
    {   
        _Albedo1("Albedo", 2D) = "white" {}
        _Noise("Noise", 2D) = "black" {}
        _Distortion("Distortion", Range(0, 0.2)) = 0.1
        _Speed("Speed", Range(0, 1)) = 5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

		Pass
        {
            Cull Off
            
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

			sampler2D _Albedo1, _Noise;
			float _Distortion, _Speed;

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
                float2 offset = _Speed * _Time.y;

                float2 disturbedOffset = tex2D(_Noise, input.uv + offset).xy;
                disturbedOffset *= _Distortion;

                float4 color = tex2D(_Albedo1, input.uv + disturbedOffset);
                return color;
            }
            
            ENDHLSL
        }
    }
}
