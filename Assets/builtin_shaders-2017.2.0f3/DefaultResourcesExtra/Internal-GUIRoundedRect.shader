// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Hidden/Internal-GUIRoundedRect"
{
    Properties {
        _MainTex ("Texture", any) = "white" {}
    }

    CGINCLUDE
    #pragma vertex vert
    #pragma fragment frag
    #pragma target 2.5

    #include "UnityCG.cginc"

    struct appdata_t {
        float4 vertex : POSITION;
        fixed4 color : COLOR;
        float2 texcoord : TEXCOORD0;
    };

    struct v2f {
        float4 vertex : SV_POSITION;
        fixed4 color : COLOR;
        float2 texcoord : TEXCOORD0;
        float2 clipUV : TEXCOORD1;
        float4 pos : TEXCOORD2;
    };

    sampler2D _MainTex;
    sampler2D _GUIClipTexture;

    uniform float4 _MainTex_ST;
    uniform float4x4 unity_GUIClipTextureMatrix;

    uniform float _CornerRadius;
    uniform float _BorderWidths[4];
    uniform float _Rect[4];

    half GetCornerAlpha(float2 p, float2 center, float borderWidth1, float borderWidth2, float radius, float pixelScale)
    {
        bool hasBorder = borderWidth1 > 0.0f || borderWidth2 > 0.0f;

        float2 v = p - center;
        float pixelCenterDist = length(v);

        float outRad = radius;
        float outerDist = (pixelCenterDist - outRad) * pixelScale;
        half outerDistAlpha = hasBorder ? saturate(0.5f + outerDist) : 0.0f;

        float a = radius - borderWidth1;
        float b = radius - borderWidth2;
        float ellipseDist= (v.x*v.x)/(a*a) + (v.y*v.y)/(b*b);

        v.y *= a/b;
        half rawDist = (length(v) - a) * pixelScale;
        half alpha = saturate(rawDist+0.5f);
        half innerDistAlpha = hasBorder ? ((a > 0 && b > 0) ? alpha : 1.0f) : 0.0f;

        return (outerDistAlpha == 0.0f) ? innerDistAlpha : (1.0f - outerDistAlpha);
    }

    bool IsPointInside(float2 p, float4 rect)
    {
        return p.x >= rect.x && p.x <= (rect.x+rect.z) && p.y >= rect.y && p.y <= (rect.y+rect.w);
    }

    v2f vert (appdata_t v)
    {
        float3 eyePos = UnityObjectToViewPos(v.vertex);
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.color = v.color;
        o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
        o.clipUV = mul(unity_GUIClipTextureMatrix, float4(eyePos.xy, 0, 1.0));
        o.pos = v.vertex;
        return o;
    }

    fixed4 frag (v2f i) : SV_Target
    {
        float pixelScale = 1.0f/abs(ddx(i.pos.x));

        half4 col = tex2D(_MainTex, i.texcoord) * i.color;
        float2 p = i.pos.xy;

        float2 center = float2(_Rect[0]+_CornerRadius, _Rect[1]+_CornerRadius);

        float cornerRadius2 = _CornerRadius * 2.0f;
        float middleWidth = _Rect[2] - cornerRadius2;
        float middleHeight = _Rect[3] - cornerRadius2;

        bool xIsLeft = (p.x - _Rect[0] - _Rect[2]/2.0f) <= 0.0f;
        bool yIsTop = (p.y - _Rect[1] - _Rect[3]/2.0f) <= 0.0f;

        float bw1 = _BorderWidths[0];
        float bw2 = _BorderWidths[1];

        if (!xIsLeft)
        {
            center.x += middleWidth;
            bw1 = _BorderWidths[2];
        }

        if (!yIsTop)
        {
            center.y += middleHeight;
            bw2 = _BorderWidths[3];
        }

        bool isInCorner = (xIsLeft ? p.x <= center.x : p.x >= center.x) && (yIsTop ? p.y <= center.y : p.y >= center.y);
        col.a *= isInCorner ? GetCornerAlpha(p, center, bw1, bw2, _CornerRadius, pixelScale) : 1.0f;

        bool isPointInCenter = IsPointInside(p, float4(_Rect[0]+_BorderWidths[0], _Rect[1]+_BorderWidths[1], _Rect[2]-(_BorderWidths[0]+_BorderWidths[2]), _Rect[3]-(_BorderWidths[1]+_BorderWidths[3])));
        half middleAlpha = isPointInCenter ? 0.0f : col.a;
        bool hasBorder = _BorderWidths[0] > 0 || _BorderWidths[1] > 0 || _BorderWidths[2] > 0 || _BorderWidths[3] > 0;
        col.a *= hasBorder ? (isInCorner ? col.a : middleAlpha) : 1.0f;

        col.a *= tex2D(_GUIClipTexture, i.clipUV).a;

        return col;
    }
    ENDCG

    SubShader {
        Blend SrcAlpha OneMinusSrcAlpha, One One
        Cull Off
        ZWrite Off
        ZTest Always

        Pass {
            CGPROGRAM
            ENDCG
        }
    }

    SubShader {
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        ZTest Always

        Pass {
            CGPROGRAM
            ENDCG
        }
    }

FallBack "Hidden/Internal-GUITextureClip"
}
