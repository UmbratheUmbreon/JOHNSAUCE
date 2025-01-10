using System;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

public class CorrectCeilingFloors : MonoBehaviour
{
    private void Start()
    {
        foreach(MeshFilter mf in FindObjectsOfType<MeshFilter>())
        {
            MeshRenderer mr = mf.gameObject.GetComponent<MeshRenderer>();
            #if UNITY_EDITOR
            //Debug.Log(mf.name);
            string mfn = mf.sharedMesh.name;
            #else
            string mfn = mf.mesh.name;
            #endif
            if (mr == null || !(mfn.Contains("Plane") || mfn.Contains("Quad")))
            {
                continue;
            }
            #if UNITY_EDITOR
            string mtn = mr.sharedMaterial.name;
            #else
            string mtn = mr.material.name;
            #endif
            if (mtn.Contains("CafeCeiling") || !(mtn.Contains("TileFloor") || mtn.Contains("Carpet") || mtn.Contains("Ceiling") || mtn.Contains("Grass")))
            {
                continue;
            }
            Transform mt = mf.transform;
            #if UNITY_EDITOR
            Undo.RecordObject(mt, "Change Transform Scale/Rotation");
            #endif
            if (mfn.Contains("Plane"))
            {
                mt.localScale = new Vector3(1f, 1f, 1f);
                if (mtn.Contains("TileFloor") || mtn.Contains("Carpet") || mtn.Contains("Grass"))
                {
                    mt.eulerAngles = new Vector3(0f, 180f, 0f);
                }
                else
                {
                    mt.eulerAngles = new Vector3(180f, 180f, 0f);
                }
            }
            else
            {
                mt.localScale = new Vector3(10f, 10f, 1f);
                if (mtn.Contains("TileFloor") || mtn.Contains("Carpet") || mtn.Contains("Grass"))
                {
                    mt.eulerAngles = new Vector3(90f, 0f, 0f);
                }
                else
                {
                    mt.eulerAngles = new Vector3(-90f, 0f, 0f);
                }
            }
        }
    }
}