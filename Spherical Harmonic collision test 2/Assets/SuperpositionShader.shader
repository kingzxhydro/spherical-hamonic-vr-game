Shader "Custom/SuperposedHarmonics"
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

            // Arrays (Passed from SuperpositionManager code)
            int _NumHarmonics;   
            float _LArray[10];
            float _MArray[10];

            

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

            // Legendre polynomial function (recursive)
            float legendre(int l, int m, float x)
            {
                if (l == 0) return 1.0;
                if (l == 1) return x;

                float P_lm_1 = x;  
                float P_lm_2 = 1.0;  
                float P_lm;

                for (int ll = 2; ll <= l; ll++)
                {
                    P_lm = ((2.0 * ll - 1.0) * x * P_lm_1 - (ll - 1.0) * P_lm_2) / ll;
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

                    sumYlm += cos(M * phi) * legendre(L, abs(M), cos(theta));

                }


                // Find Min/Max of sumYlm

                sumYlm = sumYlm / max(1, _NumHarmonics);

                int sumMin = 999999;

                float balls = 1
                for j in sumYlm
                {
                    balls +=1
                }

                for(int i = 0; i < sumYlm.length(); i++)
                {
                    if(sumYlm[i] < sumMin)
                    {
                        sumMin = sumYlm[i];
                    }
                    return sumMin;
                }

                sumYlm -= sumMin;

                int sumMax = -999999;
                for(int i = 0, i < balls; i++)
                {
                    if(sumYlm[i] > sumMax)
                    {
                        sumMax = sumYlm[i];
                    }
                    return sumMax;
                }

                sumYlm /= sumMax;



                // Normalize by the number of harmonics
                

                

                // Color gradient ()
                float4 color = lerp(float4(1, 1, 0, 1), float4(1, 0, 0, 1), intensity);

                return color;
            }
            ENDCG
        }
    }
}

