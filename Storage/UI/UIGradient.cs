using UnityEngine;
using UnityEngine.UI;

namespace PhantomEngine
{
    [AddComponentMenu("UI/Effects/UIGradient")]
    public sealed class UIGradient : BaseMeshEffect
    {
        private enum GradientType
        {
            Linear, 
            Radial
        }
        
        [SerializeField]
        private Color GradientColor1 = Color.white;

        [SerializeField]
        private Color GradientColor2 = Color.black;
        
        [Range(-180f, 180f)]
        [SerializeField]
        private float GradientAngle = -90.0f;
        
        [SerializeField]
        private GradientType GradientStyle = GradientType.Linear;

        
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
                return;

            int vertCount = vh.currentVertCount;
            if (vertCount == 0)
                return;

            // Calculate the direction vector according to the angle
            Vector2 dir = new Vector2(Mathf.Cos(GradientAngle * Mathf.Deg2Rad), Mathf.Sin(GradientAngle * Mathf.Deg2Rad)).normalized;

            // Calculate the minimum and maximum coordinates of the mesh
            UIVertex vertex = default(UIVertex);
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            for (int i = 0; i < vertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                Vector3 pos = vertex.position;
                min.x = Mathf.Min(min.x, pos.x);
                min.y = Mathf.Min(min.y, pos.y);
                max.x = Mathf.Max(max.x, pos.x);
                max.y = Mathf.Max(max.y, pos.y);
            }

            Vector2 size = max - min;

            // Avoid error dividing by zero
            if (size.x == 0) size.x = 0.0001f;
            if (size.y == 0) size.y = 0.0001f;

            // Apply gradation
            for (int i = 0; i < vertCount; i++)
            {
                vh.PopulateUIVertex(ref vertex, i);
                Vector2 localPos = vertex.position;

                float t = 0f;
                if (GradientStyle == GradientType.Linear)
                {
                    Vector2 pos = localPos - min;
                    t = Vector2.Dot(pos / size, dir);
                }
                else if (GradientStyle == GradientType.Radial)
                {
                    Vector2 center = (min + max) * 0.5f;
                    t = Vector2.Distance(localPos, center) / (size.magnitude * 0.5f);
                }

                t = Mathf.Clamp01(t);

                Color color = Color.Lerp(GradientColor1, GradientColor2, t);
                vertex.color *= color;
                vh.SetUIVertex(vertex, i);
            }
        }
    }
}