//GrabPass スクリーンポジションに合わせて描画
Shader "Unlit/GrabPassScreen"
{
	Properties{
		_Texture("Texture",2D)="white"{}
		_Float("float",float) = 1
		_Color("Color",Color) = (1,1,1,1)
		_Range("Range",Range(-10,10))=0.5
		_Vector("ector",Vector)=(0,0,0,0)

	}
    SubShader
    {
		Tags{
			"RenderType"="Transparent"
			"Queue"="Transparent"
		}
        //すでに描画されている画面をテクスチャとして取得する
		GrabPass{}

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _GrabTexture;

			sampler2D _Texture;
			float4 _Texture_ST;
			
			fixed _Float;
			fixed4 _Color;
			fixed _Range;
			fixed4 _Vector;

			
			struct appdata
			{
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
                //スクリーンのポジションを保持する変数
				float4 screenPos : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
                //頂点座標変換
				o.vertex = UnityObjectToClipPos(v.vertex);
                //スクリーンポジション(そのピクセルのスクリーン上での座標)の取得
				o.screenPos = ComputeGrabScreenPos(o.vertex);
                //テクスチャUV座標変換
				o.uv = TRANSFORM_TEX(v.texcoord, _Texture);
				
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
                //w 除算（⇒同次座標系を理解すること）
				//（多分頂点座標とかはUnityObjectToClipPosの中とかでやってくれてるんだろうけど、screenPosの場合は自前でやってねという話だと思う）
				half2 grabUV = (i.screenPos.xy / i.screenPos.w);
                //今のピクセルのスクリーンポジション上の色を取得
                //・・・つまり、このままだとこのシェーダーをアタッチしたオブジェクトは透明になる、ということなんだけど、、、
                //※アタッチしているオブジェクトを可視化するために今は_Colorを乗算している
				fixed4 tex = tex2D(_GrabTexture, grabUV) * _Color;
                
                //実は様々な表現に使える
                tex.rgb = 1 - tex.rgb;  //例：色の反転
                
				return tex;
			}
			ENDCG
        }
    }
}
