Shader "Learning/Unlit/FresnelTrans"
{
    Properties
    {   
		// Fresnel Exponent : float entre 0.1 & 20
        Exponent("Exponent", Range(0.1,10)) = 0.5
        // 2 couleurs : une BaseColor (celle du mesh) et une pour l'effet outline du fresnel
        BaseColor("Base Color", Color) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  
            #pragma fragment frag

            #include "UnityCG.cginc"
			
            
            // Variables du bloc Properties
			float Exponent;
			float4 BaseColor;
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldSpace : TEXCOORD0;
                float3 normalWorld : TEXCOORD1;
                // + 
                // Transférer la position & la normale en WORLD SPACE
            };

            v2f vert (vertexInput v)
            {
                v2f o;
               
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                
                // TO DO 
                // position en float4 => w = 1
                // direction en float4 => w = 0
                // matrice: unity_ObjectToWorld
                o.worldSpace = mul(unity_ObjectToWorld, v.vertex);
                o.normalWorld = mul(unity_ObjectToWorld,float4(v.normal,0).xyz);
                
                // la normale en worldspace de la struct v2f doit être normalisée
                o.normalWorld = normalize(o.normalWorld);
                
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // TO DO: Une ligne à coder après chaque commentaire
                // Calculer le vecteur FragmentToCamera puis le normaliser
                float3 FragmentToCamera = _WorldSpaceCameraPos.xyz - i.worldSpace.xyz;
                FragmentToCamera = normalize(FragmentToCamera);
	            
                // Normaliser de nouveau la normale de la struct v2f
                float3 renormalize = normalize(i.normalWorld);
                
                // Calcul du produit scalaire entre le vecteur PixelToCamera (View vector) & la normale
	            float NdotV = dot(FragmentToCamera, renormalize);
                
                // Visualiser le résultat de NdotV  => ligne temporaire, juste pour comprendre l'effet à cette étape
                // "Ajuster" le résultat obtenu
                // Utiliser la fonction pow(valueToRaise, FresnelExponent)
                // lerp entre BaseColor, FresnelColor et le rim calculé ci-dessus.
	            return float4(BaseColor.rgb, pow(1 - NdotV, Exponent));
            }
            ENDHLSL
        }
    }
}
