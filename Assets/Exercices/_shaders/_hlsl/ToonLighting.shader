Shader"Learning/Lit/ToonLighting"
{
    Properties
    {
_Texture("texture", 2D) = "white" {}
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
float3 _WorldSpaceLightDir;
sampler2D _Texture;
			
			struct vertexInput
			{
				float4 vertex : POSITION;	
    float3 normal : NORMAL;
};

			struct v2f
			{
				float4 vertex : SV_POSITION;
    float3 worldNormal : TEXCOORD0;
				// normal en world space
			};

			v2f vert(vertexInput v)
			{
				v2f o;

				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				// To do
				o.worldNormal = normalize(mul(unity_ObjectToWorld, float4(v.normal, 0).xyz));
				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				// To do => NdotL 
				// N & L dans le même espace et normalisés
    i.worldNormal = normalize(i.worldNormal);
    float3 LightDir = normalize(_WorldSpaceLightDir.xyz);
   
				// L => direction reçue depuis le script C#. Forward de la DirLight
				// A inverser car côté shader, le vecteur de la lumière part depuis le fragment
    float3 L = - LightDir;
    float NdotL = dot(i.worldNormal, L);
				// dot retourne des valeurs entre -1 et 1, => clamp à utiliser
     NdotL = clamp(NdotL, 0.1, 0.95);
    
    return tex2D(_Texture, NdotL);
}
			
            ENDHLSL
        }
    }
}
