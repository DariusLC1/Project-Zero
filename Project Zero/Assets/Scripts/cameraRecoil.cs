//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class cameraRecoil : MonoBehaviour
//{
//    public Vector3 currentRotation;
//    public Vector3 targetRotation;

//    [Header("----- HipFire -----")]
//    [SerializeField] public float recoilX;
//    [SerializeField] public float recoilY;
//    [SerializeField] public float recoilZ;

//    [Header("----- Settings -----")]
//    [SerializeField] public float snappy;
//    [SerializeField] public float returnSpeed;

//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
//        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappy * Time.fixedDeltaTime);
//    }

//    public void RecoilFire()
//    {
//        targetRotation = new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
//    }
//}
