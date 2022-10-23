Shader "Unlit/SminShader"
{
    Properties
    {
        _Position ("Circle Position", Range(0, 1)) = 0.5
        _Smooth ("Circle Smooth", Range(0.0, 0.1)) = 0.01
        _K ("strength", Range(0.0, 0.5)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _Position;
            float _Smooth;
            float _K;
            
            float circle(float2 p, float r)
            {
                float d = length(p) - r;
                return d;
            }

            float smin(float a, float b, float k)
            {  
                float h = clamp(0.5 + 0.5 * (b-a) / k, 0.0, 1.0);
                return lerp(b, a, k) - k * h * (1.0 - h);

            } 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float a = circle(i.uv, 0.5);
                float b = circle(i.uv - _Position, 0.2);

                float s = smin(a, b, _K);
                return float4(s.xxx, 1);

            }
            ENDCG
        }
    }
}
