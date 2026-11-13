using UnityEngine;

namespace ThroughHellAndFire.POC
{
    public class scriptBidonCapsuleSuisCam : MonoBehaviour
    {
        [SerializeField] Camera monVRCam;

        void FixedUpdate()
        {
            this.transform.position = monVRCam.transform.position;
            this.transform.rotation = monVRCam.transform.rotation;
        }
    }
}