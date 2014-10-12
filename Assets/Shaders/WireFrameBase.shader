Shader "Custom/WireFrameBase"
{
 Properties
 {
   _LineColor ("Line Color", Color) = (1,1,1,1)
   _GridColor ("Grid Color", Color) = (1,1,1,0)
   _LineWidth ("Line Width", float) = 0.2
   _Color ("Main Color", Color) = (1,1,1,1)
   _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
 }
 SubShader
 {
 	CGPROGRAM
	#pragma surface surf BlinnPhong
 
 	sampler2D _MainTex;
 	fixed4 _Color;
 	
 	struct Input {
		    float2 uv_MainTex;
		    float3 viewDir;
		};
     
     void surf (Input IN, inout SurfaceOutput o) {
		    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
		    fixed4 c = tex * _Color;
		    o.Albedo = c.rgb;
		   
		    o.Gloss = tex.a;
		    o.Alpha = c.a;
		}
		
	ENDCG
 	
 	
   Pass
   {
     Tags { "RenderType" = "Opaque" }
     Blend SrcAlpha OneMinusSrcAlpha
     AlphaTest Greater 0.5
 
     CGPROGRAM
     #pragma vertex vert
     #pragma fragment frag 
 
     uniform float4 _LineColor;
     uniform float4 _GridColor;
     uniform float _LineWidth;
     
 
     // vertex input: position, uv1, uv2
     struct appdata
     {
       float4 vertex : POSITION;
       float4 texcoord1 : TEXCOORD0;
       float4 color : COLOR;
     };
 
     struct v2f
     {
       float4 pos : POSITION;
       float4 texcoord1 : TEXCOORD0;
       float4 color : COLOR;
     };
 
     v2f vert (appdata v)
     {
       v2f o;
       o.pos = mul( UNITY_MATRIX_MVP, v.vertex);
       o.texcoord1 = v.texcoord1;
       o.color = v.color;
       return o;
     }
 
     fixed4 frag(v2f i) : COLOR
     {
       fixed4 answer;
 
       float lx = step(_LineWidth, i.texcoord1.x);
       float ly = step(_LineWidth, i.texcoord1.y);
       float hx = step(i.texcoord1.x, 1.0 - _LineWidth);
       float hy = step(i.texcoord1.y, 1.0 - _LineWidth);
 
       answer = lerp(_LineColor, _GridColor, lx*ly*hx*hy);
 
       return answer;
     }
     ENDCG
    }
 } 
 Fallback "Vertex Colored", 1
}