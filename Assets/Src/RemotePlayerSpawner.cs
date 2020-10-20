using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StreamServer.Model;
using UnityEngine;

namespace StreamServer
{
    [CreateAssetMenu]
    public class RemotePlayerSpawner : ScriptableObject
    {
        [SerializeField] private GameObject remotePlayerPrefab;
        [NonSerialized] private List<GameObject> _remotePlayers = new List<GameObject>();
        [NonSerialized] public TaskScheduler TaskScheduler;

        public async Task Spawn(MinimumAvatarPacket packet)
        {
            var taskFactory = new TaskFactory();
            await taskFactory.StartNew(() =>
            {
                try
                {
                    remotePlayerPrefab.GetComponent<RemoteTransformRegister>().userId = packet.PaketId;
                    remotePlayerPrefab.GetComponent<ChangeName>().Change(packet.PaketId);
                    _remotePlayers.Add(Instantiate(remotePlayerPrefab));
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler);
        }

        public async Task Remove(string userId)
        {
            var taskFactory = new TaskFactory();
            await taskFactory.StartNew(() =>
            {
                try
                {
                    var toRemove = _remotePlayers.Find(x => x.GetComponent<RemoteTransformRegister>().userId == userId);
                    Destroy(toRemove);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler);
            
        }
    }
}
