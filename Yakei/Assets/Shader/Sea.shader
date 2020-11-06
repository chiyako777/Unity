//夜の海の表現
Shader "Custom/Sea"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        //vertexシェーダーで処理された頂点情報(surfシェーダーへのinput)
        //疑問：surfシェーダーはピクセル単位で呼出しだから、
        //　　　このinput構造体のデータもピクセル単位のデータ？
        struct Input
        {
            float2 uv_MainTex;  //テクスチャのuv座標
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //**uvスクロールで水面を動かす(他制御に干渉しないように、uv座標を別変数に移す)
            fixed2 uv = IN.uv_MainTex;
            uv.x += 0.1 * _Time;
            uv.y += 0.2 * _Time;
            o.Albedo = tex2D(_MainTex,uv);

            //**テクスチャの境目をぼかす
            if(IN.uv_MainTex.x < 0.01 || IN.uv_MainTex.x > 0.99){
                o.Albedo = fixed4(1, 1, 1, 1);
            }
            if(IN.uv_MainTex.y < 0.01 || IN.uv_MainTex.y > 0.99){
                o.Albedo = fixed4(1, 1, 1, 1);
            }
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
