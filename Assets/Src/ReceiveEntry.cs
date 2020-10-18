using System.Threading.Tasks;
using StreamServer;
using UnityEngine;
using UnityEngine.Serialization;

public class ReceiveEntry : MonoBehaviour
{
    private InputLoop input;

    [FormerlySerializedAs("streamClientSetting")] [SerializeField] private UdpSocketHolder udpSocketHolder;
    [SerializeField] private ModelManager modelManager;
    void OnEnable()
    {
        input = new InputLoop(udpSocketHolder.UdpClient, modelManager, 5);
        input.Start();
    }

    private async void OnDisable()
    {
        input.Stop();
        await Task.Delay(10);
        udpSocketHolder.TryClose();
    }
}
