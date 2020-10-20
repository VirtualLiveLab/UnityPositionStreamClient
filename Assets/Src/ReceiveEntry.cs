using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StreamServer;
using UnityEngine;
using UnityEngine.Serialization;

public class ReceiveEntry : MonoBehaviour
{
    private InputLoop input;
    private StatusCheckLoop _statusCheckLoop;
    private readonly List<CancellationTokenSource> _cancellationTokenSources = new List<CancellationTokenSource>();

    [FormerlySerializedAs("streamClientSetting")] [SerializeField] private UdpSocketHolder udpSocketHolder;
    [FormerlySerializedAs("modelManager")] [SerializeField] private DataHolder dataHolder;
    
    private Task delay = Task.Delay(100);
    
    async Task OnEnable()
    {
        dataHolder.Initialize();
        await delay;
        input = new InputLoop(udpSocketHolder.UdpClient, dataHolder, 5);
        input.Start();
        _statusCheckLoop = new StatusCheckLoop(dataHolder, 5000);
        _statusCheckLoop.Run();
        _cancellationTokenSources.Add(_statusCheckLoop.Cts);
    }

    private async void OnDisable()
    {
        await delay;
        input.Stop();
        await Task.Delay(10);
        udpSocketHolder.TryClose();
        foreach (var cts in _cancellationTokenSources)
        {
            cts.Cancel();
        }
    }
}
