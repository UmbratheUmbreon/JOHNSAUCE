// This shader is licensed under a Creative Commons Attribution 4.0 International License.
// (https://creativecommons.org/licenses/by/4.0/)
// This means you MUST give me (YuraSuper2048) credit if you are using this.
// If you redistribute this shader (modified or not) you should save this message as is. 

Shader "Custom/Diffuse with SpriteRenderer support" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    LOD 200

CGPROGRAM
#pragma surface surf Lambert alpha:fade

sampler2D _MainTex;
fixed4 _Color;

struct Input {
    float2 uv_MainTex;
    fixed4 color : COLOR;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    o.Albedo = c.rgb * IN.color.rgb;
    o.Alpha = c.a * IN.color.a;
}
ENDCG
}

Fallback "Legacy Shaders/Transparent/VertexLit"
}