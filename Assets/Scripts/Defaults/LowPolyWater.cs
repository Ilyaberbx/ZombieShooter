using UnityEngine;

namespace LowPolyWater
{
    public class LowPolyWater : MonoBehaviour
    {
        [SerializeField] private float _waveHeight = 0.5f;
        [SerializeField] private float _waveFrequency = 0.5f;
        [SerializeField] private float _waveLength = 0.75f;

        public Vector3 waveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);

        private MeshFilter _meshFilter;
        private Mesh _mesh;
        private Vector3[] _vertices;

        private void Awake() => _meshFilter = GetComponent<MeshFilter>();
        private void Start() => CreateMeshLowPoly(_meshFilter);

        private MeshFilter CreateMeshLowPoly(MeshFilter meshFilter)
        {
            _mesh = meshFilter.sharedMesh;
            Vector3[] originalVertices = _mesh.vertices;

            int[] triangles = _mesh.triangles;
            Vector3[] vertices = new Vector3[triangles.Length];
            
            //Assign vertices to create triangles out of the mesh
            for (int i = 0; i < triangles.Length; i++)
            {
                vertices[i] = originalVertices[triangles[i]];
                triangles[i] = i;
            }

            _mesh.vertices = vertices;
            _mesh.SetTriangles(triangles, 0);
            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();
            _vertices = _mesh.vertices;

            return meshFilter;
        }

        private void Update() => GenerateWaves();
        private void GenerateWaves()
        {
            for (int i = 0; i < _vertices.Length; i++)
            {
                Vector3 v = _vertices[i];

                v.y = 0.0f;

                float distance = Vector3.Distance(v, waveOriginPosition);
                distance = (distance % _waveLength) / _waveLength;

                v.y = _waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * _waveFrequency
                + (Mathf.PI * 2.0f * distance));

                _vertices[i] = v;
            }

            SaveMeshSettings();
        }
        private void SaveMeshSettings()
        {
            _mesh.vertices = _vertices;
            _mesh.RecalculateNormals();
            _mesh.MarkDynamic();
            _meshFilter.mesh = _mesh;
        }
    }
  
}