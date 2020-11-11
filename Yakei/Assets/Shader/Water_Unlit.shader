//歪みシェーダー（Planeにアタッチして海面の表現に使用）
Shader "Unlit/Water_Unlit"
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
                float4 grabPos : TEXCOORD1;
            };

            sampler2D _DistortionTex;
            half4 _DistortionTex_ST;
            sampler2D _GrabPassTexture;
            half _DistortionPower;

            v2f vert (appdata v)
            {
                v2f o = (v2f)0;
                //座標変換
                o.vertex = UnityObjectToClipPos(v.vertex);
                //テクスチャの座標変換
                o.uv = TRANSFORM_TEX(v.uv, _DistortionTex);
                //スクリーンポジションの取得
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //w除算（同次座標系を理解すること。　最終的な座標値は(x/w,y/w,z/w)になるわけなので、これは絶対に必要な手順）
                half2 uv = half2(i.grabPos.x/i.grabPos.w,i.grabPos.y/i.grabPos.w);

                //Distortionの値に応じてサンプリングするuvをずらす + uvスクロール
                //**    -0.5しているのはデフォルトテクスチャ(gray)の場合distortion = 0 になって歪まないようにするための調整と思われる
                //**    挙動的には、明るい色だとdistortion > 0 になって、結果として+方向にuvが歪む
                //**               暗い色だとdistortion < 0 になって、結果として-方向にuvが歪む
                half2 distortion = tex2D(_DistortionTex,i.uv + _Time.x*0.1f).rg - 0.5;
                distortion *= _DistortionPower;

                uv = uv + distortion;
                return tex2D(_GrabPassTexture,uv);
            }
            ENDCG
        }
    }
}
