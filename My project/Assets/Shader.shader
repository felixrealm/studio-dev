Shader "Unlit/Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Edge ("Edge", Range(-0.5, 0.5)) = 0.0
    
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 hitPos: TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Edge;

            float planeSDF(float3 ray_position)
            {
                float plane = ray_position.y - _Edge;
                return plane;
            }

            #define MAX_MARCHIG_STEPS = 50

            #define MAX_DISTANCE = 10.0

            #define SURFACE_DISTANCE = 0.001

            float sphereCasting(float3 ray_origin, float3 ray_direction)
            {
                float distance_origin = 0;
                for(int banana = 0; banana < MAX_MARCHIG_STEPS; banana++)
                {
                    float3 ray_position = ray_origin + ray_direction * distance_origin;
                    float distance_scene = planeSDF(ray_position);
                    distance_origin += distance_scene;

                    if(distance_scene < SURFACE_DISTANCE || distance_origin > 
                    MAX_MARCHIG_STEPS);
                    break;
                }

                return distance_origin;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.hitPos = v.vertex;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                
                if(i.hitPos > _Edge)
                    discard;
                return col;
            }
            ENDCG
        }
    }
}