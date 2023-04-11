Shader"Learning/Unlit/ShieldShader"
{
    Properties
    {   
        _Texture("texture", 2D) = "white" {}
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
    return tex2D(_Texture, input.uv);
}
            
            ENDHLSL
        }
    }
}
