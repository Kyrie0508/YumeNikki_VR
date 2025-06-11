Shader "Visutronik/CharacterUnlit"
{
    //https://discussions.unity.com/t/how-can-i-provide-shadows-in-my-own-shader/625217/2 

    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", COLOR) = (1,1,1,1)
        _Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5

        [HideInInspector] _BlendMode ("_BlendMode", Float) = 0.0
        [HideInInspector] _CullMode ("_CullMode", Float) = 2.0
        [HideInInspector] _VColBlendMode ("_VColBlendMode", Float) = 0.0
        [HideInInspector] _SrcBlend ("_SrcBlend", Float) = 1.0
        [HideInInspector] _DstBlend ("_DstBlend", Float) = 0.0
        [HideInInspector] _ZWrite ("_ZWrite", Float) = 1.0

        // VertexColor
    }
    SubShader
    {
        Tags { "RenderType"="Cutout" }
        LOD 100

        Pass
        {
            Cull [_CullMode]
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            ZTest LEqual
            BlendOp Add, Max
			
			Tags {"LightMode"="ForwardBase"}
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile _ _ALPHATEST_ON _ALPHABLEND_ON
            #pragma multi_compile _ _VERTEXCOL_MUL
            
            #include "UnityCG.cginc"
			
			// shadow helper functions and macros
			#include "Lighting.cginc"
			
			// compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
			
			// shadow helper functions and macros
			#include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal: NORMAL;

                #if defined(_VERTEXCOL_MUL)
                    fixed4 color : COLOR;
                #endif

                UNITY_VERTEX_INPUT_INSTANCE_ID 
            };

            struct v2f
            {
                //float4 vertex : SV_POSITION;
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
				
                //put shadows data into TEXCOORD1 => cast shadow
                SHADOW_COORDS(1) 

                //add for shadow functions
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;

                #if defined(_VERTEXCOL_MUL)
                    fixed4 color : COLOR2;
                #endif

                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;
            half _Cutoff;
            
            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.pos);
                
                #if defined(_VERTEXCOL_MUL)
                    o.color = v.color;
                #endif
			    
                //Calculate diffuse & ambient
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                o.diff = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz)) * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));

				// compute shadows data
                TRANSFER_SHADOW(o)
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                
                #if defined(_VERTEXCOL_MUL)
                    col *= i.color;
                #endif
                
                #if defined(_ALPHATEST_ON)
                    clip(col.a - _Cutoff);
                #endif
                
                #if !defined(_ALPHATEST_ON) && !defined(_ALPHABLEND_ON)
                    col.a = 1.0;
                #endif
                
                UNITY_APPLY_FOG(i.fogCoord, col);
				
				// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
                fixed shadow = SHADOW_ATTENUATION(i);
                // darken light's illumination with shadow, keep ambient intact
                fixed3 lighting = i.diff * shadow + i.ambient;
				
                //multiply the color with the light
                col.rgb *= lighting;                

                return col;
            }
            ENDCG
        }
		
		//shadow casting support
        //UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
        UsePass "Standard/SHADOWCASTER"
    }
}
