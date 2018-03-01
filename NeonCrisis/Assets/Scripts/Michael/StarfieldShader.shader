Shader "Unlit/StarfieldShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float Noise2d( in float2 x )
			{
			    float xhash = cos( x.x * 37.0 );
			    float yhash = cos( x.y * 57.0 );
			    return frac( 415.92653 * ( xhash + yhash ) );
			}

			float NoisyStarField( in float2 vSamplePos, float fThreshhold )
			{
			    float StarVal = Noise2d( vSamplePos );
			    if ( StarVal >= fThreshhold )
			        StarVal = pow( (StarVal - fThreshhold)/(1.0 - fThreshhold), 6.0 );
			    else
			        StarVal = 0.0;
			    return StarVal;
			}

			float StableStarField( in float2 vSamplePos, float fThreshhold )
			{
			    // Linear interpolation between four samples.
			    // Note: This approach has some visual artifacts.
			    // There must be a better way to "anti alias" the star field.
			    float fractX = frac( vSamplePos.x );
			    float fractY = frac( vSamplePos.y );
			    float2 floorSample = floor( vSamplePos );    
			    float v1 = NoisyStarField( floorSample, fThreshhold );
			    float v2 = NoisyStarField( floorSample + float2( 0.0, 1.0 ), fThreshhold );
			    float v3 = NoisyStarField( floorSample + float2( 1.0, 0.0 ), fThreshhold );
			    float v4 = NoisyStarField( floorSample + float2( 1.0, 1.0 ), fThreshhold );

			    float StarVal =   v1 * ( 1.0 - fractX ) * ( 1.0 - fractY )
			        			+ v2 * ( 1.0 - fractX ) * fractY
			        			+ v3 * fractX * ( 1.0 - fractY )
			        			+ v4 * fractX * fractY;
				return StarVal;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);


				float4 vColor = float4( 0.1, 0.2, 0.4,0 );

			    // Note: Choose fThreshhold in the range [0.99, 0.9999].
			    // Higher values (i.e., closer to one) yield a sparser starfield.
			    float StarFieldThreshhold = 0.0001;
			    // Stars with a slow crawl.
			    float xRate = 0.2;
			    float yRate = 5;
			    float2 vSamplePos = i.uv + float2( xRate * float( 1 ), yRate * float( 1 ) );
				float StarVal = StableStarField( vSamplePos, StarFieldThreshhold );
			    vColor += float4( StarVal, StarVal, StarVal, StarVal );
				
				float4 fragColor = float4(vColor);
				float4 col = float4(1,0,0,1);
				return vColor;
			}
			ENDCG
		}
	}
}
