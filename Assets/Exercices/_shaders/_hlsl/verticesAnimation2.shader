Shader"Learning/Unlit/verticesAnimation2"
{
    Properties
    {   
        _Texture("texture", 2D) = "white" {}
_Speed("speed", float) = 1
_NormalScaling("normalScaling", float) = 1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

sampler2D _Texture;
float _Speed, _NormalScaling;

            #include "UnityCG.cginc"
			
			struct vertexInput
            {
                float4 vertex : POSITION;	
    float2 uv : TEXCOORD0;
    float3 normal : NORMAL;
};
			
            struct v2f
            {
                float4 vertex : SV_POSITION;    
    float2 uv : TEXCOORD0;
};

            v2f vert (vertexInput v)
            {
                v2f o;
                v.uv.xy += float2(_Time.x * _Speed, 0);
                v.vertex.xyz += v.normal * _NormalScaling * tex2Dlod(_Texture, float4(v.uv, 0, 0));   
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f input) : SV_Target
            {
    return tex2D(_Texture, input.uv);
}
            
            ENDHLSL
        }
    }
}
