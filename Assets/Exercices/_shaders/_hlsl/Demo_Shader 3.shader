Shader "Learning/Unlit/Demo_Shader3"
{
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue
        Albedo1("Sand Map", 2D) = "white" {}
        Albedo2("Plant Map", 2D) = "Black" {}
        DistanceRange("DistanceRange", float) = 0
        //WorldSpaceCameraPos("Camera Position")
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
			float DistanceRange;
			
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

            float4 frag(v2f i) : SV_Target
            {
                float camDistance = distance(_WorldSpaceCameraPos,i.vertexWorld.xyz);
                float clampDist = clamp(camDistance/5,0,DistanceRange);
                
                return lerp(tex2D(Albedo1,i.uv),tex2D(Albedo2,i.uv),clampDist); 
            }
            
            ENDHLSL
        }
    }
}
