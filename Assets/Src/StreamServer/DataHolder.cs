﻿using System;
using System.Collections.Concurrent;
using StreamServer.Model;
using UnityEngine;
 using UnityEngine.Serialization;

 namespace StreamServer
{
    [CreateAssetMenu]
    public class DataHolder : ScriptableObject
    {
        [NonSerialized]
        public ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();

        [FormerlySerializedAs("selfName")] public string selfId = "";

        public void Initialize()
        {
            Users = new ConcurrentDictionary<string, User>();
        }
    }
}
