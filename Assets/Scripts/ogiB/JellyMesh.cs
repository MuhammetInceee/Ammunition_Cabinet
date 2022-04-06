using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMesh : MonoBehaviour
{
    public float Intensity = 1f;
    public float Mass = 1f;
    public float Stiffnes = 1f;
    public float damping = 0.75f;
    private Mesh _OrginialMesh, _MeshClone;
    private MeshRenderer _renderer;
    private Vector3[] _vertexArray;

    private JellyVertex[] _jv;
    void Start()
    {
        _OrginialMesh = GetComponent<MeshFilter>().sharedMesh;
        _MeshClone = Instantiate(_OrginialMesh);
        GetComponent<MeshFilter>().sharedMesh = _MeshClone;
        _renderer = GetComponent<MeshRenderer>();

        _jv = new JellyVertex[_MeshClone.vertices.Length];
        for(int i = 0; i< _MeshClone.vertices.Length; i++)
        {
            _jv[i] = new JellyVertex(i, transform.TransformPoint(_MeshClone.vertices[i]));
        }
    }
    void FixedUpdate()
    {
        _vertexArray = _OrginialMesh.vertices;
        for(int i = 0; i < _jv.Length; i++)
        {
            Vector3 target = transform.TransformPoint(_vertexArray[_jv[i]._ID]);
            float intesity = (1 - (_renderer.bounds.max.y - target.y) / _renderer.bounds.size.y) * Intensity;
            _jv[i].Shake(target, Mass, Stiffnes, damping);
            target = transform.InverseTransformPoint(_jv[i]._Position);
            _vertexArray[_jv[i]._ID] = Vector3.Lerp(_vertexArray[_jv[i]._ID], target, intesity);
        }
        _MeshClone.vertices = _vertexArray;
    }
    public class JellyVertex
    {
        public int _ID;
        public Vector3 _Position;
        public Vector3 _velocity, _Force;
        public JellyVertex(int id, Vector3 pos)
        {
            _ID = id;
            _Position = pos;
        }
        public void Shake(Vector3 target, float m, float s, float d)
        {
            _Force = (target - _Position) * s;
            _velocity = (_velocity + _Force / m) * d;
            _Position += _velocity;
            if ((_velocity + _Force + _Force / m).magnitude < 0.001f)
            {
                _Position = target;
            }
        }
    }

}
