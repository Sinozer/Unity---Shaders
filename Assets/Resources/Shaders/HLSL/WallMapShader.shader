Shader "Learning/Unlit/WallMapShader"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        Noise("Noise", 2D) = "black" {}
        Distortion("Distortion", Range(0, 0.2)) = 0.1
        Speed("Speed", Range(0, 0.2)) = 0.1
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry"}

		Pass
        {
            HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"

			sampler2D Noise;
			float Distortion, Speed;
			
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
                float2 offset = Speed * _Time.y;

                float2 disturbedOffset = tex2D(Noise, input.uv + offset).xy;
                disturbedOffset *= Distortion;

                float4 color = tex2D(Noise, input.uv + disturbedOffset);
                float4 colorSend = {0,0,color.b,1};

                
                return colorSend;
            }
            ENDHLSL
        }
    }
}
