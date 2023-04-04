Shader "Learning/Unlit/Demo_Shader2"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        Albedo1("Day Map", 2D) = "white" {}
        Albedo2("Night Map", 2D) = "Black" {}
        BlendAlbedo("Noise",2D) = "white" {}
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

			sampler2D Albedo1, Albedo2,BlendAlbedo;
			
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

            float4 frag(v2f i) : SV_Target
            {
                return lerp(tex2D(Albedo1,i.uv),tex2D(Albedo2,i.uv),tex2D(BlendAlbedo,i.uv).r); 
            }
            
            ENDHLSL
        }
    }
}
