Shader "Unlit/Wireframe"
{
    Properties
    {
        _LineColor("Line Color", Color) = (1, 1, 1, 1)
        _WireThickness("Wire Thickness", Range(0, 800)) = 100
    }
        SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
        LOD 100
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2g
            {
                float4 vertex : SV_POSITION;
                float4 worldPosition : TEXCOORD0;
            };
            struct g2f
            {
                float4 vertex : SV_POSITION;
                float4 worldPosition : TEXCOORD0;
                float3 dist : TEXCOORD1;
            };
            float _WireThickness;
            fixed4 _LineColor;
            v2g vert(appdata v)
            {
                v2g o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPosition = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }
            [maxvertexcount(3)]
            void geom(triangle v2g i[3], inout TriangleStream<g2f> stream)
            {
                float2 p0 = i[0].vertex.xy / i[0].vertex.w;
                float2 p1 = i[1].vertex.xy / i[1].vertex.w;
                float2 p2 = i[2].vertex.xy / i[2].vertex.w;
                float2 edge0 = p2 - p1;
                float2 edge1 = p2 - p0;
                float2 edge2 = p1 - p0;

                // Area formula ... Cross product / 2
                // However, for avoiding dividing by 2. It will use as double area.
                float area = abs(edge1.x * edge2.y - edge1.y * edge2.x);
                float thickness = 800 - _WireThickness;
                g2f o;
                float v;
                o.worldPosition = i[0].worldPosition;
                o.vertex = i[0].vertex;
                v = area / length(edge0);
                // This means calculating "Height".
                // Height = Area / Base. (Base = length(edge))
                o.dist.xyz = float3(v, 0, 0) * o.vertex.w * thickness;
                stream.Append(o);
                o.worldPosition = i[1].worldPosition;
                o.vertex = i[1].vertex;
                v = area / length(edge1);
                o.dist.xyz = float3(0, v, 0) * o.vertex.w * thickness;
                stream.Append(o);
                o.worldPosition = i[2].worldPosition;
                o.vertex = i[2].vertex;
                v = area / length(edge2);
                o.dist.xyz = float3(0, 0, v) * o.vertex.w * thickness;
                stream.Append(o);
            }
             fixed4 frag(g2f i) : SV_Target
            {
                float minDistanceToEdge = min(i.dist.x, min(i.dist.y, i.dist.z));
                fixed4 col = _LineColor;
                col.a *= 1.0 - minDistanceToEdge;
                return col;
            }
            ENDCG
        }
    }
}