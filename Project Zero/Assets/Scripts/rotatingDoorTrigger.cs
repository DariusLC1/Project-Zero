using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingDoorTrigger : MonoBehaviour
{
    [SerializeField] private rotatingDoor Door;

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
