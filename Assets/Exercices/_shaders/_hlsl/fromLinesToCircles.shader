Shader"Learning/Unlit/FromLinesToCircles"
{
    Properties
    {  
        _Texture("texture", 2D) = "white" {}
_Speed("speed", float) = 0
_Origin("CircleOrigin", Vector) = (0.5,0.5,0,0)
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
float _Speed;
float2 _Origin;
            #include "UnityCG.cginc"
			
			struct vertexInput
            {
                float4 vertex : POSITION;	
    float2 uv : TEXCOORD;
};
			
            struct v2f
            {
                float4 vertex : SV_POSITION;   
    float2 uv : TEXCOORD;
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
    float d = distance(_Origin, input.uv);
    return tex2D(_Texture, d + _Speed*_Time.y);
}
            ENDHLSL
        }
    }
}
