using System.Collections.Generic;
using System.Threading.Tasks;
using StreamServer;
using StreamServer.Model;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = StreamServer.Model.Vector3;
using Vector4 = StreamServer.Model.Vector4;

public class SendEntry : MonoBehaviour
{
    [SerializeField] private string userId;
    [FormerlySerializedAs("streamClientSetting")] [SerializeField] private UdpSocketHolder udpSocketHolder;
    private SyncOutputLoop output;
    private Task delay = Task.Delay(100);
    async Task OnEnable()
    {
        await delay;
        output = new SyncOutputLoop(udpSocketHolder.UdpClient,udpSocketHolder.RemoteEndPoint , userId);
        output.Start();
        output.TransformList.Add(transform);
    }

    private async void OnDisable()
    {
        await delay;
        await Task.Delay(10);
        udpSocketHolder.TryClose();
    }

    private async Task Update()
    {
        await delay;
        var position = transform.localPosition;
        var rotation = transform.rotation;
        var buff = Utility.PacketsToBuffer(new List<MinimumAvatarPacket>{new MinimumAvatarPacket(
            userId,
            new Vector3(position.x, position.y, position.z),
            rotation.eulerAngles.y,
            new Vector4(
                rotation.x,
                rotation.y,
                rotation.z,
                rotation.w))});
        output.Send(buff);
    }
}
