﻿using DevicesSyncUnity;
using DevicesSyncUnity.Messages;
using UnityEngine;
using UnityEngine.Networking;

namespace NormandErwan.MasterThesis.Experiment.DeviceControllers
{
  public class DeviceControllerSync : DevicesSync
  {
    // Editor Fields

    [SerializeField]
    private DeviceController deviceController;

    // Properties

    public DeviceController DeviceController { get { return deviceController; } set { deviceController = value; } }
    protected override int DefaultChannelId { get { return Channels.DefaultReliable; } }

    // Variables

    protected DeviceControllerSyncMessage deviceControllerMessage;

    // Methods

    protected override void Awake()
    {
      base.Awake();

      deviceControllerMessage = new DeviceControllerSyncMessage(DeviceController, () => 
      {
        SendToServer(deviceControllerMessage, Channels.DefaultReliable);
      });
      MessageTypes.Add(deviceControllerMessage.MessageType);
    }

    protected override DevicesSyncMessage OnServerMessageReceived(NetworkMessage netMessage)
    {
      return ProcessReceivedMessage(netMessage, false);
    }

    protected override DevicesSyncMessage OnClientMessageReceived(NetworkMessage netMessage)
    {
      return ProcessReceivedMessage(netMessage, true);
    }

    protected virtual DevicesSyncMessage ProcessReceivedMessage(NetworkMessage netMessage, bool onClient)
    {
      if (!onClient || (onClient && !isServer))
      {
        DeviceControllerSyncMessage deviceControllerReceived;
        if (TryReadMessage(netMessage, deviceControllerMessage.MessageType, out deviceControllerReceived))
        {
          deviceControllerReceived.Sync(DeviceController);
          return deviceControllerReceived;
        }
      }
      return null;
    }
  }
}
