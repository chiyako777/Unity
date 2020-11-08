//ガウシアンブラー
//参考：https://light11.hatenadiary.com/entry/2018/05/20/010105
Shader "Unlit/Blur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SamplingDistance ("Sampling Distance", float) = 1.0
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

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                //サンプリングポイント
                half2 coordV : TEXCOORD0;
                half2 coordH : TEXCOORD1;
                //サンプリングポイントのオフセット（ずらす感覚）
                half2 offsetV: TEXCOORD2;
                half2 offsetH: TEXCOORD3;

                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _MainTex_TexelSize;   //テクスチャサイズ(x:1/width, y:1/height ,z:width ,w:height)
            float _SamplingDistance;    //サンプリング距離（元のテクスチャ画像の何ピクセル間隔でサンプリングするか）
            static const int samplingCount = 7; //uv方向に「7*__SamplingDistance」、サンプリングする
            static const half weights[samplingCount] = { 0.036, 0.113, 0.216, 0.269, 0.216, 0.113, 0.036 };     //サンプリングした色に掛け合わせる重み（ガウス関数から導出）

            v2f vert (appdata v)
            {
                v2f o;
                //基本の座標変換
                o.vertex = UnityObjectToClipPos(v.vertex);
                half2 uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                //サンプリングポイントのオフセットを算出
                //**考え方：
                //  ⇒⇒テクスチャの所定の位置から周囲「7*__SamplingDistance」ピクセルサンプリングしたいのは分かった
                //      けど「テクスチャ画像の元の1ピクセル」はuv座標の数値で言うとどれくらいなのよ？が分からん！
                //　　　だから、TexelSizeを使う
                //      _Maintex_TexelSize.x = 1/テクスチャ画像のwidth = uv座標に直したときの1ピクセル分の数値(横)
                //      _Maintex_TexelSize.y = 1/テクスチャ画像のheight = uv座標に直したときの1ピクセル分の数値(縦)
                o.offsetV = _MainTex_TexelSize.xy * half2(0.0, 1.0) * _SamplingDistance;
                o.offsetH = _MainTex_TexelSize.xy * half2(1.0, 0.0) * _SamplingDistance;

                //サンプリング開始ポイントのUV座標
                o.coordV = uv - o.offsetV * ((samplingCount - 1) * 0.5);
                o.coordH = uv - o.offsetH * ((samplingCount - 1) * 0.5);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 col = 0;

                //垂直方向
                for (int j = 0; j < samplingCount; j++){
                    //サンプリングして重みをかける。後で水平方向も合成するため0.5をかける
                    col += tex2D(_MainTex, i.coordV) * weights[j] * 0.5;
                    //offset分だけサンプリングポイントをずらす
                    i.coordV += i.offsetV;
                }

                //水平方向
                for (int j = 0; j < samplingCount; j++){
                    col += tex2D(_MainTex, i.coordH) * weights[j] * 0.5;
                    i.coordH += i.offsetH;
                }

                return col;
            }
            ENDCG
        }
    }
}
