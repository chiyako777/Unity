// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
 The MIT License (MIT)

Copyright (c) 2013 yamamura tatsuhiko

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
Shader "UI/Mask"
{
	Properties
	{
		//★Propertiesは、頂点フラグメントシェーダー側で定義した同名のプロパティに自動的に紐づく

		//[PerRendererData] _MaskTex("Mask Texture", 2D) = "white" {}		//_MaskTexture:動的セット
		_MaskTex("Mask Texture", 2D) = "white" {}		//_MaskTexture:動的セット
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_Range("Range", Range (0, 1)) = 0

		_ColorMask ("Color Mask", Float) = 15
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			
			struct appdata_t	//頂点シェーダーのIN
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f		//頂点シェーダーのOUT、フラグメントシェーダーのIN
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};
			
			fixed4 _Color;		//Propertie紐づき
			fixed4 _TextureSampleAdd;	//どこで設定されてるのか今の所分からない
	
			//使っていない（2D Rect Mask使う時にどうたらこうたららしいけど、差し当たっては気にする必要なさそう)
			bool _UseClipRect;	
			float4 _ClipRect;
			bool _UseAlphaClip;

			v2f vert(appdata_t IN)
			{
				v2f OUT;

				//座標変換
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				//uv受け渡し
				OUT.texcoord = IN.texcoord;
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
				//Color受け渡し
				OUT.color = IN.color * _Color;
				return OUT;
			}

			//_MainTexのIN
			// FadeUIスクリプトの[SerializeField] Texture textureに設定されたものが
			// Graphics.Blitを使ってマスク画像にコピーされて、このマテリアルに紐づく形
			sampler2D _MainTex;		
			sampler2D _MaskTex;		//Propertieと紐づき
			float _Range;	//Propertieと紐づき

			fixed4 frag(v2f IN) : SV_Target
			{
				half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;
				half mask = tex2D(_MaskTex, IN.texcoord).a;
				//Range:0.5 ⇒ maskのαと最終結果のαが完全一致
				//Range:0.0 ⇒ 最終結果のαは確実に1以上 ⇒ つまり全くマスクされない
				//Range:1.0 ⇒ 最終結果のαは確実に0以下 ⇒ つまり全部マスクされる
				half alpha = mask - (-1 + _Range * 2);
				color.a *= alpha;
			
				clip (color.a - 0.001);		//color.a <= 0 なら描画を行わないように

				return color;
			}

		ENDCG
		}
	}
}
