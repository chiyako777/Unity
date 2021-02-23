//GLSLから移植
Shader "Custom/Captino"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
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

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            //** カプチーノのような模様を作る
            float2 uv = IN.uv_MainTex - float2(0.5f,0.5f);

            float vertColor = 0.0f;
            for(float i=0.0f; i<30.0f; i++){
                float t = i + 0.3f;
                uv.y += sin(-t + uv.x * 9.0f) * 0.1;
                uv.x += cos(-t + uv.y * 6.0 + cos(t/1.0f)) * 0.15;
                float value = sin(uv.y * 10.0) + uv.x * 5.1;
                float stripColor = 1.0 / sqrt(abs(value)) * 3.0;

                vertColor += stripColor/50.0f;
            }

            fixed3 col = fixed3(vertColor*0.8f , vertColor*0.4f , vertColor*0.2f);
            col *= col.r + col.g + col.b;

            o.Albedo = col.rgb;
            o.Alpha = 1.0f;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
