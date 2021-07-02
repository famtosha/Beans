using System.IO;
using PacketManager;
using UnityEngine;

public class DynamicLevelObject : StaticLevelObject
{
    private Timer _updateTimer;

    protected override void Start()
    {
        base.Start();
        _updateTimer = new Timer(Server.dynamicObjectUpdateRate);
    }

    private void Update()
    {
        _updateTimer.UpdateTimer(Time.deltaTime);
        if (_updateTimer.isReady)
        {
            _sol.UpdateSynchronizedObjectData(objectID);
            _updateTimer.Reset();
        }
    }
}
