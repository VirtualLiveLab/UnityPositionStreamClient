using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using StreamServer;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class IpUserUI : MonoBehaviour
{
    [SerializeField] private DataHolder _dataHolder;
    [SerializeField] private UdpSocketHolder _socketHolder;
    [SerializeField] private Button _button;
    [SerializeField] private InputField ipAddress;
    [SerializeField] private InputField port;
    [SerializeField] private InputField userId;
    void Start()
    {
        Random r1 = new System.Random();
        var initialUserName = "User" + r1.Next(0, 1000);
        userId.text = initialUserName;
        _dataHolder.selfId = initialUserName;
        Console.WriteLine(r1.Next(0, 1000));
        _button.onClick.AddListener(() =>
        {
            _socketHolder.RemoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress.text), Int32.Parse(port.text));
            _dataHolder.selfId = userId.text;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
