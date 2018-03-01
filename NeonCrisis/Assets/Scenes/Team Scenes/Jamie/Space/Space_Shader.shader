Shader "Unlit/TestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	// The "main texture" which should contain a tiling noise texture.. this pans downwards to give a sense of speed
		_MainTexPanningSpeed("Texture Pan Speed", Float) = 0.10
	// how much of this texture is blended with the resulting input
		_MainTexStrength("Texture Blend Strength", Float) = 0.3
	// fractal layer one
		_ColorTintLayer1 ("Layer1Color", Color) = (0, 0, 0, 1)    // (R, G, B, A)
		_FractalLayer1TimeScale ("Frac L1 Timescale", Float) = 0.0
		_FractalLayer1ScaleFloat("Frac L1 Scale", Float) = 0.0
		_FractalLayer1Strength("Frac L1 Strength", Float) = 1.0
		_FractalLayer1OffsetXFloat("Frac L1 Offset X", Float) = 0.0
		_FractalLayer1OffsetYFloat("Frac L1 Offset Y", Float) = 0.0
	// fractal layer two
		_ColorTintLayer2 ("Layer2Color", Color) = (0, 0, 0, 1)    // (R, G, B, A)
		_FractalLayer2TimeScale("Frac L2 Timescale", Float) = 0.0
		_FractalLayer2ScaleFloat("Frac L2 Scale", Float) = 0.0
		_FractalLayer2Strength("Frac L2 Strength", Float) = 1.0
		_FractalLayer2OffsetXFloat("Frac L2 Offset X", Float) = 0.0
		_FractalLayer2OffsetYFloat("Frac L2 Offset Y", Float) = 0.0

	// starfield input
		_StarfieldColor("StarfieldColor", Color) = (0,0,0,1)
		_StarfieldXDirection("SF X Dir", Float) = 50.0
		_StarfieldYDirection("SF Y Dir", Float) = 10.0
		_StarfieldBlendStrength("Starfield Blend Strength", Float) = 0.3
		_StarfieldExp("Starfield Exp", Float) = 0.0001

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
				float2 uv2 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float _MainTexPanningSpeed;
			float _MainTexStrength;
			float4 _MainTex_ST;
			// fractal layer one
			half4 _ColorTintLayer1;
			float _FractalLayer1TimeScale;
			float _FractalLayer1ScaleFloat;
			float _FractalLayer1OffsetXFloat;
			float _FractalLayer1OffsetYFloat;
			float _FractalLayer1Strength;
			// fractal layer two
			half4 _ColorTintLayer2;
			float _FractalLayer2TimeScale;
			float _FractalLayer2ScaleFloat;
			float _FractalLayer2OffsetXFloat;
			float _FractalLayer2OffsetYFloat;
			float _FractalLayer2Strength;

			half4 _StarfieldColor;
			float _StarfieldXDirection;
			float _StarfieldYDirection;
			float _StarfieldBlendStrength;
			float _StarfieldExp;

			// vertex shader code.. basically doesn't do much other than pass UV coords into pixel shader
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				// transform tex takes the input UV and scales and offsets it based on the 
				// texture settings of the material parameter
				o.uv2 = TRANSFORM_TEX(v.uv, _MainTex);
				// this is the unscaled and offset version of the input UV so basically 0..1 in both X and Y coords
				o.uv = v.uv;
				return o;
			}

			// function to calculate a fractal pattern
			// http://www.fractalforums.com/new-theories-and-research/very-simple-formula-for-fractal-patterns/
			float field(in float3 p, float s) {
				float strength = 7. + .03 * log(1.e-6 + frac(sin(_Time.y) * 4373.11));
				float accum = s/4.;
				float prev = 0.;
				float tw = 0.;
				// note the 26 here? try fewer and see what you get?
				for (int i = 0; i < 26; ++i) {
					float mag = dot(p, p);
					p = abs(p) / mag + float3(-.5, -.4, -1.5);
					float w = exp(-float(i) / 7.);
					accum += w * exp(-strength * pow(abs(mag - prev), 2.3));
					tw += w;
					prev = mag;
				}
				return max(0., 5. * accum / tw - .7);
			}

			// slightly simpler function to calculate the fractal pattern
			float field2(in float3 p, float s) {
				float strength = 7. + .03 * log(1.e-6 + frac(sin(_Time.y) * 4373.11));
				float accum = s / 4.;
				float prev = 0.;
				float tw = 0.;
				// note the 18 iterations here? Try fewer and see what you get!
				for (int i = 0; i < 18; ++i) {
					float mag = dot(p, p);
					p = abs(p) / mag + float3(-.5, -.4, -1.5);
					float w = exp(-float(i) / 7.);
					accum += w * exp(-strength * pow(abs(mag - prev), 2.3));
					tw += w;
					prev = mag;
				}
				return max(0., 5. * accum / tw - .7);
			}

			// given a UV? return a float3 (colour) based on a bit of maths - used to create the starfield
			float3 nrand3(float2 co)
			{
				float3 a = frac(cos(co.x*8.3e-3 + co.y)*float3(1.3e5, 4.7e5, 2.9e5));
				float3 b = frac(sin(co.x*0.3e-3 + co.y)*float3(8.1e5, 1.0e5, 0.1e5));
				float3 c = lerp(a, b, 0.5);
				return c;
			}

			fixed4 CalculateStarfield(v2f i)
			{
				fixed4 starcolour = float4(0., 0., 0., 0.);
				starcolour = -starcolour;
				float2 u = float2(i.uv / 1.0);
				for (float n = .1; n < .9; n += .04)
				{
					float3 p = float3(u + (_Time.y / n - n) / float2 (_StarfieldXDirection, _StarfieldYDirection), n);
					//p = abs(1. - mod(p, 2.));
					p = abs(1. - (p % 2.));
					float a = length(p), b, c = 0.;
					for (float inner = .1; inner < .9; inner += .04)
					{
						p = abs(p) / a / a - .52;
						b = length(p);
						c += abs(a - b);
						a = b;
						starcolour += c * float4(inner, inner, inner, 0) / _StarfieldExp;
					}
				}
				return starcolour;
			}

			// this is the pixel shader code
			fixed4 frag (v2f i) : SV_Target
			{
				// get the colour for the first fractal layer
				// TODO: make this into a function rather than simply copying it twice!!
				// calculate the "scaled" time (_Time.y is unity's internal time value)
				float scaledtime1 = _Time.y * _FractalLayer1TimeScale;
				// calculate scaled UV coords (i.uv is the UV passed in from the vertex shader)
				float2 uvcoord1 = float2(_FractalLayer1OffsetXFloat + i.uv.x, _FractalLayer1OffsetYFloat + i.uv.y);
				// scale the UV's before we used them
				float2 uvs1 = uvcoord1 * _FractalLayer1ScaleFloat;
				// no idea.. think it scales and biases the UV's
				float3 p1 = float3(uvs1 / 4., 0) + float3(1., -1.3, 0.);
				// this does the sort of figure of 8 movement pattern with the UV's
				p1 += .2 * float3(sin(scaledtime1 / 16.), sin(scaledtime1 / 12.), sin(scaledtime1 / 128.));
				// call the "field" function to calculate the fractal value for this pixel
				float t1 = field(p1, _FractalLayer1Strength);
				// no idea what this is doing 
				float v1 = (1. - exp((abs(i.uv.x) - 1.) * 6.)) * (1. - exp((abs(i.uv.y) - 1.) * 6.));
				// output the final colour for this "layer"
				fixed4 layer1col = lerp(.4, 1., v1) * float4(1.8 * t1 * t1 * t1, 1.4 * t1 * t1, t1, 1.0);

				// and the second fractal layer (basically copies the above, but calls the cheaper field function
				float scaledtime2 = _Time.y * _FractalLayer2TimeScale;
				float2 uvcoord2 = float2(_FractalLayer2OffsetXFloat + i.uv.x, _FractalLayer2OffsetYFloat + i.uv.y);
				float2 uvs2 = uvcoord2 * _FractalLayer2ScaleFloat;
				float3 p2 = float3(uvs2 / 4., 0) + float3(1., -1.3, 0.);
				p2 += .2 * float3(sin(scaledtime2 / 16.), sin(scaledtime2 / 12.), sin(scaledtime2 / 128.));
				float t2 = field2(p2, _FractalLayer2Strength);
				float v2 = (1. - exp((abs(i.uv.x) - 1.) * 6.)) * (1. - exp((abs(i.uv.y) - 1.) * 6.));
				fixed4 layer2col = lerp(.4, 1., v2) * float4(1.8 * t2 * t2 * t2, 1.4 * t2 * t2, t2, 1.0);

				// scrolling texture.. make a greyscale noise texture and this will pan it?
				float2 pan = float2(i.uv.x, i.uv.y + _MainTexPanningSpeed * _Time.y);
				// this gets the colour of a texture given an input UV coordinate
				// note: the texture must be set to "repeat" otherwise things go wonky!
				fixed4 texcol = tex2D(_MainTex, pan);

				//Let's add some stars
				////Thanks to http://glsl.heroku.com/e#6904.0
				//float2 seed = pan.xy * 2.0;
				////seed = floor(seed * 1.0);
				//float3 rnd = nrand3(seed);
				//float sc1 = pow(rnd.y, 40.0);
				//float4 starcolor = float4(sc1,sc1,sc1,1.0);

				////Second Layer
				//float2 seed2 = p2.xy * 2.0;
				//seed2 = floor(seed2 * i.uv.y);
				//float3 rnd2 = nrand3(seed2);
				//float sc2 = pow(rnd2.y, 40.0);
				//starcolor += float4(sc2,sc2,sc2,sc2);
				fixed4 starcolour = CalculateStarfield(i);


				


				// draw the final image.. which is fractal layer 1 and 2 multiplied, then multiplied with
				// panning texture, then a starfield is added on top.
				//return layer1col * layer2col + (texcol * _MainTexStrength);
				// TODO: fix starfield stuff and then uncomment this line!
				//return layer1col * layer2col * (texcol * _MainTexStrength) + starcolour;
				return (layer1col + _ColorTintLayer1) * (layer2col + _ColorTintLayer2) * (_StarfieldColor * starcolour * _StarfieldBlendStrength);

				//return layer1col  * layer2col * (starcolour * _StarfieldBlendStrength) + starcolour;
				//return (texcol * _MainTexStrength) + starcolor;
				//return starcolour;
			}
			ENDCG
		}
	}
}
