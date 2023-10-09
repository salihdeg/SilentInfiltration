using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.raidus);


        if (fov.eyePosForRaycast != null)
        {
            Vector3 viewAngle01 = DirectionFromAgnle(fov.eyePosForRaycast.eulerAngles.y, -fov.angle / 2);
            Vector3 viewAngle02 = DirectionFromAgnle(fov.eyePosForRaycast.eulerAngles.y, fov.angle / 2);

            Handles.color = Color.yellow;
            Handles.DrawLine(fov.eyePosForRaycast.position, fov.eyePosForRaycast.position + viewAngle01 * fov.raidus);
            Handles.DrawLine(fov.eyePosForRaycast.position, fov.eyePosForRaycast.position + viewAngle02 * fov.raidus);
        }


        if (fov.canSeePlayer && fov.eyePosForRaycast != null)
        {
            Handles.color = Color.green;

            Handles.DrawLine(fov.eyePosForRaycast.position, fov.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAgnle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
