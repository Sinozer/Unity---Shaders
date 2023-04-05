Shader "Learning/Unlit/Learning5"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        TextureMap("Texture Map", 2D) = "white" {}
        NoiseMap("Noise Map", 2D) = "white" {}
        ScrollingSpeed("Scrolling Speed", Vector) = (0,0,0,0)
        DistrubFactor("Distrub Factor", Range(0,0.2)) = 0.1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }

		Pass
        {
            Cull Off
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D TextureMap, NoiseMap;
			float2 ScrollingSpeed;
			float DistrubFactor;
			
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
                float2 offset = ScrollingSpeed * _Time.x;
                float2 distortion = tex2D(NoiseMap,i.uv + offset).rr ;
                distortion *= DistrubFactor;
                return tex2D(TextureMap,i.uv + distortion);
            }
            
            ENDHLSL
        }
    }
}
