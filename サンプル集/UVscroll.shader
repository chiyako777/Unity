//水面シェーダー（uvスクロール)
Shader "Custom/UVscroll"
{
    Properties
    {
        _MainTex ("Water TExture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed2 uv = IN.uv_MainTex;
            //テクスチャをスクロール（経過時間分だけOffSetをずらしていく)
            uv.x += 0.3 * _Time;
            uv.y += 0.4 * _Time;
            o.Albedo = tex2D(_MainTex,uv);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
