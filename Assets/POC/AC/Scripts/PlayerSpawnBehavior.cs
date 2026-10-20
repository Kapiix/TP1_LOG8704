using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace ThroughHellAndFire.POC
{
    public class PlayerSpawnBehavior : NetworkBehaviour
    {
        [SerializeField] GameObject myXRCam;
        [SerializeField] InputActionManager myInputAction;

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
                myInputAction.enabled = false;
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
