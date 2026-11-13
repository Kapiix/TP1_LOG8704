using Unity.Netcode;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace ThroughHellAndFire.POC
{
    public class PlayerSpawnBehavior : NetworkBehaviour
    {
        [SerializeField] GameObject myXRCam;
        [SerializeField] InputActionManager myInputAction;
        [SerializeField] GameObject locomotion;

        void Awake()
        {

        }
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            Debug.Log($"Player {OwnerClientId}: " + $"IsOwner={IsOwner}, " + $"IsHost={IsHost}, " + $"IsServer={IsServer}");
            if (!IsOwner)
            {
                myXRCam.GetComponent<Camera>().enabled = false;
                myXRCam.GetComponent<AudioListener>().enabled = false;
                myXRCam.GetComponent<TrackedPoseDriver>().enabled = false;
                //myInputAction.enabled = false;
                var tpd = GetComponentsInChildren<TrackedPoseDriver>(true);
                foreach (var item in tpd)
                {
                    item.enabled = false;
                }
            }
            if (IsOwner)
            {
                GetComponent<CharacterController>().enabled = true;
                myInputAction.enabled = true;
                myInputAction.GetComponent<XROrigin>().enabled = true;
                locomotion.SetActive(true);
            }
        }

        public override void OnNetworkDespawn()
        {
            //ClearBleed();
            base.OnNetworkDespawn();
        }

        //private void FixedUpdate()
        //{
        //    if (IsOwner)
        //    {
        //        this.transform.position = myXRCam.transform.position;
        //    }
        //}
    }
}
