﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class Cube : MonoBehaviour {
    [Header("輸入每邊有多少格（不是頂點數量）")]
    public int xSize, ySize, zSize;

    private Mesh mesh;
    private Vector3[] vertices;

    private void Awake()
    {
        


    }

    void Generate(){
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Procedural Cube";
        CreateVertices();
        CreateTriangles();
    }

    void CreateVertices(){
        mesh.vertices = vertices;
        int cornerVertices = 8;
        int edgeVertices = 4 * (xSize + ySize + zSize - 3);
        int faceVertices = 2 * ((xSize - 1) * (ySize - 1) +
                                (ySize - 1) * (zSize - 1) +
                                (zSize - 1) * (xSize - 1));

        vertices = new Vector3[cornerVertices + faceVertices + edgeVertices];

        int v = 0;
        for (int y = 0; y <= ySize; y++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                vertices[v++] = new Vector3(x, y, 0);

            }
            for (int z = 1; z <= zSize; z++)
            {
                vertices[v++] = new Vector3(xSize, y, z);
                            }
            for (int x = xSize - 1; x >= 0; x--)
            {
                vertices[v++] = new Vector3(x, y, zSize);
            }
            for (int z = zSize - 1; z > 0; z--)
            {
                vertices[v++] = new Vector3(0, y, z);
            }
        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                vertices[v++] = new Vector3(x, ySize, z);
            }
        }
        for (int z = 1; z < zSize; z++)
        {
            for (int x = 1; x < xSize; x++)
            {
                vertices[v++] = new Vector3(x, 0, z);
            }
        }
    }

    void CreateTriangles(){
        int quad = 2 * ((xSize) * (ySize) +
                       (ySize) * (zSize) +
                       (zSize) * (xSize));

        int[] triangles = new int[quad * 6];
        int ring = (xSize + ySize) * 2;

        // t = iterate index
        // v = the first vertices index of every quad 
        int t = 0, v = 0;

        for (int q = 0; q < ring - 1;q++,v++){
            t = SetQuad(triangles, t, v, v + 1, v + ring, v + ring + 1);
        }

        mesh.triangles = triangles;
    }

    private static int 
    SetQuad(int[] triangles,int i ,int v00,int v01,int v10,int v11){
        triangles[i] = v00;
        triangles[i + 1] = triangles[i + 4] = v01;
        triangles[i + 2] = triangles[i + 3] = v10;
        triangles[i + 5] = v11;
        return i + 6;
    }

    private void OnDrawGizmos()
    {
        if(vertices == null){
            return;
        }

        Gizmos.color = Color.black;

        for (int i = 0; i < vertices.Length;i++){
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }

    }
}
