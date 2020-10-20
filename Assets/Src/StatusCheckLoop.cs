﻿using System;
using System.Threading.Tasks;
using EventServerCore;
using LoopLibrary;

namespace StreamServer
{
    public class StatusCheckLoop : BaseLoop<Unit>
    {
        private ModelManager _modelManager;
        public StatusCheckLoop(ModelManager modelManager, int interval, string name = "Input")
            : base(interval, name)
        {
            _modelManager = modelManager;
        }

        protected override async Task Update(int count)
        {
            Utility.PrintDbg($"Num clients: {_modelManager.Users.Count}");
        }
    }
}