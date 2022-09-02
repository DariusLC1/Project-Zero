using UnityEngine;

public class EnemyDoorTrigger : MonoBehaviour
{
    [SerializeField] private EnemyDoor Door;

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        {

            if (!Door.isOpen && gameManager.instance.doorEnemyCount == 0)
            {
                Door.Open(other.transform.position);
            }

            else if (Door.isOpen && gameManager.instance.doorEnemyCount != 0)
            {
                Door.Close();
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.TryGetComponent<CharacterController>(out CharacterController controller))
    //    {

    //        if (!Door.isOpen)
    //        {
    //            Door.Open(other.transform.position);
    //        }
    //    }
    //}

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
