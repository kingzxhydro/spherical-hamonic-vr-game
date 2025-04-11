Shader "Custom/OrbitalShader"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _L ("L (Polar Oscillations)", Range(0,10)) = 0
        _M ("M (Azimuthal Oscillations)", Range(-10,10)) = 0
        _Amplitude ("Displacement Amplitude", Range(0, 1)) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float4 _Color;
            int _L;
            int _M;
            float _Amplitude = 1.0;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : TEXCOORD0;
                float intensity : TEXCOORD1;
            };

            float legendre(int l, int m, float x)
            {
                if (l == 0) return 1.0;
                if (l == 1) return x;

                float P_lm_1 = x;
                float P_lm_2 = 1.0;
                float P_lm = 0;

                for (int ll = 2; ll <= l; ++ll)
                {
                    P_lm = ((2.0 * ll - 1.0) * x * P_lm_1 - (ll + m - 1.0) * P_lm_2) / ll;
                    P_lm_2 = P_lm_1;
                    P_lm_1 = P_lm;
                }

                return P_lm;
            }

            v2f vert(appdata v)
            {
                v2f o;

                float3 normalDir = normalize(v.normal);
                float3 pos = v.vertex.xyz;

                // Spherical angles from the normal
                float theta = acos(normalDir.y);
                float phi = atan2(normalDir.z, normalDir.x);

                // Spherical Harmonic (real part only)
                float P = legendre(_L, abs(_M), cos(theta));
                float Ylm = cos(_M * phi) * P;

                // Displacement (modulus squared) (Number is Amplitude)
                float displacement = 0.2 * abs(Ylm);

                // Apply displacement along the normal
                float3 displaced = pos + normalDir * displacement;

                o.pos = UnityObjectToClipPos(float4(displaced, 1.0));
                o.normal = UnityObjectToWorldNormal(v.normal);

                // For color in fragment
                o.intensity = clamp(Ylm + 0.5, 0.0, 1.0);

                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // Yellow (low) to red (high)
                float4 color = lerp(float4(1, 1, 0, 1), float4(1, 0, 0, 1), i.intensity);
                return color * _Color;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
