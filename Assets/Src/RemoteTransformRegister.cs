using System.Collections.Generic;
using StreamServer.Model;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace StreamServer
{
    public class RemoteTransformRegister : MonoBehaviour
    {
        [SerializeField] private string userId;
        [SerializeField] private ModelManager modelManager;

        private void Start()
        {
        }

        private void Update()
        {
            try
            {
                var packet = modelManager.Users[userId].Data.CurrentMinimumAvatarPacket;
                if (packet != null)
                {
                    var pos = new Vector3(
                        packet.Position.X,
                        packet.Position.Y,
                        packet.Position.Z);
                    var rot = new Quaternion(
                        packet.NeckRotation.X,
                        packet.NeckRotation.Y,
                        packet.NeckRotation.Z,
                        packet.NeckRotation.W);
                    transform.localPosition = pos;
                    transform.rotation = rot;
                }
            }
            catch (KeyNotFoundException e)
            {
                //Debug.Log("Key not found");
            }
        }
    }
}
