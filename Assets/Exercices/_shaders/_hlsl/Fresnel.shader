Shader"Learning/Unlit/Fresnel"
{
    Properties
    {   
		_Exponent ("FresnelExponent" , Range(0.1, 20)) = 0.5
        _BaseColor ("Color1", Color) = (0,0,0,0)
        _FresnelColor ("Color2", Color) = (0,0,0,0)
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
            float _Exponent;
            float4 _BaseColor, _FresnelColor;       
            
            struct vertexInput
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
			
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
            };

            v2f vert (vertexInput v)
            {
                v2f o;
               
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
              
                // TO DO 
                // position en float4 => w = 1
                // direction en float4 => w = 0
    
                //matrice unity_ObjectToWorld
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                
                // la normale en worldspace de la struct v2f doit être normalisée
                o.worldNormal = normalize(mul(unity_ObjectToWorld, float4(v.normal, 0)));
    
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // TO DO: Une ligne à coder après chaque commentaire
                // Calculer le vecteur FragmentToCamera puis le normaliser AB = B - A
    float3 FragmentToCamera = normalize(_WorldSpaceCameraPos - i.worldPos);
	            
                // Normaliser de nouveau la normale de la struct v2f
                
    i.worldNormal = normalize(i.worldNormal);
    
                // Calcul du produit scalaire entre le vecteur PixelToCamera (View vector) & la normale
    float NdotV = dot(i.worldNormal, FragmentToCamera);
    
                // Visualiser le résultat de NdotV  => ligne temporaire, juste pour comprendre l'effet à cette étape
   // return NdotV;
                // "Ajuster" le résultat obtenu
    NdotV = 1 - NdotV;
                // Utiliser la fonction pow(valueToRaise, FresnelExponent)
    float rim = pow(NdotV, _Exponent);
                // lerp entre BaseColor, FresnelColor et le rim calculé ci-dessus.
    return lerp(_BaseColor, _FresnelColor, rim);
}
            ENDHLSL
        }
    }
}
