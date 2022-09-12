using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour
{

    [SerializeField] private playerController playerController;
    private LineRenderer lr;
    [SerializeField] private Transform startPoint;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();

        playerController = GameObject.Find("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        lr.SetPosition(0, startPoint.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.right, out hit))
        {
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }

            if (hit.transform.tag == "Player")
            {
                playerController.death();
            }

        }
        else lr.SetPosition(1, -transform.right * 5000);
    }
}

