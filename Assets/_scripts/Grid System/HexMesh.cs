using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexMesh : MonoBehaviour
{

    [SerializeField] HexGridGenerator hexGenerator;
    [SerializeField] List<Vector3> hexPositions = new();
    [SerializeField] float radius;
    [SerializeField] int YOffset;
    [SerializeField] GameObject _parent;

    float Radius()
    {
        if (hexPositions.Count <= 1) return 1;
        return (hexPositions[1].x - hexPositions[0].x);
    }

    public Vector3[] Generate(Vector3 pos)
    {
        radius = Radius();
        float vert = 2 * radius / Mathf.Sqrt(3);
        Vector3[] points = new Vector3[7];
        points[0] = new(pos.x, pos.y + YOffset, pos.z);
        points[1] = new(pos.x + radius, pos.y + YOffset, pos.z - vert / 2);
        points[2] = new(pos.x + radius, pos.y + YOffset, pos.z + vert / 2);
        points[3] = new(pos.x, pos.y + YOffset, pos.z + vert);
        points[4] = new(pos.x - radius, pos.y + YOffset, pos.z + vert / 2);
        points[5] = new(pos.x - radius, pos.y + YOffset, pos.z - vert / 2);
        points[6] = new(pos.x, pos.y + YOffset, pos.z - vert);
        return points;
    }


    public MeshFilter meshFilter;


    [ContextMenu("Mesh_Create")]
    void CreateMesh()
    {
        if (_parent == null)
        {
            _parent = new GameObject();
        }
        else
        {
            DestroyImmediate(_parent);
            _parent = new GameObject();
        }
        GetPos();
        //SetGizmoPos();
        for (int i = 0; i < hexPositions.Count; i++)
        {
            Mesh_Generation(i);
        }
    }





    void Mesh_Generation(int index)
    {
        GameObject go = Instantiate(meshFilter.gameObject);
        go.transform.position = hexPositions[index];
        go.transform.parent = _parent.transform;
        Mesh mesh = new Mesh();
        var verts = Generate(Vector3.zero);
        Vector2[] uv = new[]
        {
            new Vector2(0.5f, 0.5f),//V0
            new Vector2(1, 0.25f),  //V1
            new Vector2(1, 0.75f),  //V2
            new Vector2(0.5f, 1),   //V3
            new Vector2(0, 0.75f),  //V4
            new Vector2(0, 0.25f),  //V5
            new Vector2(0.5f, 0),   //V6
        };
        int[] triangles = new[]
        {                                 //  x=i*7;          
            2,   1,   0,         //       //  x+2 , x+1, x,
            3,   2,   0,         //       //  x+3 , x+2, x,
            4,   3,   0,         //       //  x+4 , x+3, x,
            5,   4,   0,         //       //  x+5 , x+4, x,
            6,   5,   0,         //       //  x+6 , x+5, x,
            1,   6,   0,         //       //  x+1 , x+6, x,
        };
        mesh.vertices = verts;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        go.GetComponent<MeshFilter>().mesh = mesh;
    }





    [ContextMenu("Elle Get Positions")]
    void GetPos()
    {
        radius = Radius();
        hexPositions.Clear();
        foreach (var item in hexGenerator.hexes_Objects)
        {
            hexPositions.Add(item.transform.position);
        }
    }


    //#region GiZMOS



    ////[SerializeField] HashSet<Vector3> allVertsPositions = new();
    //[SerializeField] List<Vector3> allVertsPositions = new();
    //[SerializeField, Range(0.05f, .8f)] float gizmoRadius = .5f;
    //[SerializeField] int hashsetCount;
    //[ContextMenu("Elle Set Gizmo Positions")]
    //void SetGizmoPos()
    //{
    //    allVertsPositions.Clear();
    //    foreach (var hexPos in hexPositions)
    //    {
    //        foreach (Vector3 item in Generate(hexPos))
    //        {
    //            allVertsPositions.Add(item);
    //        }
    //    }
    //    hashsetCount = allVertsPositions.Count;
    //}


    ////private void OnDrawGizmos()
    //private void OnDrawGizmosSelected()
    //{
    //    if (allVertsPositions.Count <= 0) return;
    //    int i = 0;
    //    foreach (var item in allVertsPositions)
    //    {
    //        i++;
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(item, gizmoRadius);
    //        if (i >= 2270) break;
    //    }

    //}



    //#endregion
}
