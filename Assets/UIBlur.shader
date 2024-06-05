Shader "Custom/TransparentEdges"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeSize ("Edge Size", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _EdgeSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                // Calculate distance from center
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // Set alpha to 0 for edges, and smoothly transition to 1 towards the center
                float alpha = 1.0 - smoothstep(_EdgeSize, 0.5, dist);
                col.a *= alpha;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Diffuse"
}
