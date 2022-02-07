Shader "My/Earth"
{
    Properties
    {
		_Day("Texture Day", 2D) = "White" {}
		_Night("Texture Night", 2D) = "White" {}
		_Spec("Texture Specular", 2D) = "White" {}
		_Clouds("Texture Clouds", 2D) = "White" {}
		_Smooth("Smooth Step", float) = 0.1
		_SpecularColor("SpecularColor", color) = (1,1,1,1)
		_Shininess("Shininess", int) = 128
		_Ambient("Ambient Power", float) = 0.2
		_CloudSpeed("cloud Speed", float) = -0.01
		_ColorIntensity("Color intensity", float) = 0.8
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
            };

            struct v2f
            {
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 ldir : TEXTCOORD1;
				float world : TEXTCOORD2;
				float2 uv : TEXCOORD0;
            };

			uniform sampler2D _Day;
			uniform sampler2D _Night;
			uniform sampler2D _Spec;
			uniform sampler2D _Clouds;
			uniform float4 _SpecularColor;
			uniform int _Shininess;
			uniform float _Ambient;
			uniform float _Smooth;
			uniform float _CloudSpeed;
			uniform float _ColorIntensity;

            v2f vert (appdata v)
            {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.normal = normalize(mul(float4(v.normal, 0), unity_WorldToObject).xyz);

				o.uv = v.uv;

				o.ldir = normalize(_WorldSpaceLightPos0.xyz);
				o.world = mul(UNITY_MATRIX_M, v.vertex).xyz;
				return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				i.normal = normalize(i.normal);
				float3 eye = normalize(_WorldSpaceCameraPos.xyz - i.world);

				float ndotl = dot(i.normal, i.ldir); //<- FOR LIGHT SHADING

				//float2 muv = i.uv + float2(1, 0) * _Time;

				float3 h = normalize(i.ldir + eye);
				float halfVector = max(0.0, dot(i.normal, h)); //Specular Radiance ndoth

				float4 colorDay = tex2D(_Day, i.uv) * _ColorIntensity;
				float4 colorNight = tex2D(_Night, i.uv);

				float t = smoothstep(-_Smooth, _Smooth, ndotl);
				float4 color = lerp(colorNight, colorDay, t); //(1 - t) *a + (t)*b 

				float isSpec = tex2D(_Spec, i.uv).r; //0 or 1
				float4 specular = _SpecularColor * pow(halfVector, _Shininess) * isSpec;

				float2 cuv = i.uv + float2(1, 0); //* _Time * _CloudSpeed;
				float isClouds = tex2D(_Clouds, cuv).r;
				float4 clouds = lerp(color, float4(1,1,1,1), isClouds * max(t, 0.1));

				//ndotl = max(_Ambient, ndotl);
				return clouds + specular;
            }
            ENDCG
        }
    }
}
