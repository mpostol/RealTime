//<summary>
//  Title   : Define error numbers.
//  System  : Microsoft Visual C# .NET 2005
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    MZBRZEZNY - 2007-02-12:
//    Zmieniono niektore wartosci bledow (UWAGA: kody bledow nie moga byc wieksze niz 65535!!)
//    MZbrzezny - 06-08-2004: created
//    <Author> - <date>:
//    <description>
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.com.pl
//  http:\\www.cas.eu
//</summary>

using System;

namespace CAS.Lib.RTLib.Processes
{
  //
  //EventID = Error + ErrorCause*1000
  //
  /// <summary>
  /// Define error numbers
  /// </summary>
  [Obsolete(@"It conflicts with the C:\MPVS\CASTrunk\PR36-CAS_MAIN_CORE_PCKG\RTLib\Processes\ErrorsEnum.cs")]
  public enum Error: int
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
  //public enum ErrorCauseMask: int
  //{
  //  Processes_EventLogMonitor_WriteEntry = 0000,
  //  Processes_Assertion_Assert = 1000,
  //  Processes_MonitoredThread = 2000,
  //}
}