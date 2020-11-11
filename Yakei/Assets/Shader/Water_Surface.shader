//歪みシェーダー（Planeにアタッチして海面の表現に使用）
Shader "Custom/Water_Surface"
{
    Properties
    {
        _DistortionTex("Distortion Texture(RG)",2D) = "gray"{}  //gray = rgb:128の色(ちょうど真ん中)　※テクスチャ未設定時のデフォルトということかな？
        _DistortionPower("DistortionPower",Range(0,1)) = 0
    }
    SubShader
    {
        Tags
        { 
            //海上の他オブジェクトより前に描画するために、QueueをTransparent-1に設定
            "Queue" = "Transparent-1"
            "RenderType"="Opaque" 
        }
        
        Cull Back   //オブジェクトの裏面を描画しない
        ZWrite On   //デプスバッファへの書き込みON
        ZTest LEqual    //デプステスト方式：LEqual すでに描画されているオブジェクトと距離が等しいかより近しい場合に描画
        ColorMask RGB   //カラーチャンネルへの書き込みを設定（RGBに書き込み可能、αには多分書き込みできない設定）

        GrabPass{"_GrabPassTexture"}

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _DistortionTex;
        sampler2D _GrabPassTexture;
        half _DistortionPower;

        struct Input{
            float2 uv_DistortionTex;
            float4 screenPos;
        };

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //スクリーン座標をw除算
            half2 uv = half2(IN.screenPos.x/IN.screenPos.w,IN.screenPos.y/IN.screenPos.w);
            //Distortionの値に応じて、DistortionTextureからサンプリングするuvをずらす + uvスクロール
            half2 distortion = tex2D(_DistortionTex,IN.uv_DistortionTex + _Time.x*0.1f).rg - 0.5;
            distortion *= _DistortionPower;
            uv = uv + distortion;
            //最終的に描画されたレンダリング結果(=_GrabPasstexture)を描画する
            o.Albedo = tex2D(_GrabPassTexture,uv);
        }

        ENDCG
    }
}
