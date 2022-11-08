using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC.View.Grid
{
    //Class Based On Hex Grid Tutorial: https://www.youtube.com/watch?v=EPaSmQ2vtek&list=PLL_zf3MigDAPckjYBJ1nha_Toww3zHY7j&index=1
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class HexRenderer : MonoBehaviour
    {
        //Unity Component
        private Mesh mesh;
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;
        public Material TileMaterial;

        //Geometry Variabels
        public float InnerRadious;
        public float OuterRadious;
        public float Height;
        public bool IsFlatToped;
        private List<Face> faces;
        private const int numberOfFaces = 6;
      
        private void Awake()
        {
            SetupMesh();
        }

        

        private void SetupMesh()
        {
            //Get Component Used In Order To Remove The Necessity Of Manual Setup On Prefab
            meshFilter = gameObject.GetComponent<MeshFilter>();
            meshRenderer = gameObject.GetComponent<MeshRenderer>();


            mesh = new Mesh();
            mesh.name = "Hex";

            meshFilter.mesh = mesh;
            meshRenderer.material = TileMaterial;
            gameObject.AddComponent<MeshCollider>();
        }

        private void OnEnable()
        {
            DrawMesh();
        }

        public virtual void DrawMesh()
        {
            DrawFaces();
            CombineFaces();
        }

        protected virtual void DrawFaces()
        {
            faces = new List<Face>();

            //Top Faces
            for (int i = 0; i < numberOfFaces; i++)
            {
                faces.Add(CreateFace(InnerRadious, OuterRadious, Height / 2, Height / 2, i));
            }

            // Botton Faces
            for (int i = 0; i < numberOfFaces; i++)
            {
                faces.Add(CreateFace(InnerRadious, OuterRadious, -Height / 2, -Height / 2, i, true));
            }

            //Outer Faces
            for (int i = 0; i < numberOfFaces; i++)
            {
                faces.Add(CreateFace(OuterRadious, OuterRadious, Height / 2, -Height / 2, i, true));
            }

            // Inner Faces
            for (int i = 0; i < numberOfFaces; i++)
            {
                faces.Add(CreateFace(InnerRadious, InnerRadious, Height / 2, -Height / 2, i));
            }
        }

        protected virtual Face CreateFace(float innerRad, float outerRad, float heightA, float heightB, int point, bool reverse = false)
        {
            Vector3 pointA = GetPoint(innerRad, heightB, point);
            Vector3 pointB = GetPoint(innerRad, heightB, (point < 5) ? point + 1 : 0); // this is responsible to the last point to conect To the fist
            Vector3 pointC = GetPoint(outerRad, heightA, (point < 5) ? point + 1 : 0);
            Vector3 pointD = GetPoint(outerRad, heightA, point);

            List<Vector3> vertices = new List<Vector3>() { pointA, pointB, pointC, pointD };
            List<int> triangles = new List<int>() { 0, 1, 2, 2, 3, 0 };
            List<Vector2> uvs = new List<Vector2>() { new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) };

            if (reverse)
            {
                vertices.Reverse();
            }

            return new Face(vertices, triangles, uvs);
        }
        
        protected virtual Vector3 GetPoint(float size, float height, int index)
        {
            float angleDegree = IsFlatToped ? 60f * index : 60f * index - 30f;
            float angleRadian = Mathf.PI / 180f * angleDegree;
            return new Vector3(size * Mathf.Cos(angleRadian), height, size * Mathf.Sin(angleRadian));
        }

        protected virtual void CombineFaces()
        {
            List<Vector3> vertices = new List<Vector3>();
            List<int> tris = new List<int>();
            List<Vector2> uvs = new List<Vector2>();

            for (int i = 0; i < faces.Count; i++)
            {
                //Adding Vertices
                vertices.AddRange(faces[i].Vertices);
                uvs.AddRange(faces[i].Uvs);

                //Offset Triagles
                int offset = 4 * i;
                foreach (int triangle in faces[i].Triangles)
                {
                    tris.Add(triangle + offset);
                }
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = tris.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.RecalculateNormals();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
            {
                DrawMesh();
            }
        }
#endif
    }

    public struct Face
    {
        public List<Vector3> Vertices;
        public List<int> Triangles;
        public List<Vector2> Uvs;

        public Face(List<Vector3> vertices, List<int> triangles, List<Vector2> uvs)
        {
            Vertices = vertices;
            Triangles = triangles;
            Uvs = uvs;
        }
    }
}


