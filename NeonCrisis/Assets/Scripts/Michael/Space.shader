Shader "Unlit/Space"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_xScrollValue("XScrollingSpeed", Range(-10,10)) = 0
		_yScrollValue("YScrollingSpeed", Range(-10,10)) = 0
		_Transparency("Transparency", Range(0,1)) = 0
		
	}
	SubShader
	{
		Tags { 
		"RenderType" = "Transparent" 
		"Queue" = "Transparent"
		
		}
		LOD 100

		Cull Back

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
				float4 uvgrab : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _xScrollValue;
			fixed _yScrollValue;
			float _Transparency;

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//o.uv = v.uv;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				//o.uvgrab = ComputeGrabScreenPos(o.vertex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//Scroll UV
				fixed2 _UVScroller = i.uv;
				float x = _xScrollValue * _Time;
				float y = _yScrollValue * _Time;
				_UVScroller += fixed2(x, y);
			
				
				// sample the texture
				fixed4 col = tex2D(_MainTex, _UVScroller);
				
				//fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(i.uvgrab));

				clip(col.r - unity_DeltaTime);
				// apply fog
				return col;
			}
			ENDCG
		}
	}
}
