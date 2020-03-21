//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace UAOOI.ProcessObserver.RealTime.Processes
{
  /// <summary>
  /// Define error numbers
  /// </summary>
  /// <remarks>
  /// EventID = Error + ErrorCause*1000
  /// </remarks>
  [Obsolete(@"It conflicts with the UAOOI.ProcessObserver.RealTime.Processes.Error")]
  public enum ErrorCore : int
  {
    //ApplicationLayer_NULL_protocol = 100, //RTLib
    //ApplicationLayer_MBUS_PRIVATE_MBUS_message = 200, //RTLib
    //ApplicationLayer_SBUS_PRIVATE_SBUS_message = 250, //SBUS_PLUGIN
    /// <summary>
    /// ApplicationLayer_SBUS_PRIVATE_SBUSbase_message
    /// </summary>
    ApplicationLayer_SBUS_PRIVATE_SBUSbase_message = 260, //SBUS_PLUGIN

    //ApplicationLayer_SBUS_PRIVATE_SBUSnet_message = 270, //SBUS_PLUGIN
    //CommunicationLayer_UMessage = 300, //RTLib
    //BaseStation_Segment = 400, //Commserver
    //ApplicationLayer_MBUS_MBUS_ApplicationLayerMaster = 500, //RTLib
    //ApplicationLayer_SBUS_SBUS_ApplicationLayerMaster = 550, //RTLib
    /// <summary>
    /// ApplicationLayer_SBUS_SBUS_ApplicationLayerSlave
    /// </summary>
    ApplicationLayer_SBUS_SBUS_ApplicationLayerSlave = 560, //RTLib

    //ApplicationLayer_SBUS_SBUS_ApplicationLayerCommon = 570, //RTLib
    //Processes_IEnvelope = 600, //RTLib
    //BaseStation_DataQueue = 700, //Commserver
    //BaseStation_BaseStation_Management = 800,  //Commserver

    ////RTLib
    //Vacat = 10000,
    //RTLib_AppConfigManagement = 11000,
    //ApplicationLayer_InterfaceNotImplementedException = 12000,
    //RTLib_AppAdditionalInfos = 13000,
    //// CommClient
    //CommClient_BaseStation_Initialization = 20000,
    //CommClient_OPC_Interface = 20100,
    //// CommServer
    //CommServer_OPC_Interface = 30000,
    //NetworkConfig_ApplicationProtocol = 30100,
    //CommServer_Configuration = 30200,
    /// <summary>
    /// CommServer_EC2EC3_symulator
    /// </summary>
    CommServer_EC2EC3_symulator = 30200,

    //CommServer_CommServerComponent = 30300,
    ////opc_da_netserver
    //CAS_OpcSvr_Da_NETServer_Server = 50000,
    //CAS_OpcSvr_Da_NETServer_DaServerBUSSniffer = 50001,
    //CAS_OpcSvr_Da_NETServer_Subscription = 50100,
    ////devicesymulator'
    //CAS_OpcSvr_Da_NETServer_Initialization = 55000,
    ////CommunicationLayer
    //CommunicationLayer = 60000,
    //CommunicationLayer_Net_to_Serial = 60100
  }

}