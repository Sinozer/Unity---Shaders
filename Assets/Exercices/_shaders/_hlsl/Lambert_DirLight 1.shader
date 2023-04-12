Shader "Learning/Lit/Lambert_DirLight1"
{
    Properties
    {
    	TextureAlb("Texture", 2D) = "white" {}
    }

    SubShader
    {
		Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			
			#include "UnityCG.cginc"

			// Récupérez les data depuis le script LightData, attaché sur votre Directional Light
            sampler2D TextureAlb;
            float4 _LightColor;
            float4 _WorldSpaceLightDir;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAl;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normalWS : TEXCOORD0;
				// normal en world space
			};

			v2f vert(vertexInput v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normalWS = normalize(mul(unity_ObjectToWorld, float4(v.normal,0).xyz));
				// To do
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				// To do => NdotL
				// N & L dans le même espace et normalisés
				float3 N = normalize(i.normalWS);
				float3 L = normalize(_WorldSpaceLightDir);
				// L => direction reçue depuis le script C#. Forward de la DirLight
				// A inverser car côté shader, le vecteur de la lumière part depuis le fragment
				// dot retourne des valeurs entre -1 et 1, => clamp à utiliser
				return tex2D(TextureAlb,saturate(dot(N, 1 - L)));
			}
			
            ENDHLSL
        }
    }
}
