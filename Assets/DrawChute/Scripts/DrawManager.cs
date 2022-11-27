using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawManager : MonoBehaviour
{
    private Camera mainCam;
    private GameObject _currentDrawing;
    
    private void Awake()
    {
        mainCam = GetComponent<PlayerInput>().camera;
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DrawStart());
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            EditTheDrawing();
        }
    }

    private IEnumerator DrawStart()
    {
        GameObject drawing = new GameObject("DrawingObject");
        drawing.AddComponent<MeshRenderer>();
        var drawingMeshFilter = drawing.AddComponent<MeshFilter>();
        var drawingRenderer = drawing.GetComponent<Renderer>();
        if (_currentDrawing != null) { ResetPlayer();}
        _currentDrawing = drawing;
        
        Mesh mesh = new Mesh();
        List<Vector3> vertices = new List<Vector3>(new Vector3[8]);
        
        Vector3 startPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector3 temp = new Vector3(startPos.x, startPos.y, 0.5f);
        for (int i = 0; i < vertices.Count; i++)
        {
            vertices[i] = temp;
        }

        List<int> triangles = new List<int>(new int[36]);
        
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 0;
        triangles[4] = 3;
        triangles[5] = 2;
        //
        triangles[6] = 2;
        triangles[7] = 3;
        triangles[8] = 4;
        triangles[9] = 2;
        triangles[10] = 4;
        triangles[11] = 5;
        //
        triangles[12] = 1;
        triangles[13] = 2;
        triangles[14] = 5;
        triangles[15] = 1;
        triangles[16] = 5;
        triangles[17] = 6;
        //
        triangles[18] = 0;
        triangles[19] = 7;
        triangles[20] = 4;
        triangles[21] = 0;
        triangles[22] = 4;
        triangles[23] = 3;
        //
        triangles[24] = 5;
        triangles[25] = 4;
        triangles[26] = 7;
        triangles[27] = 5;
        triangles[28] = 7;
        triangles[29] = 6;
        //
        triangles[30] = 0;
        triangles[31] = 6;
        triangles[32] = 7;
        triangles[33] = 0;
        triangles[34] = 1;
        triangles[35] = 6;

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        
        drawingMeshFilter.mesh = mesh;
        drawingRenderer.material.color = Color.green;

        Vector3 lastMousePos = startPos;

        while (true)
        {
            float mixDis = 0.1f;

            float distance = Vector3.Distance(mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), lastMousePos);

            while (distance < mixDis)
            {
                distance = Vector3.Distance(mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)), lastMousePos);
                yield return null;
            }
            
            vertices.AddRange(new Vector3[4]);
            triangles.AddRange(new int[30]);

            int vIndex = vertices.Count - 8;
            int vIndex0 = vIndex + 3;
            int vIndex1 = vIndex + 2;
            int vIndex2 = vIndex + 1;
            int vIndex3 = vIndex + 0;
            
            int vIndex4 = vIndex + 4;
            int vIndex5 = vIndex + 5;
            int vIndex6 = vIndex + 6;
            int vIndex7 = vIndex + 7;
            
            Vector3 currentMousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Vector3 mouseForwardVector = (currentMousePos - lastMousePos).normalized;
            float lineThickness = 0.25f;
            Vector3 topAndRightVertex = currentMousePos - Vector3.Cross(mouseForwardVector, Vector3.back) * lineThickness;
            Vector3 bottomAndRightVertex = currentMousePos - Vector3.Cross(mouseForwardVector, Vector3.forward) * lineThickness;
            Vector3 topAndLeftVertex = new Vector3(topAndRightVertex.x, topAndRightVertex.y, 1);
            Vector3 bottomAndLeftVertex = new Vector3(bottomAndRightVertex.x, bottomAndRightVertex.y, 1);

            vertices[vIndex4] = topAndLeftVertex;
            vertices[vIndex5] = topAndRightVertex;
            vertices[vIndex6] = bottomAndRightVertex;
            vertices[vIndex7] = bottomAndLeftVertex;

            int tIndex = triangles.Count - 30;
            triangles[tIndex + 0] = vIndex2;
            triangles[tIndex + 1] = vIndex3;
            triangles[tIndex + 2] = vIndex4;
            triangles[tIndex + 3] = vIndex2;
            triangles[tIndex + 4] = vIndex4;
            triangles[tIndex + 5] = vIndex5;
            //
            triangles[tIndex + 6] = vIndex1;
            triangles[tIndex + 7] = vIndex2;
            triangles[tIndex + 8] = vIndex5;
            triangles[tIndex + 9] = vIndex1;
            triangles[tIndex + 10] = vIndex5;
            triangles[tIndex + 11] = vIndex6;
            //
            triangles[tIndex + 12] = vIndex0;
            triangles[tIndex + 13] = vIndex7;
            triangles[tIndex + 14] = vIndex4;
            triangles[tIndex + 15] = vIndex0;
            triangles[tIndex + 16] = vIndex4;
            triangles[tIndex + 17] = vIndex3;
            //
            triangles[tIndex + 18] = vIndex5;
            triangles[tIndex + 19] = vIndex4;
            triangles[tIndex + 20] = vIndex7;
            triangles[tIndex + 21] = vIndex0;
            triangles[tIndex + 22] = vIndex4;
            triangles[tIndex + 23] = vIndex3;
            //
            triangles[tIndex + 24] = vIndex0;
            triangles[tIndex + 25] = vIndex6;
            triangles[tIndex + 26] = vIndex7;
            triangles[tIndex + 27] = vIndex0;
            triangles[tIndex + 28] = vIndex1;
            triangles[tIndex + 29] = vIndex6;

            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();

            lastMousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            
            yield return null;
        }
    }

    public Transform _player;
    private void EditTheDrawing()
    {
        if (_currentDrawing == null) return;
        
        _currentDrawing.transform.position = Vector3.zero;
        var rig =  _currentDrawing.AddComponent<Rigidbody>();
        var col = _currentDrawing.AddComponent<MeshCollider>();
        var joint = _currentDrawing.AddComponent<FixedJoint>();
        joint.connectedBody = _player.GetComponent<Rigidbody>();
        _player.GetComponent<FixedJoint>().connectedBody = rig;
        col.convex = true;
        rig.drag = 15f;
        _player.GetComponent<Rigidbody>().isKinematic = false;
        //_currentDrawing.transform.SetParent(_player);
    }

    private void ResetPlayer()
    {
        _player.GetComponent<Rigidbody>().isKinematic = true;
        _player.position = new Vector3(0,5f,0);
        _player.rotation = Quaternion.identity;
        Destroy(_currentDrawing.gameObject);
    }
}
