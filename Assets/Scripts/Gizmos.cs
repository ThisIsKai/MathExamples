using UnityEngine;

public class GizmosExample : MonoBehaviour
{
    // This method is called when the object is selected
    void OnDrawGizmosSelected()
    {
        // Set the color for the subsequent Gizmo drawing operations
        Gizmos.color = Color.yellow;
        

        // Draw a wire cube at the GameObject's position with a specific size
        Gizmos.DrawWireCube(transform.position, new Vector3(10, 10, 10));



        // Gizmos.DrawSphere(Vector3 center, float radius)
       //  Gizmos.DrawLine(Vector3 from, Vector3 to)
    }
}