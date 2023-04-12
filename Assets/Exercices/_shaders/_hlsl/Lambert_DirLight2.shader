Shader "Learning/Lit/Lambert_DirLight"
{
    Properties
    {
    	_Albedo("Albedo", 2D) = "white" {}
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
            float4 _LightColor;
            float4 _WorldSpaceLightDir;
            sampler2D _Albedo;
			
			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normalWS : TEXTCOORD0;
			};

			v2f vert(vertexInput v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.normalWS = normalize(mul(unity_ObjectToWorld, v.normal));
				
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				// To do => NdotL
				// N & L dans le même espace et normalisés
				_WorldSpaceLightDir.xyz = normalize(_WorldSpaceLightDir.xyz);
				
				// L => direction reçue depuis le script C#. Forward de la DirLight
				// A inverser car côté shader, le vecteur de la lumière part depuis le fragment
				float3 L = -_WorldSpaceLightDir.xyz;
				
				// dot retourne des valeurs entre -1 et 1, => clamp à utiliser
				const float NdotL = dot(i.normalWS, L);
				const float NdotL_clamped = clamp(NdotL, 0.2, 0.95);
				float4 text = tex2D(_Albedo, NdotL_clamped);
				
				return text;
			}
			
            ENDHLSL
        }
    }
}
