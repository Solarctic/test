Shader "Unlit/Blinking"
{
    Properties
    {
        _IsBlinking("Is Blinking", range(0,1)) = 0
        _BaseColor ("Base Color",  Color) = (0,0,0,1)
        _BlinkColor("Blink Color", Color) = (1,1,1,1)
        _BlinkSpeed("Blink Speed", float) = 3
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float  _IsBlinking;
            float4 _BaseColor;
            float4 _BlinkColor;
            float  _BlinkSpeed;

            fixed4 frag(v2f i) : SV_Target
            {
                float blinkTime = floor(sin(_Time[1] * _BlinkSpeed) + 1) * _IsBlinking;
                float4 col = lerp(_BaseColor, _BlinkColor, blinkTime);
                
                return col;
            }
            ENDCG
        }
    }
}
