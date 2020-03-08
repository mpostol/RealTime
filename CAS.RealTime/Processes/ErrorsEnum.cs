//___________________________________________________________________________________
//
//  Copyright (C) 2020, Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community at GITTER: https://gitter.im/mpostol/OPC-UA-OOI
//___________________________________________________________________________________

using System;

namespace CAS.Lib.RTLib.Processes
{

  /// <summary>
  /// Enum that gives number for error in trace messages
  /// </summary>
  /// <remarks>
  /// EventID = Error + ErrorCause*1000
  /// </remarks>
  [Obsolete("Trace related error must be defined in context of application")]
  public enum Error: int
  {
    /// <summary>
    /// error in ApplicationLayer Null protocol
    /// </summary>
    ApplicationLayer_NULL_protocol = 100, //RTLib
    /// <summary>
    /// error in MBUS message
    /// </summary>
    ApplicationLayer_MBUS_PRIVATE_MBUS_message = 200, //MBUS_PLUGIN
    /// <summary>
    /// error in SBUS message
    /// </summary>
    ApplicationLayer_SBUS_PRIVATE_SBUS_message = 250, //SBUS_PLUGIN
    /// <summary>
    /// error in SBUS base message (common for RS and NET)
    /// </summary>
    ApplicationLayer_SBUS_PRIVATE_SBUSbase_message = 260, //SBUS_PLUGIN
    /// <summary>
    /// error in SBUS net message
    /// </summary>
    ApplicationLayer_SBUS_PRIVATE_SBUSnet_message = 270, //SBUS_PLUGIN
    /// <summary>
    /// error in umessage
    /// </summary>
    CommunicationLayer_UMessage = 300, //RTLib
    /// <summary>
    /// error in envelope
    /// </summary>
    ApplicationLayer_SBUS_SBUS_ApplicationLayerSlave = 560, //RTLib
    /// <summary>
    /// The processes i envelope
    /// </summary>
    Processes_IEnvelope = 600, //RTLib
    /// <summary>
    /// error in DataQueue
    /// </summary>
    BaseStation_DataQueue = 700, //Commserver
    /// <summary>
    /// Error in BaseStattion Management
    /// </summary>
    BaseStation_BaseStation_Management = 800,  //Commserver

    //RTLib
    /// <summary>
    /// Reserved for future use
    /// </summary>
    Vacat = 10000,
    /// <summary>
    /// Error in RTLib: Application configuration management
    /// </summary>
    RTLib_AppConfigManagement = 11000,
    /// <summary>
    ///Error in  RTLib: Application Layer ApplicationLayer_InterfaceNotImplementedException
    /// </summary>
    ApplicationLayer_InterfaceNotImplementedException = 12000,
    /// <summary>
    /// Error in RTLib: Application additional information 
    /// </summary>
    RTLib_AppAdditionalInfos = 13000,
    // CommClient
    /// <summary>
    /// Error in CommClient_BaseStation_Initialization
    /// </summary>
    CommClient_BaseStation_Initialization = 20000,
    /// <summary>
    /// Error in CommClient_OPC_Interface
    /// </summary>
    CommClient_OPC_Interface = 20100,
    // CommServer
    /// <summary>
    /// Error in CommServer_OPC_Interface
    /// </summary>
    CommServer_OPC_Interface = 30000,
    /// <summary>
    /// Error in NetworkConfig_ApplicationProtocol
    /// </summary>
    NetworkConfig_ApplicationProtocol = 30100,
    /// <summary>
    /// Error in CommServer_Configuration
    /// </summary>
    CommServer_Configuration = 30200,
    /// <summary>
    /// Error in CommServer_EC2EC3_symulator
    /// </summary>
    CommServer_EC2EC3_symulator = 30200,
    /// <summary>
    /// Error in CommServer_CommServerComponent
    /// </summary>
    CommServer_CommServerComponent = 30300,
    // DataPorter
    /// <summary>
    /// Error in DataPorter_OPC_Interface
    /// </summary>
    DataPorter_OPC_Interface = 40100,
    /// <summary>
    /// Error in DataPorter_ApplicationConfiguration
    /// </summary>
    DataPorter_ApplicationConfiguration = 40200,
    /// <summary>
    /// Error in DataPorter_Transaction_OperationDBThread
    /// </summary>
    DataPorter_Transaction_OperationDBThread = 40300,
    /// <summary>
    ///  Error in DataPorter_OPCBufferedDataAccess
    /// </summary>
    DataPorter_OPCBufferedDataAccess = 40400,
    /// <summary>
    /// Error in DataPorter_OPCRealtimeDataAccess
    /// </summary>
    DataPorter_OPCRealtimeDataAccess = 40500,
    /// <summary>
    /// Error in DataPorter_OPCDataQueue
    /// </summary>
    DataPorter_OPCDataQueue = 40600,
    /// <summary>
    /// Error in DataPorter_gui
    /// </summary>
    DataPorter_gui = 40700,
    /// <summary>
    /// Error in DataPorter_Servers
    /// </summary>
    DataPorter_BufferedAccessTransaction = 40800,
    /// <summary>
    /// Error in DataPorter_Servers
    /// </summary>
    DataPorter_Servers = 40900,
    //opc_da_netserver
    /// <summary>
    /// Error in CAS_OpcSvr_Da_NETServer_Server
    /// </summary>
    CAS_OpcSvr_Da_NETServer_Server = 50000,
    /// <summary>
    /// Error in CAS_OpcSvr_Da_NETServer_DaServerBUSSniffer
    /// </summary>
    CAS_OpcSvr_Da_NETServer_DaServerBUSSniffer = 50001,
    /// <summary>
    /// Error in CAS_OpcSvr_Da_NETServer_Subscription
    /// </summary>
    CAS_OpcSvr_Da_NETServer_Subscription = 50100,
    /// <summary>
    /// Error in CAS_OpcSvr_Da_NETServer_Initialization
    /// </summary>
    CAS_OpcSvr_Da_NETServer_Initialization = 55000,
    //CommunicationLayer
    /// <summary>
    /// Error in CommunicationLayer
    /// </summary>
    CommunicationLayer = 60000,
    /// <summary>
    /// Error in CommunicationLayer_Net_to_Serial
    /// </summary>
    CommunicationLayer_Net_to_Serial = 60100
  }
  /// <summary>
  /// mask for error cause
  /// </summary>
  public enum ErrorCauseMask: int
  {
    /// <summary>
    /// CommClient_BaseStation_Initialization
    /// </summary>
    Processes_EventLogMonitor_WriteEntry = 0000,
    /// <summary>
    /// Processes_Assertion_Assert
    /// </summary>
    Processes_Assertion_Assert = 1000,
    /// <summary>
    /// Processes_MonitoredThread
    /// </summary>
    Processes_MonitoredThread = 2000,
  }
}