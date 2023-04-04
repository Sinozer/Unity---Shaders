Shader "Learning/Unlit/Reference"
{
    // Propriétés exposées dans l'inspector
    // NE PAS OUBLIER DE FAIRE LE LIEN APRES DANS VOTRE CODE POUR Y ACCEDER
    Properties
    {   
        // NOM_VARIABLE("NOM_AFFICHE_DANS_L'INSPECTOR", Shaderlab type) = defaultValue 
		_WaveScale("Wave scale", Float) = 0.07
        _ReflDistort("Reflection distort", Range(0,1.5)) = 0.5  // slider
        _TintColor("Tint", Color) = (0.34, 0.85, 0.92, 1) // color
        _WindDir("Wind Direction", Vector) = (1, 0, 0, 1)
        _ShieldMap("Shield Map", 2D) = "white" {} // textures 
        _SwordMap("Sword Map", 2D) = "black" {} 	
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Geometry" }

		Pass
        {
			HLSLPROGRAM
            #pragma vertex vert  // On définit le vertex shader. Nom de la fonction: vert (peut être changé)
            #pragma fragment frag  // On définit le fragment shader. Nom de la fonction: frag (peut être changé)

            #include "UnityCG.cginc"
			
			// Ecrivez ici les variables exposées dans le bloc Properties
            float _WaveScale, _ReflDistort;
            sampler2D _ShieldMap, _Re_SwordMapfractionTex;
            float4 _TintColorRefrColor, _WindDir;
			
			struct vertexInput
            {
                float4 vertex : POSITION;						
            };
            
            // Autre exemple d'une struct contenant beaucoup plus de data 
            /*
            struct vertexInput {
                float4 vertex : POSITION;
                float4 tangent : TANGENT;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
                float4 texcoord1 : TEXCOORD1;
                float4 texcoord2 : TEXCOORD2;
                float4 texcoord3 : TEXCOORD3;
                float4 color : COLOR;
            };
            */
			
            // Données qui vont être interpolées par le Rasterizer et qui seront en input du fragment shader
            // Chaque variable de cette struct doit être calculée dans le vertex shader !
            struct v2f   // v2f = vertex to fragment     ou p-e appelé vertexOutput
            {
	            float4 vertex : SV_POSITION; // => SV_POSITION signifie que c'est la position en clip space 
            };

            v2f vert(vertexInput v)
            {
	            v2f o;
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex); // ETAPE OBLIGATOIRE DU VS. De l'espace objet à l'espace de clipping (écran)
	            return o;
            }

            // Etape intermédiaire entre le VS et le FS, la rasterization. 
            // Le GPU va déterminer les fragments couverts par les polygones, et les données transférées
            // du VS au FS seront interpolées.

            // FRAGMENT SHADER: calcul la couleur finale du fragment/pixel
            float4 frag(v2f i) : SV_Target
            {
	            return float4(1,0,0,0);
            }
            
            ENDHLSL
        }
    }
}
