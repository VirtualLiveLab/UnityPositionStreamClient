﻿using System;
using System.Collections.Concurrent;
using StreamServer.Model;
using UnityEngine;

namespace StreamServer
{
    [CreateAssetMenu]
    public class DataHolder : ScriptableObject
    {
        [NonSerialized]
        public ConcurrentDictionary<string, User> Users = new ConcurrentDictionary<string, User>();

        public void Initialize()
        {
            Users = new ConcurrentDictionary<string, User>();
        }
    }
}
