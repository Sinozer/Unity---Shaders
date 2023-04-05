Shader "Learning/Unlit/Learning3"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        TextureMap("Texture Map", 2D) = "white" {}
        ScrollingSpeed("Scrolling Speed", Vector) = (0,0,0,0)
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D TextureMap;
			float2 ScrollingSpeed;
			
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
                o.uv = v.uv + float2(ScrollingSpeed.x * _Time.x,ScrollingSpeed.y * _Time.x);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                return tex2D(TextureMap,i.uv); 
            }
            
            ENDHLSL
        }
    }
}
