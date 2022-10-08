using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AIFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        AIFOV fov = (AIFOV)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);
        Vector3 ViewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 ViewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + ViewAngleA * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + ViewAngleB * fov.viewRadius);
    }
}
