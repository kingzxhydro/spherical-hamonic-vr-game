﻿Shader "Custom/SuperposedHarmonics"
{
    Properties
    {
        _Color ("Base Color", Color) = (1,1,1,1)
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

            // Arrays (Passed from SuperpositionManager code or CorrectSolution code)
            int _NumHarmonics;   
            int _LArray[10];
            int _MArray[10];

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

            // Accurate Associated Legendre Polynomial function
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

            // Fragment Shader (Superposition Calculation)
            float4 frag(v2f i) : SV_Target
            {
                float3 n = normalize(i.worldNormal);
                float theta = acos(n.y);  
                float phi = atan2(n.z, n.x);

                // Superposition loop
                float sumYlm = 0.0;

                for (int idx = 0; idx < _NumHarmonics; idx++)
                {
                    int L = _LArray[idx];
                    int M = _MArray[idx];

                    sumYlm += cos(abs(M) * phi) * legendre(L, abs(M), cos(theta));
                }

                // Normalize by the number of harmonics
                sumYlm = sumYlm / max(1, _NumHarmonics);

                // not divide by 2, as for this it makes it very washed out
                //float intensity = (sumYlm + 1.0) / 2.0;
                float intensity = clamp(sumYlm + 0.5, 0.0, 1.0);


                // Color gradient ()
                float4 color = lerp(float4(1, 1, 0, 1), float4(1, 0, 0, 1), intensity);

                return color;
            }
            ENDCG
        }
    }
}

