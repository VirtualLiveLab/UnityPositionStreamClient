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
    [SerializeField] private ModelManager modelManager;
    
    private Task delay = Task.Delay(100);
    
    async Task OnEnable()
    {
        modelManager.Initialize();
        await delay;
        input = new InputLoop(udpSocketHolder.UdpClient, modelManager, 5);
        input.Start();
        _statusCheckLoop = new StatusCheckLoop(modelManager, 1000);
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
