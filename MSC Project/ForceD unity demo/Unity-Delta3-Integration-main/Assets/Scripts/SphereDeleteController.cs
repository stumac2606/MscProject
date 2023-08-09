/*using UnityEngine;

public class SphereDeletionController : MonoBehaviour
{
    public Vector3 InitialPosition { get; set; }
    public Vector3 TargetPosition { get; set; }
    public NonLinearMovement ParentScript { get; set; }

    private void Update()
    {
        if (ParentScript != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, TargetPosition);
            if (distanceToTarget < 0.1f) // Adjust this threshold as needed
            {
                // Delete this sphere
                Destroy(gameObject);

                // Delete all force spheres
                foreach (GameObject forceSphere in ParentScript.ForceSpheres)
                {
                    Destroy(forceSphere);
                }

                // Clear the list of force spheres
                ParentScript.ForceSpheres.Clear();
            }
        }
        
    }
}
*/