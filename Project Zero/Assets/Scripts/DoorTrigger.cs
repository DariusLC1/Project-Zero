using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Door Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {

            if (!Door.isOpen)
            {
                Door.Open(other.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {

            if (Door.isOpen)
            {
                Door.Close();
            }
        }
    }
}
