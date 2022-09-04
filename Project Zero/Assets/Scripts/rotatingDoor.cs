using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatingDoor : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] private float speed = 1f;

    [Header("Rotation Configs")]

    [SerializeField] private float rotationAmount = 90f;

    [SerializeField] private float forwardDirection = 0;

    private Vector3 StartRotation;
    private Vector3 Forward;

    private Coroutine animationCoroutine;

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;

        //Chose a direction relative to the door frame that serves as forward;

        Forward = transform.forward;
    }

    public void Open(Vector3 UserPosition)
    {
        if (!isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            float dot = Vector3.Dot(Forward,(UserPosition - transform.position).normalized);
            Debug.Log($"Dot:{dot.ToString("N3")}");
            animationCoroutine = StartCoroutine(DoRotationOpen(dot));
        }
    }

    private IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (forwardAmount >= forwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(0, startRotation.y - rotationAmount, 0));
        }

        else
        {
            endRotation = Quaternion.Euler(new Vector3(0,startRotation.y + rotationAmount, 0));
        }

        isOpen = true;

        float time = 0;
        while (time < 1)
        {
            transform.rotation =  Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }

    public void Close()
    {
        if (isOpen)
        {
            if (animationCoroutine != null)
            {
                StopCoroutine(animationCoroutine);
            }

            animationCoroutine = StartCoroutine(DoRotationClose());
        }
    }

    private IEnumerator DoRotationClose()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        isOpen = false;

        float time = 0;
         while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
