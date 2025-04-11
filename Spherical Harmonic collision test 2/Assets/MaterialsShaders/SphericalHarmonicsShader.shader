Shader "Custom/SphericalHarmonics"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
        _L ("L (Polar Oscillations)", Range(0,5)) = 0
        _M ("M (Azimuthal Oscillations)", Range(-5,5)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Shader Properties
            float4 _Color;
            int _L;
            int _M;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            // Recursive Legendre polynomial function
            float legendre(int l, int m, float x)
            {
                if (l == 0) return 1.0;
                if (l == 1) return x;

                float P_lm_1 = x;  // P_1m(x)
                float P_lm_2 = 1.0;  // P_0m(x)
                float P_lm;

                for (int ll = 2; ll <= l; ll++)
                {
                    P_lm = ((2.0 * ll - 1.0) * x * P_lm_1 - (ll + m - 1.0) * P_lm_2) / ll;
                    P_lm_2 = P_lm_1;
                    P_lm_1 = P_lm;
                }

                return P_lm;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 n = normalize(i.worldNormal);
                float theta = acos(n.y);  // Polar angle (0 to π)
                float phi = atan2(n.z, n.x);  // Azimuthal angle (0 to 2π)

                // Compute the spherical harmonic function
                float Ylm = cos(_M * phi) * legendre(_L, abs(_M), cos(theta));

                // Normalize Ylm to range [0,1]
                float intensity = clamp(Ylm + 0.5, 0.0, 1.0);

                // Map intensity to yellow (low) and red (high)
                float4 color = lerp(float4(1, 1, 0, 1), float4(1, 0, 0, 1), intensity);

                return color;
            }
            ENDCG
        }
    }
}


