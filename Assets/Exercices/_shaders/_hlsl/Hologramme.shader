Shader"Learning/Unlit/hologramme"
{
    Properties
    { 
        _Color("color", Color) = (0.6,0.2,0.2)
        _Texture("texture", 2D) = "white" {}
        _Speed("speed", float) = 0.5
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
float4 _Color;
float _Speed;

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
    float4 vertexWorld : TEXCOORD1;
};

            v2f vert (vertexInput v)
            {
                v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
    o.vertexWorld = mul(unity_ObjectToWorld, v.vertex);
    o.uv = v.uv;
    
                return o;
            }

            float4 frag(v2f input) : SV_Target
            {
    float2 uv = (input.uv.x, (input.vertexWorld.y + _Time.x * _Speed)%1);
    float4 tex = tex2D(_Texture, uv);
    if (tex.r <= 0.1)
        discard;
    return tex * _Color;
}
            
            ENDHLSL
        }
    }
}
