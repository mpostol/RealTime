using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using System.Text;

namespace Utils
{
	/// <summary>
	/// HTTPServer is an abstract class - that provides http server.
	/// </summary>
	public abstract class HTTPServer
	{
    #region PRIVATE
    #region static
    /// <summary>
    /// This function send the Header Information to the client (Browser)
    /// </summary>
    /// <param name="sHttpVersion">HTTP Version</param>
    /// <param name="sMIMEHeader">Mime Type</param>
    /// <param name="iTotBytes">Total Bytes to be sent in the body</param>
    /// <param name="mySocket">Socket reference</param>
    /// <returns></returns>
    private static void SendHeader(string sHttpVersion, string sMIMEHeader, int iTotBytes, string sStatusCode, ref Socket mySocket)
    {

      String sBuffer = "";
			
      // if Mime type is not provided set default to text/html
      if (sMIMEHeader.Length == 0 )
      {
        sMIMEHeader = "text/html";  // Default Mime Type is text/html
      }

      sBuffer = sBuffer + sHttpVersion + sStatusCode + "\r\n";
      sBuffer = sBuffer + "Server: cx1193719-b\r\n";
      sBuffer = sBuffer + "Content-Type: " + sMIMEHeader + "\r\n";
      sBuffer = sBuffer + "Accept-Ranges: bytes\r\n";
      sBuffer = sBuffer + "Content-Length: " + iTotBytes + "\r\n\r\n";
			
      Byte[] bSendData = Encoding.ASCII.GetBytes(sBuffer); 

      SendToBrowser( bSendData, ref mySocket);

      Logger("Total Bytes : " + iTotBytes.ToString(),false);

    }
    /// <summary>
    /// Overloaded Function, takes string, convert to bytes and calls 
    /// overloaded sendToBrowserFunction.
    /// </summary>
    /// <param name="sData">The data to be sent to the browser(client)</param>
    /// <param name="mySocket">Socket reference</param>
    private static  void SendToBrowser(String sData, ref Socket mySocket)
    {
      SendToBrowser (Encoding.ASCII.GetBytes(sData), ref mySocket);
    }
    /// <summary>
    /// Sends data to the browser (client)
    /// </summary>
    /// <param name="bSendData">Byte Array</param>
    /// <param name="mySocket">Socket reference</param>
    private static  void SendToBrowser(Byte[] bSendData, ref Socket mySocket)
    {
      int numBytes = 0;
			
      try
      {
        if (mySocket.Connected)
        {
          if (( numBytes = mySocket.Send(bSendData, bSendData.Length,0)) == -1)
            Logger("Socket Error cannot Send Packet",false);
          else
          {
            #if DEBUG
            Logger("No. of bytes send {0}" + numBytes.ToString(),false);
            #endif
          }
        }
        else
          Logger("HTTP Connection Dropped....",false);
      }
      catch (Exception  e)
      {
        Logger("Error Occurred : {0} "+ e.ToString(),true );
      }
    }
    #endregion
    internal static void Logger(string logstring,bool stop)
    {
      //MZTD:wpis do logow potrzebny -DONE
      //Console.WriteLine(logstring);
      //if(stop) Console.ReadLine();
			System.Diagnostics.EventLogEntryType logtype;
			if(stop)logtype= System.Diagnostics.EventLogEntryType.Error;else logtype=System.Diagnostics.EventLogEntryType.Information;
			new Processes.EventLogMonitor(logstring, logtype, 
				(int)Processes.Error.RTLib_AppAdditionalInfos, 98).WriteEntry();
    }
    private int listener_port;
    private Thread ListenerThread;
    private TcpListener myListener;
    /// <summary>
    /// This method Accepts new connection and
    /// First it receives the welcome massage from the client,
    /// Then it sends the Current date time to the Client.
    /// </summary>
    private void ListenerThreadBody()
    {
      int iStartPos = 0;
      String sRequest;
      String sDirName;
      String sRequestedFile;
      while(true)
      {
        #region MAIN LOOP
        //Accept a new connection
        Socket mySocket = myListener.AcceptSocket() ;
#if DEBUG
        Logger("Socket Type " + 	mySocket.SocketType ,false); 
#endif
        if(mySocket.Connected)
        {
          #region if(mySocket.Connected)
#if DEBUG
          Logger("\nClient Connected!!\n==================\nCLient IP {0}\n"+mySocket.RemoteEndPoint.ToString(),false) ;
#endif
          #region reading client request
          //make a byte array and receive data from the client 
          Byte[] bReceive = new Byte[1024] ;
          int i = mySocket.Receive(bReceive,bReceive.Length,0) ;
          //Convert Byte to String
          string sBuffer = Encoding.ASCII.GetString(bReceive);
          #endregion getting client request
          #region data analysis
          //At present we will only deal with GET type
          if (sBuffer.Substring(0,3) != "GET" )
          {
            Console.WriteLine("Only Get Method is supported..");
            mySocket.Close();
            continue;
          }
          // Look for HTTP request
          iStartPos = sBuffer.IndexOf("HTTP",1);
          // Get the HTTP text and version e.g. it will return "HTTP/1.1"
          string sHttpVersion = sBuffer.Substring(iStartPos,8);
          // Extract the Requested Type and Requested file/directory
          sRequest = sBuffer.Substring(0,iStartPos - 1);
          //Replace backslash with Forward Slash, if Any
          sRequest.Replace("\\","/");
          //If file name is not supplied add forward slash to indicate 
          //that it is a directory and then we will look for the 
          //default file name..
          if ((sRequest.IndexOf(".") <1) && (!sRequest.EndsWith("/")))
          {
            sRequest = sRequest + "/"; 
          }
          //Extract the requested file name
          iStartPos = sRequest.LastIndexOf("/") + 1;
          sRequestedFile = sRequest.Substring(iStartPos);
          //Extract The directory Name
          sDirName = sRequest.Substring(sRequest.IndexOf("/"), sRequest.LastIndexOf("/")-3);
          //extract parameters from file name
          string []parameters=sRequestedFile.Split('?');
          sRequestedFile=parameters[0];
          Hashtable paramtable=null;
          if(parameters.Length>1)
          {
            string []parameters2=parameters[1].Split('&');
            //build of parameters hash table
            paramtable=new Hashtable();
            foreach(string par in parameters2)
            {
              if(par.Split('=').Length>1) paramtable.Add(par.Split('=')[0],par.Split('=')[1]);
              else paramtable.Add(par.Split('=')[0],"");
            }
          }
          #endregion  data analysis
          string SendMessage=this.GetStringData(sDirName, sRequestedFile,paramtable);
          SendHeader(sHttpVersion,  "", SendMessage.Length, " 200 OK", ref mySocket);
          SendToBrowser ( SendMessage, ref mySocket);
          mySocket.Close();						
        }
        #endregion if(mySocket.Connected)
        #endregion MAIN LOOP
      }
    }
    private void InitServer (int port)
    {
      listener_port=port;
      try
      {
        //start listing on the given port
        myListener = new TcpListener(IPAddress.Any,listener_port) ;
        myListener.Start();
        Logger("Web Server Running ("+IPAddress.Any.ToString()+")... ",false);

      }
      catch(Exception e)
      {
        Logger("An Exception Occurred while Listening :" +e.ToString(),true);
      }
    }
    #endregion PRIVATE
    #region PUBLIC
    /// <summary>
    /// function that should be overriden to provide string with response for html data request
    /// </summary>
    /// <param name="directory">directory name</param>
    /// <param name="filename">filename that client requests</param>
    /// <param name="parameters">parameters that client send with GET request</param>
    /// <returns></returns>
    protected abstract string GetStringData(string directory, string filename, Hashtable parameters);
    /// <summary>
    /// Server Initiation
    /// </summary>
    /// <param name="port">tcp port that should be used for listening</param>
    public HTTPServer(int port)
    {
      InitServer(port);
    }
    /// <summary>
    /// Server Initiation on standart port
    /// </summary>
		public HTTPServer()
		{
      InitServer(80);
    }
    /// <summary>
    /// Starts the HTTP Server
    /// </summary>
    public void Start()
    {
      //start the thread which calls the method 'ListenerThreadBody'
      ListenerThread = Processes.Manager.StartProcess( new ThreadStart(ListenerThreadBody), "HTTPServer", true);
    }
    /// <summary>
    /// Stops the HTTP Server
    /// </summary>
    public void Stop()
    {
      ListenerThread.Abort();
    }
    #endregion PUBLIC
	}
}
