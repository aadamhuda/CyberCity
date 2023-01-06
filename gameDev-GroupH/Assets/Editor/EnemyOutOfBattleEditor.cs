using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FirstEnemy))]

// Editor file to see sight of enemy
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FirstEnemy fov = (FirstEnemy)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.GetRadius()) ;

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.GetAmgle() / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.GetAmgle() / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.GetRadius()) ;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.GetRadius()) ;

        if (fov.GetInRange())
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.GetPlayer().transform.position) ;
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}