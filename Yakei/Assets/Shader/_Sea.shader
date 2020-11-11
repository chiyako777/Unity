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
        //float4 _MainTex_TexelSize = float4(1/812,1/542,812,542);  //テクスチャサイズ(安全を見て、自分で値をべた書きしておく)
        float4 _MainTex_TexelSize;  //テクスチャサイズ
        float _SamplingDistance  = 1.0;
        static const int _SamplingCount = 7; //サンプリングするピクセル数(周囲7*7ピクセル)
        half weight[_SamplingCount] = {0.036,0.113,0.216,0.269,0.216,0.113,0.036};   //サンプリングした色をかけ合わせる重み

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
            //uv.x += 0.1 * _Time;
            //uv.y += 0.2 * _Time;
            //o.Albedo = tex2D(_MainTex,uv);

            //**テクスチャの境目をぼかす

            //サンプリングポイントの間隔を算出（テクスチャの縦横サイズを考慮して算出する）
            half2 offsetV = _MainTex_TexelSize.xy * half2(0.0,1.0) * _SamplingDistance;
            half2 offsetH = _MainTex_TexelSize.xy * half2(1.0,0.0) * _SamplingDistance;

            //サンプリング開始ポイントのuv座標算出
            half2 coordV = IN.uv_MainTex - offsetV * ((_SamplingCount-1) * 0.5);
            half2 coordH = IN.uv_MainTex - offsetH * ((_SamplingCount-1) * 0.5);

            //if(IN.uv_MainTex.x < 0.01 || IN.uv_MainTex.x > 0.99 || IN.uv_MainTex.y < 0.01 || IN.uv_MainTex.y > 0.99){
                half4 col = 0;
                //垂直方向に色を掛け合わせる
                // for(int j=0; j<_SamplingCount; j++){
                //     col += tex2D(_MainTex,coordV) * weight[j] * 0.5;
                //     //col += tex2D(_MainTex,coordV);
                //     coordV += offsetV;
                // }
                // //水平方向に色を掛け合わせる
                // for(int j=0; j<_SamplingCount; j++){
                //     col += tex2D(_MainTex,coordH) * weight[j] * 0.5;
                //     coordH += offsetH;
                // }
                
                o.Albedo = col;
                //o.Albedo = fixed4(1,1,1,1);
            //}
            
        }

        //half4 getBlurCol
        ENDCG
    }
    FallBack "Diffuse"
}
