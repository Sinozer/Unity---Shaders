Shader"Learning/Unlit/verticesAnimation"
{
    Properties
    {   
        _Amplitude("amplitude",  float) = 1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag
float _Amplitude;
            #include "UnityCG.cginc"
			
			struct vertexInput
            {
                float4 vertex : POSITION;						
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;    
            };

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
    o.vertex.y += sin(_Amplitude * _Time.y);
    o.vertex.x += cos(_Amplitude * _Time.y);
    return o;
            }

            float4 frag(v2f input) : SV_Target
            {
                
                return float4(0.5,0.4,0.1,0); 
            }
            
            ENDHLSL
        }
    }
}
