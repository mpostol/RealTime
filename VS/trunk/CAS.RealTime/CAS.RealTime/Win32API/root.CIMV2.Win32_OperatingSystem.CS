//<summary>
//  Title   : CAS.Lib.RTLib.Win32API
//  System  : Microsoft Visual C# .NET 
//  $LastChangedDate$
//  $Rev$
//  $LastChangedBy$
//  $URL$
//  $Id$
//  History :
//    20080625: mzbrzezny: ForceReboot method is added (another WMI function is called: Windows32Shutdown with ForceReboot Parameters)
//    MPostol - 2003: 
//    - created
//
//  Copyright (C)2006, CAS LODZ POLAND.
//  TEL: +48 (42) 686 25 47
//  mailto:techsupp@cas.eu
//  http://www.cas.eu
//</summary>

#pragma warning disable 1591
//

namespace CAS.Lib.RTLib.Win32API
{
  using System;
  using System.ComponentModel;
  using System.Management;
  using System.Collections;
  using System.Globalization;
  using System.ComponentModel.Design.Serialization;
  using System.Reflection;
  /// <summary>
  /// Functions ShouldSerialize  are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use IsNull function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
  /// Functions IsNull() are used to check if a property is NULL.
  /// Functions Reset  are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
  /// Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
  /// Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
  /// An Early Bound class generated for the WMI class.Win32_OperatingSystem
  /// </summary>
  public class OperatingSystem: System.ComponentModel.Component
  {

    // Private property to hold the WMI namespace in which the class resides.
    private static string CreatedWmiNamespace = "ROOT\\CIMV2";

    // Private property to hold the name of WMI class which created this class.
    private static string CreatedClassName = "Win32_OperatingSystem";

    // Private member variable to hold the ManagementScope which is used by the various methods.
    private static System.Management.ManagementScope statMgmtScope = null;

    private ManagementSystemProperties PrivateSystemProperties;

    // Underlying lateBound WMI object.
    private System.Management.ManagementObject PrivateLateBoundObject;

    // Member variable to store the 'automatic commit' behavior for the class.
    private bool AutoCommitProp = true;

    // Private variable to hold the embedded property representing the instance.
    private System.Management.ManagementBaseObject embeddedObj;

    // The current WMI object used
    private System.Management.ManagementBaseObject curObj;

    // Flag to indicate if the instance is an embedded object.
    private bool isEmbedded = false;

    // Below are different overloads of constructors to initialize an instance of the class with a WMI object.
    public OperatingSystem()
      :
            this( ( (System.Management.ManagementScope)( null ) ), ( (System.Management.ManagementPath)( null ) ), ( (System.Management.ObjectGetOptions)( null ) ) )
    {
    }

    public OperatingSystem( string keyName )
      :
            this( ( (System.Management.ManagementScope)( null ) ), ( (System.Management.ManagementPath)( new System.Management.ManagementPath( OperatingSystem.ConstructPath( keyName ) ) ) ), ( (System.Management.ObjectGetOptions)( null ) ) )
    {
    }

    public OperatingSystem( System.Management.ManagementScope mgmtScope, string keyName )
      :
            this( ( (System.Management.ManagementScope)( mgmtScope ) ), ( (System.Management.ManagementPath)( new System.Management.ManagementPath( OperatingSystem.ConstructPath( keyName ) ) ) ), ( (System.Management.ObjectGetOptions)( null ) ) )
    {
    }

    public OperatingSystem( System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions )
      :
            this( ( (System.Management.ManagementScope)( null ) ), ( (System.Management.ManagementPath)( path ) ), ( (System.Management.ObjectGetOptions)( getOptions ) ) )
    {
    }

    public OperatingSystem( System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path )
      :
            this( ( (System.Management.ManagementScope)( mgmtScope ) ), ( (System.Management.ManagementPath)( path ) ), ( (System.Management.ObjectGetOptions)( null ) ) )
    {
    }

    public OperatingSystem( System.Management.ManagementPath path )
      :
            this( ( (System.Management.ManagementScope)( null ) ), ( (System.Management.ManagementPath)( path ) ), ( (System.Management.ObjectGetOptions)( null ) ) )
    {
    }

    public OperatingSystem( System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions )
    {
      if ( ( path != null ) )
      {
        if ( ( CheckIfProperClass( mgmtScope, path, getOptions ) != true ) )
        {
          throw new System.ArgumentException( "Class name does not match." );
        }
      }
      PrivateLateBoundObject = new System.Management.ManagementObject( mgmtScope, path, getOptions );
      PrivateSystemProperties = new ManagementSystemProperties( PrivateLateBoundObject );
      curObj = PrivateLateBoundObject;
    }

    public OperatingSystem( System.Management.ManagementObject theObject )
    {
      if ( ( CheckIfProperClass( theObject ) == true ) )
      {
        PrivateLateBoundObject = theObject;
        PrivateSystemProperties = new ManagementSystemProperties( PrivateLateBoundObject );
        curObj = PrivateLateBoundObject;
      }
      else
      {
        throw new System.ArgumentException( "Class name does not match." );
      }
    }

    public OperatingSystem( System.Management.ManagementBaseObject theObject )
    {
      if ( ( CheckIfProperClass( theObject ) == true ) )
      {
        embeddedObj = theObject;
        PrivateSystemProperties = new ManagementSystemProperties( theObject );
        curObj = embeddedObj;
        isEmbedded = true;
      }
      else
      {
        throw new System.ArgumentException( "Class name does not match." );
      }
    }

    // Property returns the namespace of the WMI class.
    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string OriginatingNamespace
    {
      get
      {
        return "ROOT\\CIMV2";
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string ManagementClassName
    {
      get
      {
        string strRet = CreatedClassName;
        if ( ( curObj != null ) )
        {
          if ( ( curObj.ClassPath != null ) )
          {
            strRet = ( (string)( curObj[ "__CLASS" ] ) );
            if ( ( ( strRet == null )
                        || ( strRet == System.String.Empty ) ) )
            {
              strRet = CreatedClassName;
            }
          }
        }
        return strRet;
      }
    }

    // Property pointing to an embedded object to get System properties of the WMI object.
    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public ManagementSystemProperties SystemProperties
    {
      get
      {
        return PrivateSystemProperties;
      }
    }

    // Property returning the underlying lateBound object.
    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public System.Management.ManagementBaseObject LateBoundObject
    {
      get
      {
        return curObj;
      }
    }

    // ManagementScope of the object.
    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public System.Management.ManagementScope Scope
    {
      get
      {
        if ( ( isEmbedded == false ) )
        {
          return PrivateLateBoundObject.Scope;
        }
        else
        {
          return null;
        }
      }
      set
      {
        if ( ( isEmbedded == false ) )
        {
          PrivateLateBoundObject.Scope = value;
        }
      }
    }

    // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool AutoCommit
    {
      get
      {
        return AutoCommitProp;
      }
      set
      {
        AutoCommitProp = value;
      }
    }

    // The ManagementPath of the underlying WMI object.
    [Browsable( true )]
    public System.Management.ManagementPath Path
    {
      get
      {
        if ( ( isEmbedded == false ) )
        {
          return PrivateLateBoundObject.Path;
        }
        else
        {
          return null;
        }
      }
      set
      {
        if ( ( isEmbedded == false ) )
        {
          if ( ( CheckIfProperClass( null, value, null ) != true ) )
          {
            throw new System.ArgumentException( "Class name does not match." );
          }
          PrivateLateBoundObject.Path = value;
        }
      }
    }

    // Public static scope property which is used by the various methods.
    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public static System.Management.ManagementScope StaticScope
    {
      get
      {
        return statMgmtScope;
      }
      set
      {
        statMgmtScope = value;
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość BootDevice wskazuje nazwę dysku, z którego wykonywany jest rozruch sys" +
"temu operacyjnego Win32. \nPrzykład: \\\\Urządzenie\\Dysk_twardy0." )]
    public string BootDevice
    {
      get
      {
        return ( (string)( curObj[ "BootDevice" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość BuildNumber wskazuje numer kompilacji systemu operacyjnego i może być " +
"wykorzystana do uzyskania bardziej precyzyjnych informacji dotyczących produktu " +
"niż numery wersji produktu.\nPrzykład: 1381" )]
    public string BuildNumber
    {
      get
      {
        return ( (string)( curObj[ "BuildNumber" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość BuildType wskazuje typ kompilacji wykorzystanej do przygotowania syste" +
"mu operacyjnego. Przykładem może być kompilacja przeznaczona do sprzedaży detali" +
"cznej i kompilacja kontrolowana." )]
    public string BuildType
    {
      get
      {
        return ( (string)( curObj[ "BuildType" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Caption jest zwięzłym (ciąg w pojedynczym wierszu) tekstowym opisem ob" +
"iektu." )]
    public string Caption
    {
      get
      {
        return ( (string)( curObj[ "Caption" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość CodeSet wskazuje numer strony kodowej używanej przez system operacyjny. Strona kodowa zawiera tabelę znaków wykorzystywaną przez system operacyjny do translacji ciągów dla różnych języków. Instytut American National Standards Institute (ANSI) opublikował listę numerów reprezentujących zdefiniowane strony kodowe. Jeżeli system operacyjny nie korzysta ze strony kodowej ANSI, ustawiona zostanie wartość tego członka równa 0. Ciąg CodeSet może zawierać do sześciu znaków do definiowania numeru strony kodowej.
Przykład: 1255." )]
    public string CodeSet
    {
      get
      {
        return ( (string)( curObj[ "CodeSet" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość CountryCode wskazuje kod kraju/regionu używany przez system operacyjny. Wartości są oparte na międzynarodowych prefiksach wybierania numerów (znanych również jako kody krajów/regionów firmy IBM). Ciąg CountryCode może używać do sześciu znaków do zdefiniowania kodu kraju.
Przykład: 1 (dla Stanów Zjednoczonych)." )]
    public string CountryCode
    {
      get
      {
        return ( (string)( curObj[ "CountryCode" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string CreationClassName
    {
      get
      {
        return ( (string)( curObj[ "CreationClassName" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string CSCreationClassName
    {
      get
      {
        return ( (string)( curObj[ "CSCreationClassName" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość CSDVersion zawiera ciąg zakończony znakiem null identyfikujący najnowszy pakiet Service Pack zainstalowany w systemie komputerowym. Jeżeli pakiet Service Pack nie jest zainstalowany, ciąg jest wartością NULL. W wypadku systemów komputerowych, w których uruchomiono system operacyjny Windows 95, właściwość ta zawiera ciąg zakończony znakiem null, udostępniający dowolne dodatkowe informacje dotyczące systemu operacyjnego.
Przykład: Service Pack 3." )]
    public string CSDVersion
    {
      get
      {
        return ( (string)( curObj[ "CSDVersion" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public string CSName
    {
      get
      {
        return ( (string)( curObj[ "CSName" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsCurrentTimeZoneNull
    {
      get
      {
        if ( ( curObj[ "CurrentTimeZone" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "CurrentTimeZone wskazuje liczbę minut, o jaką system operacyjny jest przesunięty " +
"względem czasu Greenwich. Liczba jest dodatnia, ujemna lub równa zero." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public short CurrentTimeZone
    {
      get
      {
        if ( ( curObj[ "CurrentTimeZone" ] == null ) )
        {
          return System.Convert.ToInt16( 0 );
        }
        return ( (short)( curObj[ "CurrentTimeZone" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsDebugNull
    {
      get
      {
        if ( ( curObj[ "Debug" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość Debug określa, czy system operacyjny jest kompilacją kontrolowaną (debugowaną). Kompilacje kontrolowane udostępniają kontrolę błędów, weryfikację argumentów i kod debugowania systemu. Dodatkowy kod w kontrolowanym kodzie binarnym generuje komunikat błędu debugera jądra systemu i przechodzi do debugera. Umożliwia to natychmiastowe ustalenie przyczyny i lokalizację błędu. Wydajność kompilacji kontrolowanych jest mniejsza, ponieważ wykonywany jest dodatkowy kod.
Wartości: PRAWDA lub FAŁSZ. Wartość właściwości równa PRAWDA oznacza, że zainstalowana jest debugująca wersja pliku User.exe." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public bool Debug
    {
      get
      {
        if ( ( curObj[ "Debug" ] == null ) )
        {
          return System.Convert.ToBoolean( 0 );
        }
        return ( (bool)( curObj[ "Debug" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Description udostępnia opis systemu operacyjnego Windows. Niektóre int" +
"erfejsy użytkownika (umożliwiające edytowanie tego opisu) ograniczają długość op" +
"isu do 48 znaków." )]
    public string Description
    {
      get
      {
        return ( (string)( curObj[ "Description" ] ) );
      }
      set
      {
        curObj[ "Description" ] = value;
        if ( ( ( isEmbedded == false )
                    && ( AutoCommitProp == true ) ) )
        {
          PrivateLateBoundObject.Put();
        }
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsDistributedNull
    {
      get
      {
        if ( ( curObj[ "Distributed" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Wartość logiczna wskazująca, czy system operacyjny jest rozproszony wśród wielu w" +
"ęzłów. Jeśli tak, węzły te powinny być zgrupowane jako klaster." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public bool Distributed
    {
      get
      {
        if ( ( curObj[ "Distributed" ] == null ) )
        {
          return System.Convert.ToBoolean( 0 );
        }
        return ( (bool)( curObj[ "Distributed" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsForegroundApplicationBoostNull
    {
      get
      {
        if ( ( curObj[ "ForegroundApplicationBoost" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość ForegroundApplicationBoost wskazuje wzrost priorytetu aplikacji wykonywanej w pierwszym planie. W wypadku systemów komputerowych, w których uruchomiony jest system operacyjny Windows NT 4.0 i Windows 2000, przyśpieszenie aplikacji jest implementowane przez przydzielenie aplikacji większej liczby przedziałów czasu wykonania (długości kwantu). Wartość właściwości ForegroundApplicationBoost równa 0 oznacza, że system zwiększa długość kwantu 6 razy, wartość równa 1 oznacza zwiększenie 12 razy, a wartość równa 2 oznacza zwiększenie 18 razy. W systemach Windows NT w wersji 3.51 i starszych przyśpieszenie aplikacji jest implementowane przez zwiększenie priorytetu w harmonogramie. W wypadku tych systemów priorytet w harmonogramie jest zwiększany o wartość tej właściwości. Wartość domyślna jest równa 2." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public ForegroundApplicationBoostValues ForegroundApplicationBoost
    {
      get
      {
        if ( ( curObj[ "ForegroundApplicationBoost" ] == null ) )
        {
          return ( (ForegroundApplicationBoostValues)( System.Convert.ToInt32( 0 ) ) );
        }
        return ( (ForegroundApplicationBoostValues)( Convert.ToInt32( curObj[ "ForegroundApplicationBoost" ] ) ) );
      }
      set
      {
        curObj[ "ForegroundApplicationBoost" ] = value;
        if ( ( ( isEmbedded == false )
                    && ( AutoCommitProp == true ) ) )
        {
          PrivateLateBoundObject.Put();
        }
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsFreePhysicalMemoryNull
    {
      get
      {
        if ( ( curObj[ "FreePhysicalMemory" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba kilobajtów pamięci fizycznej obecnie nieużywanej i dostępnej" )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 FreePhysicalMemory
    {
      get
      {
        if ( ( curObj[ "FreePhysicalMemory" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "FreePhysicalMemory" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsFreeSpaceInPagingFilesNull
    {
      get
      {
        if ( ( curObj[ "FreeSpaceInPagingFiles" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Całkowita liczba kilobajtów, które można mapować do plików stronicowania systemu " +
"operacyjnego bez powodowania wymiany innych stron." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 FreeSpaceInPagingFiles
    {
      get
      {
        if ( ( curObj[ "FreeSpaceInPagingFiles" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "FreeSpaceInPagingFiles" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsFreeVirtualMemoryNull
    {
      get
      {
        if ( ( curObj[ "FreeVirtualMemory" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba kilobajtów pamięci wirtualnej obecnie nieużywanej i dostępnej. Można ją ob" +
"liczyć na przykład dodając ilość wolnej pamięci RAM do ilości wolnego miejsca st" +
"ronicowania (tzn. dodając właściwości FreePhysicalMemory i FreeSpaceInPagingFile" +
"s)." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 FreeVirtualMemory
    {
      get
      {
        if ( ( curObj[ "FreeVirtualMemory" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "FreeVirtualMemory" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsInstallDateNull
    {
      get
      {
        if ( ( curObj[ "InstallDate" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość InstallDate jest wartością typu data/godzina wskazującą datę i godzinę" +
" zainstalowania obiektu. Brak tej wartości nie oznacza, że obiekt nie jest zains" +
"talowany." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.DateTime InstallDate
    {
      get
      {
        if ( ( curObj[ "InstallDate" ] != null ) )
        {
          return ToDateTime( ( (string)( curObj[ "InstallDate" ] ) ) );
        }
        else
        {
          return System.DateTime.MinValue;
        }
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsLastBootUpTimeNull
    {
      get
      {
        if ( ( curObj[ "LastBootUpTime" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Godzina, o której nastąpił ostatni rozruch systemu operacyjnego" )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.DateTime LastBootUpTime
    {
      get
      {
        if ( ( curObj[ "LastBootUpTime" ] != null ) )
        {
          return ToDateTime( ( (string)( curObj[ "LastBootUpTime" ] ) ) );
        }
        else
        {
          return System.DateTime.MinValue;
        }
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsLocalDateTimeNull
    {
      get
      {
        if ( ( curObj[ "LocalDateTime" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Powiadomienie systemu operacyjnego o lokalnej dacie i porze dnia." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.DateTime LocalDateTime
    {
      get
      {
        if ( ( curObj[ "LocalDateTime" ] != null ) )
        {
          return ToDateTime( ( (string)( curObj[ "LocalDateTime" ] ) ) );
        }
        else
        {
          return System.DateTime.MinValue;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość Locale wskazuje identyfikator języka używany przez system operacyjny. Identyfikator języka jest standardowym skrótem numerycznym wskazującym kraj lub region. Poszczególne języki mają unikatowe identyfikatory języka (LANGID), które są 16-bitowymi wartościami składającymi się z głównego identyfikatora języka i pomocniczego identyfikatora języka." )]
    public string Locale
    {
      get
      {
        return ( (string)( curObj[ "Locale" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Manufacturer wskazuje nazwę producenta systemu operacyjnego. W wypadku" +
" systemów Win32 wartość tej właściwości jest równa Microsoft Corporation." )]
    public string Manufacturer
    {
      get
      {
        return ( (string)( curObj[ "Manufacturer" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsMaxNumberOfProcessesNull
    {
      get
      {
        if ( ( curObj[ "MaxNumberOfProcesses" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Maksymalna liczba kontekstów procesu, które może obsługiwać system operacyjny. Jeśli nie ma ustalonego maksimum, wartość powinna być równa 0. W systemach, które mają ustalone maksimum, ten obiekt może być pomocny w diagnozowaniu błędów występujących, kiedy osiągane jest maksimum. Jeśli jest nieznana, należy wprowadzić wartość -1." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt32 MaxNumberOfProcesses
    {
      get
      {
        if ( ( curObj[ "MaxNumberOfProcesses" ] == null ) )
        {
          return System.Convert.ToUInt32( 0 );
        }
        return ( (System.UInt32)( curObj[ "MaxNumberOfProcesses" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsMaxProcessMemorySizeNull
    {
      get
      {
        if ( ( curObj[ "MaxProcessMemorySize" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Maksymalna liczba kilobajtów pamięci, która może być przydzielona dla procesu. Dla systemów operacyjnych bez pamięci wirtualnej wartość ta jest zazwyczaj równa całkowitej ilości pamięci fizycznej zmniejszonej o pamięć zajętą przez BIOS i system operacyjny. Dla niektórych systemów operacyjnych ta wartość może być nieskończonością - i w takim wypadku należy wprowadzić 0. W innych wypadkach ta wartość może być stałą - na przykład 2GB lub 4GB." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 MaxProcessMemorySize
    {
      get
      {
        if ( ( curObj[ "MaxProcessMemorySize" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "MaxProcessMemorySize" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Dziedziczona właściwość Name służy jako klucz wystąpienia systemu operacyjnego w " +
"systemie komputerowym." )]
    public string Name
    {
      get
      {
        return ( (string)( curObj[ "Name" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsNumberOfLicensedUsersNull
    {
      get
      {
        if ( ( curObj[ "NumberOfLicensedUsers" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba licencji użytkownika dla systemu operacyjnego. Jeśli jest nieograniczona, " +
"należy wprowadzić wartość 0. Jeśli nieznana, należy wprowadzić wartość -1." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt32 NumberOfLicensedUsers
    {
      get
      {
        if ( ( curObj[ "NumberOfLicensedUsers" ] == null ) )
        {
          return System.Convert.ToUInt32( 0 );
        }
        return ( (System.UInt32)( curObj[ "NumberOfLicensedUsers" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsNumberOfProcessesNull
    {
      get
      {
        if ( ( curObj[ "NumberOfProcesses" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba kontekstów procesu obecnie załadowanych lub działających w systemie operac" +
"yjnym." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt32 NumberOfProcesses
    {
      get
      {
        if ( ( curObj[ "NumberOfProcesses" ] == null ) )
        {
          return System.Convert.ToUInt32( 0 );
        }
        return ( (System.UInt32)( curObj[ "NumberOfProcesses" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsNumberOfUsersNull
    {
      get
      {
        if ( ( curObj[ "NumberOfUsers" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba sesji użytkownika, dla których system operacyjny przechowuje obecnie infor" +
"macje o stanie" )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt32 NumberOfUsers
    {
      get
      {
        if ( ( curObj[ "NumberOfUsers" ] == null ) )
        {
          return System.Convert.ToUInt32( 0 );
        }
        return ( (System.UInt32)( curObj[ "NumberOfUsers" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Organization wskazuje zarejestrowaną nazwę firmy użytkownika (systemu " +
"operacyjnego).\nPrzykład: Microsoft Corporation." )]
    public string Organization
    {
      get
      {
        return ( (string)( curObj[ "Organization" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsOSLanguageNull
    {
      get
      {
        if ( ( curObj[ "OSLanguage" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość OSLanguage wskazuje, która wersja językowa systemu operacyjnego jest z" +
"ainstalowana.\nPrzykład: 0x0807 (niemiecka, szwajcarska)" )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt32 OSLanguage
    {
      get
      {
        if ( ( curObj[ "OSLanguage" ] == null ) )
        {
          return System.Convert.ToUInt32( 0 );
        }
        return ( (System.UInt32)( curObj[ "OSLanguage" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsOSProductSuiteNull
    {
      get
      {
        if ( ( curObj[ "OSProductSuite" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość OSProductSuite wskazuje zainstalowany i licencjonowany produkt systemo" +
"wy dodawany do systemu operacyjnego." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public OSProductSuiteValues OSProductSuite
    {
      get
      {
        if ( ( curObj[ "OSProductSuite" ] == null ) )
        {
          return ( (OSProductSuiteValues)( System.Convert.ToInt32( 0 ) ) );
        }
        return ( (OSProductSuiteValues)( Convert.ToInt32( curObj[ "OSProductSuite" ] ) ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsOSTypeNull
    {
      get
      {
        if ( ( curObj[ "OSType" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba całkowita wskazująca typ systemu operacyjnego." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public OSTypeValues OSType
    {
      get
      {
        if ( ( curObj[ "OSType" ] == null ) )
        {
          return ( (OSTypeValues)( System.Convert.ToInt32( 0 ) ) );
        }
        return ( (OSTypeValues)( Convert.ToInt32( curObj[ "OSType" ] ) ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Ciąg opisujący producenta i typ systemu operacyjnego - używany, kiedy właściwość systemu operacyjnego OSType jest ustawiona na wartość 1 (""Inny""). Format ciągu umieszczonego we właściwości OtherTypeDescription powinien być podobny do ciągów Values (wartości) zdefiniowanych dla właściwości OSType. Kiedy właściwość OSType ma wartość inną niż 1, właściwość OtherTypeDescription powinna być ustawiona na wartość NULL." )]
    public string OtherTypeDescription
    {
      get
      {
        return ( (string)( curObj[ "OtherTypeDescription" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość PlusProductID zawiera numer identyfikacyjny produktu dla oprogramowani" +
"a rozszerzonego systemu operacyjnego Windows Plus! (jeśli zainstalowany)." )]
    public string PlusProductID
    {
      get
      {
        return ( (string)( curObj[ "PlusProductID" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość PlusVersionNumber zawiera numer wersji oprogramowania rozszerzonego sy" +
"stemu operacyjnego Windows Plus! (jeśli zainstalowany)." )]
    public string PlusVersionNumber
    {
      get
      {
        return ( (string)( curObj[ "PlusVersionNumber" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsPrimaryNull
    {
      get
      {
        if ( ( curObj[ "Primary" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Primary określa, czy to jest podstawowy system operacyjny.\nWartości: P" +
"RAWDA lub FAŁSZ. Wartość PRAWDA wskazuje, że to jest podstawowy system operacyjn" +
"y." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public bool Primary
    {
      get
      {
        if ( ( curObj[ "Primary" ] == null ) )
        {
          return System.Convert.ToBoolean( 0 );
        }
        return ( (bool)( curObj[ "Primary" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsQuantumLengthNull
    {
      get
      {
        if ( ( curObj[ "QuantumLength" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość QuantumLength definiuje znacznik zegara na jednostkę quantum. Quantum jest jednostką czasu, dopuszczoną przez harmonogram, podawaną dla aplikacji przed przełączeniem na inną aplikację. Podczas, gdy wątek uruchamia jedno quantum, jądro wywłaszcza aplikację i przesuwa ją na koniec kolejki dla aplikacji z równymi priorytetami. Aktualna długość quantum dla wątku zmienia się w zależności od platformy systemu Windows. Tylko dla systemu Windows NT lub Windows 2000." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public QuantumLengthValues QuantumLength
    {
      get
      {
        if ( ( curObj[ "QuantumLength" ] == null ) )
        {
          return ( (QuantumLengthValues)( System.Convert.ToInt32( 0 ) ) );
        }
        return ( (QuantumLengthValues)( Convert.ToInt32( curObj[ "QuantumLength" ] ) ) );
      }
      set
      {
        curObj[ "QuantumLength" ] = value;
        if ( ( ( isEmbedded == false )
                    && ( AutoCommitProp == true ) ) )
        {
          PrivateLateBoundObject.Put();
        }
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsQuantumTypeNull
    {
      get
      {
        if ( ( curObj[ "QuantumType" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość QuantumType określa stałą albo zmienną długość kwantów czasu. System Windows NT 4.0 Workstation lub Windows 2000 ustawia domyślnie zmienną długość kwantów czasu, gdzie aplikacja pierwszego planu otrzymuje dłuższe kwanty niż aplikacja działająca w tle. Serwer Windows NT ustawia domyślnie stałą długość kwantów czasu. Kwant jest jednostką czasu wykonywania, jaką moduł planowania może przydzielić dla aplikacji przed przełączeniem do innej aplikacji. Gdy dany wątek zużyje swój kwant czasu, jądro wywłaszcza go i przesuwa go na koniec kolejki aplikacji o tym samym priorytecie. Faktyczna długość kwantu dla wątku zmienia się w zależności od platformy systemu Windows. Tylko dla systemu Windows NT lub Windows 2000." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public QuantumTypeValues QuantumType
    {
      get
      {
        if ( ( curObj[ "QuantumType" ] == null ) )
        {
          return ( (QuantumTypeValues)( System.Convert.ToInt32( 0 ) ) );
        }
        return ( (QuantumTypeValues)( Convert.ToInt32( curObj[ "QuantumType" ] ) ) );
      }
      set
      {
        curObj[ "QuantumType" ] = value;
        if ( ( ( isEmbedded == false )
                    && ( AutoCommitProp == true ) ) )
        {
          PrivateLateBoundObject.Put();
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość RegisteredUser wskazuje nazwę zarejestrowanego użytkownika systemu ope" +
"racyjnego.\nPrzykład: Jan Nowak" )]
    public string RegisteredUser
    {
      get
      {
        return ( (string)( curObj[ "RegisteredUser" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość SerialNumber wskazuje seryjny numer identyfikacyjny produktu systemu o" +
"peracyjnego.\nPrzykład:10497-OEM-0031416-71674." )]
    public string SerialNumber
    {
      get
      {
        return ( (string)( curObj[ "SerialNumber" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsServicePackMajorVersionNull
    {
      get
      {
        if ( ( curObj[ "ServicePackMajorVersion" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość ServicePackMajorVersion wskazuje główny numer wersji pakietu service pack zainstalowanego w systemie komputerowym. Jeśli żaden pakiet service pack nie został zainstalowany, wartość jest równa zero. Właściwość ServicePackMajorVersion jest prawidłowa dla komputerów z uruchomionym systemem Windows 2000 lub nowszym (W przeciwnym razie wartość jest NULL)." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt16 ServicePackMajorVersion
    {
      get
      {
        if ( ( curObj[ "ServicePackMajorVersion" ] == null ) )
        {
          return System.Convert.ToUInt16( 0 );
        }
        return ( (System.UInt16)( curObj[ "ServicePackMajorVersion" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsServicePackMinorVersionNull
    {
      get
      {
        if ( ( curObj[ "ServicePackMinorVersion" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość ServicePackMinorVersion wskazuje pomocniczy numer wersji pakietu service pack zainstalowanego w systemie komputerowym. Jeśli żaden pakiet service pack nie został zainstalowany, wartość jest równa zero. Właściwość ServicePackMinorVersion jest prawidłowa dla komputerów z uruchomionym systemem Windows 2000 lub nowszym (W przeciwnym razie wartość jest NULL)." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt16 ServicePackMinorVersion
    {
      get
      {
        if ( ( curObj[ "ServicePackMinorVersion" ] == null ) )
        {
          return System.Convert.ToUInt16( 0 );
        }
        return ( (System.UInt16)( curObj[ "ServicePackMinorVersion" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsSizeStoredInPagingFilesNull
    {
      get
      {
        if ( ( curObj[ "SizeStoredInPagingFiles" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Całkowita liczba kilobajtów, które można przechowywać w plikach stronicowania sys" +
"temu operacyjnego. Liczba ta nie reprezentuje rzeczywistego fizycznego rozmiaru " +
"pliku stronicowania na dysku. Wartość 0 wskazuje, że nie ma żadnych plików stron" +
"icowania." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 SizeStoredInPagingFiles
    {
      get
      {
        if ( ( curObj[ "SizeStoredInPagingFiles" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "SizeStoredInPagingFiles" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Właściwość Stan jest ciągiem wskazującym bieżący stan obiektu. Można zdefiniować rozmaite stany operacyjne. Stany działania to ""OK"", ""Obniżony"" i ""P. awaria"". Stan ""P. awaria"" wskazuje, że element może działać poprawnie, ale w najbliższej przyszłości może nastąpić awaria. Przykładem może być dysk twardy SMART. Można także określić stany braku działania. Są nimi ""Błąd"", ""Uruchamianie"", ""Zatrzymywanie"" i ""Serwis"". Ostatni, ""Serwis"", może się odnosić do odświeżania dublowania dysku, ponownego ładowania listy uprawnień użytkowników lub innych prac administracyjnych. Nie każda taka praca jest typu on-line, a jednak zarządzany element nie jest w stanie ""OK"" ani żadnym innym." )]
    public string Status
    {
      get
      {
        return ( (string)( curObj[ "Status" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość SystemDevice wskazuje partycję dysku fizycznego, na której zainstalowa" +
"no system operacyjny." )]
    public string SystemDevice
    {
      get
      {
        return ( (string)( curObj[ "SystemDevice" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość SystemDirectory wskazuje katalog systemowy systemu operacyjnego.\nPrzyk" +
"ład: C:\\WINDOWS\\SYSTEM32" )]
    public string SystemDirectory
    {
      get
      {
        return ( (string)( curObj[ "SystemDirectory" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsTotalSwapSpaceSizeNull
    {
      get
      {
        if ( ( curObj[ "TotalSwapSpaceSize" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( @"Całkowity obszar wymiany w kilobajtach. Wartością tą może być NULL (nieokreślona), jeśli obszar wymiany nie jest odróżniany od plików stronicowania. Jednakże niektóre systemy operacyjne rozróżniają te pojęcia. Na przykład, w systemie UNIX cały proces może być „wymieniony”, jeżeli liczba wolnych stron spadnie i pozostanie poniżej określonej wartości." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 TotalSwapSpaceSize
    {
      get
      {
        if ( ( curObj[ "TotalSwapSpaceSize" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "TotalSwapSpaceSize" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsTotalVirtualMemorySizeNull
    {
      get
      {
        if ( ( curObj[ "TotalVirtualMemorySize" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Liczba kilobajtów pamięci wirtualnej. Na przykład można to obliczyć dodając całko" +
"witą ilość pamięci RAM do wielkości obszaru wymiany (tzn. dodając ilość pamięci " +
"wewnętrznej systemu komputerowego do właściwości SizeStoredInPagingFiles." )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 TotalVirtualMemorySize
    {
      get
      {
        if ( ( curObj[ "TotalVirtualMemorySize" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "TotalVirtualMemorySize" ] ) );
      }
    }

    [Browsable( false )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    public bool IsTotalVisibleMemorySizeNull
    {
      get
      {
        if ( ( curObj[ "TotalVisibleMemorySize" ] == null ) )
        {
          return true;
        }
        else
        {
          return false;
        }
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Całkowita ilość fizycznej pamięci dostępnej dla systemu operacyjnego. Wartość nie" +
" musi wskazywać prawdziwej wielkości fizycznej pamięci, ale tę jej ilość, która " +
"jest zgłaszana do wykorzystania przez system operacyjny" )]
    [TypeConverter( typeof( WMIValueTypeConverter ) )]
    public System.UInt64 TotalVisibleMemorySize
    {
      get
      {
        if ( ( curObj[ "TotalVisibleMemorySize" ] == null ) )
        {
          return System.Convert.ToUInt64( 0 );
        }
        return ( (System.UInt64)( curObj[ "TotalVisibleMemorySize" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość Version wskazuje numer wersji systemu operacyjnego.\nPrzykład: 4.0" )]
    public string Version
    {
      get
      {
        return ( (string)( curObj[ "Version" ] ) );
      }
    }

    [Browsable( true )]
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
    [Description( "Właściwość WindowsDirectory wskazuje katalog Windows systemu operacyjnego.\nPrzykł" +
"ad: C:\\WINDOWS" )]
    public string WindowsDirectory
    {
      get
      {
        return ( (string)( curObj[ "WindowsDirectory" ] ) );
      }
    }

    private bool CheckIfProperClass( System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam )
    {
      if ( ( ( path != null )
                  && ( System.String.Compare( path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture ) == 0 ) ) )
      {
        return true;
      }
      else
      {
        return CheckIfProperClass( new System.Management.ManagementObject( mgmtScope, path, OptionsParam ) );
      }
    }

    private bool CheckIfProperClass( System.Management.ManagementBaseObject theObj )
    {
      if ( ( ( theObj != null )
                  && ( System.String.Compare( ( (string)( theObj[ "__CLASS" ] ) ), ManagementClassName, true, CultureInfo.InvariantCulture ) == 0 ) ) )
      {
        return true;
      }
      else
      {
        System.Array parentClasses = ( (System.Array)( theObj[ "__DERIVATION" ] ) );
        if ( ( parentClasses != null ) )
        {
          Int32 count = 0;
          for ( count = 0; ( count < parentClasses.Length ); count = ( count + 1 ) )
          {
            if ( ( System.String.Compare( ( (string)( parentClasses.GetValue( count ) ) ), ManagementClassName, true, CultureInfo.InvariantCulture ) == 0 ) )
            {
              return true;
            }
          }
        }
      }
      return false;
    }

    private bool ShouldSerializeCurrentTimeZone()
    {
      if ( ( IsCurrentTimeZoneNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeDebug()
    {
      if ( ( IsDebugNull == false ) )
      {
        return true;
      }
      return false;
    }

    private void ResetDescription()
    {
      curObj[ "Description" ] = null;
      if ( ( ( isEmbedded == false )
                  && ( AutoCommitProp == true ) ) )
      {
        PrivateLateBoundObject.Put();
      }
    }

    private bool ShouldSerializeDistributed()
    {
      if ( ( IsDistributedNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeForegroundApplicationBoost()
    {
      if ( ( IsForegroundApplicationBoostNull == false ) )
      {
        return true;
      }
      return false;
    }

    private void ResetForegroundApplicationBoost()
    {
      curObj[ "ForegroundApplicationBoost" ] = null;
      if ( ( ( isEmbedded == false )
                  && ( AutoCommitProp == true ) ) )
      {
        PrivateLateBoundObject.Put();
      }
    }

    private bool ShouldSerializeFreePhysicalMemory()
    {
      if ( ( IsFreePhysicalMemoryNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeFreeSpaceInPagingFiles()
    {
      if ( ( IsFreeSpaceInPagingFilesNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeFreeVirtualMemory()
    {
      if ( ( IsFreeVirtualMemoryNull == false ) )
      {
        return true;
      }
      return false;
    }

    // Converts a given datetime in DMTF format to System.DateTime object.
    static System.DateTime ToDateTime( string dmtfDate )
    {
      int year = System.DateTime.MinValue.Year;
      int month = System.DateTime.MinValue.Month;
      int day = System.DateTime.MinValue.Day;
      int hour = System.DateTime.MinValue.Hour;
      int minute = System.DateTime.MinValue.Minute;
      int second = System.DateTime.MinValue.Second;
      long ticks = 0;
      string dmtf = dmtfDate;
      System.DateTime datetime = System.DateTime.MinValue;
      string tempString = System.String.Empty;
      if ( ( dmtf == null ) )
      {
        throw new System.ArgumentOutOfRangeException();
      }
      if ( ( dmtf.Length == 0 ) )
      {
        throw new System.ArgumentOutOfRangeException();
      }
      if ( ( dmtf.Length != 25 ) )
      {
        throw new System.ArgumentOutOfRangeException();
      }
      try
      {
        tempString = dmtf.Substring( 0, 4 );
        if ( ( "****" != tempString ) )
        {
          year = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 4, 2 );
        if ( ( "**" != tempString ) )
        {
          month = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 6, 2 );
        if ( ( "**" != tempString ) )
        {
          day = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 8, 2 );
        if ( ( "**" != tempString ) )
        {
          hour = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 10, 2 );
        if ( ( "**" != tempString ) )
        {
          minute = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 12, 2 );
        if ( ( "**" != tempString ) )
        {
          second = System.Int32.Parse( tempString );
        }
        tempString = dmtf.Substring( 15, 6 );
        if ( ( "******" != tempString ) )
        {
          ticks = ( System.Int64.Parse( tempString )
                      * ( System.TimeSpan.TicksPerMillisecond / 1000 ) );
        }
        if ( ( ( ( ( ( ( ( ( year < 0 )
                    || ( month < 0 ) )
                    || ( day < 0 ) )
                    || ( hour < 0 ) )
                    || ( minute < 0 ) )
                    || ( minute < 0 ) )
                    || ( second < 0 ) )
                    || ( ticks < 0 ) ) )
        {
          throw new System.ArgumentOutOfRangeException();
        }
      }
      catch { throw new System.ArgumentOutOfRangeException(); }
      datetime = new System.DateTime( year, month, day, hour, minute, second, 0 );
      datetime = datetime.AddTicks( ticks );
      System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset( datetime );
      int UTCOffset = 0;
      long OffsetToBeAdjusted = 0;
      long OffsetMins = ( tickOffset.Ticks / System.TimeSpan.TicksPerMinute );
      tempString = dmtf.Substring( 22, 3 );
      if ( ( tempString != "***" ) )
      {
        tempString = dmtf.Substring( 21, 4 );
        try
        {
          UTCOffset = System.Int32.Parse( tempString );
        }
        catch { throw new System.ArgumentOutOfRangeException(); }
        OffsetToBeAdjusted = ( OffsetMins - UTCOffset );
        datetime = datetime.AddMinutes( OffsetToBeAdjusted );
      }
      return datetime;
    }

    // Converts a given System.DateTime object to DMTF datetime format.
    static string ToDmtfDateTime( System.DateTime date )
    {
      string utcString = System.String.Empty;
      System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset( date );
      long OffsetMins = ( tickOffset.Ticks / System.TimeSpan.TicksPerMinute );
      if ( ( System.Math.Abs( OffsetMins ) > 999 ) )
      {
        date = date.ToUniversalTime();
        utcString = "+000";
      }
      else
      {
        if ( ( tickOffset.Ticks >= 0 ) )
        {
          utcString = ( "+" + ( ( tickOffset.Ticks / System.TimeSpan.TicksPerMinute ) ).ToString().PadLeft( 3, '0' ) );
        }
        else
        {
          string strTemp = OffsetMins.ToString();
          utcString = ( "-" + strTemp.Substring( 1, ( strTemp.Length - 1 ) ).PadLeft( 3, '0' ) );
        }
      }
      string dmtfDateTime = date.Year.ToString().PadLeft( 4, '0' );
      dmtfDateTime = ( dmtfDateTime + date.Month.ToString().PadLeft( 2, '0' ) );
      dmtfDateTime = ( dmtfDateTime + date.Day.ToString().PadLeft( 2, '0' ) );
      dmtfDateTime = ( dmtfDateTime + date.Hour.ToString().PadLeft( 2, '0' ) );
      dmtfDateTime = ( dmtfDateTime + date.Minute.ToString().PadLeft( 2, '0' ) );
      dmtfDateTime = ( dmtfDateTime + date.Second.ToString().PadLeft( 2, '0' ) );
      dmtfDateTime = ( dmtfDateTime + "." );
      System.DateTime dtTemp = new System.DateTime( date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0 );
      long microsec = ( ( ( date.Ticks - dtTemp.Ticks )
                  * 1000 )
                  / System.TimeSpan.TicksPerMillisecond );
      string strMicrosec = microsec.ToString();
      if ( ( strMicrosec.Length > 6 ) )
      {
        strMicrosec = strMicrosec.Substring( 0, 6 );
      }
      dmtfDateTime = ( dmtfDateTime + strMicrosec.PadLeft( 6, '0' ) );
      dmtfDateTime = ( dmtfDateTime + utcString );
      return dmtfDateTime;
    }

    private bool ShouldSerializeInstallDate()
    {
      if ( ( IsInstallDateNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeLastBootUpTime()
    {
      if ( ( IsLastBootUpTimeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeLocalDateTime()
    {
      if ( ( IsLocalDateTimeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeMaxNumberOfProcesses()
    {
      if ( ( IsMaxNumberOfProcessesNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeMaxProcessMemorySize()
    {
      if ( ( IsMaxProcessMemorySizeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeNumberOfLicensedUsers()
    {
      if ( ( IsNumberOfLicensedUsersNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeNumberOfProcesses()
    {
      if ( ( IsNumberOfProcessesNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeNumberOfUsers()
    {
      if ( ( IsNumberOfUsersNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeOSLanguage()
    {
      if ( ( IsOSLanguageNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeOSProductSuite()
    {
      if ( ( IsOSProductSuiteNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeOSType()
    {
      if ( ( IsOSTypeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializePrimary()
    {
      if ( ( IsPrimaryNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeQuantumLength()
    {
      if ( ( IsQuantumLengthNull == false ) )
      {
        return true;
      }
      return false;
    }

    private void ResetQuantumLength()
    {
      curObj[ "QuantumLength" ] = null;
      if ( ( ( isEmbedded == false )
                  && ( AutoCommitProp == true ) ) )
      {
        PrivateLateBoundObject.Put();
      }
    }

    private bool ShouldSerializeQuantumType()
    {
      if ( ( IsQuantumTypeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private void ResetQuantumType()
    {
      curObj[ "QuantumType" ] = null;
      if ( ( ( isEmbedded == false )
                  && ( AutoCommitProp == true ) ) )
      {
        PrivateLateBoundObject.Put();
      }
    }

    private bool ShouldSerializeServicePackMajorVersion()
    {
      if ( ( IsServicePackMajorVersionNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeServicePackMinorVersion()
    {
      if ( ( IsServicePackMinorVersionNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeSizeStoredInPagingFiles()
    {
      if ( ( IsSizeStoredInPagingFilesNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeTotalSwapSpaceSize()
    {
      if ( ( IsTotalSwapSpaceSizeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeTotalVirtualMemorySize()
    {
      if ( ( IsTotalVirtualMemorySizeNull == false ) )
      {
        return true;
      }
      return false;
    }

    private bool ShouldSerializeTotalVisibleMemorySize()
    {
      if ( ( IsTotalVisibleMemorySizeNull == false ) )
      {
        return true;
      }
      return false;
    }

    [Browsable( true )]
    public void CommitObject()
    {
      if ( ( isEmbedded == false ) )
      {
        PrivateLateBoundObject.Put();
      }
    }

    private static string ConstructPath( string keyName )
    {
      string strPath = "ROOT\\CIMV2:Win32_OperatingSystem";
      strPath = ( strPath
                  + ( ".Name="
                  + ( "\""
                  + ( keyName + "\"" ) ) ) );
      return strPath;
    }

    // Different overloads of GetInstances() help in enumerating instances of the WMI class.
    public static OperatingSystemCollection GetInstances()
    {
      return GetInstances( ( (System.Management.ManagementScope)( null ) ), ( (System.Management.EnumerationOptions)( null ) ) );
    }

    public static OperatingSystemCollection GetInstances( string condition )
    {
      return GetInstances( null, condition, null );
    }

    public static OperatingSystemCollection GetInstances( System.String[] selectedProperties )
    {
      return GetInstances( null, null, selectedProperties );
    }

    public static OperatingSystemCollection GetInstances( string condition, System.String[] selectedProperties )
    {
      return GetInstances( null, condition, selectedProperties );
    }

    public static OperatingSystemCollection GetInstances( System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions )
    {
      if ( ( mgmtScope == null ) )
      {
        if ( ( statMgmtScope == null ) )
        {
          mgmtScope = new System.Management.ManagementScope();
          mgmtScope.Path.NamespacePath = "root\\CIMV2";
        }
        else
        {
          mgmtScope = statMgmtScope;
        }
      }
      System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
      pathObj.ClassName = "Win32_OperatingSystem";
      pathObj.NamespacePath = "root\\CIMV2";
      System.Management.ManagementClass clsObject = new System.Management.ManagementClass( mgmtScope, pathObj, null );
      if ( ( enumOptions == null ) )
      {
        enumOptions = new System.Management.EnumerationOptions();
        enumOptions.EnsureLocatable = true;
      }
      return new OperatingSystemCollection( clsObject.GetInstances( enumOptions ) );
    }

    public static OperatingSystemCollection GetInstances( System.Management.ManagementScope mgmtScope, string condition )
    {
      return GetInstances( mgmtScope, condition, null );
    }

    public static OperatingSystemCollection GetInstances( System.Management.ManagementScope mgmtScope, System.String[] selectedProperties )
    {
      return GetInstances( mgmtScope, null, selectedProperties );
    }

    public static OperatingSystemCollection GetInstances( System.Management.ManagementScope mgmtScope, string condition, System.String[] selectedProperties )
    {
      if ( ( mgmtScope == null ) )
      {
        if ( ( statMgmtScope == null ) )
        {
          mgmtScope = new System.Management.ManagementScope();
          mgmtScope.Path.NamespacePath = "root\\CIMV2";
        }
        else
        {
          mgmtScope = statMgmtScope;
        }
      }
      System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher( mgmtScope, new SelectQuery( "Win32_OperatingSystem", condition, selectedProperties ) );
      System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
      enumOptions.EnsureLocatable = true;
      ObjectSearcher.Options = enumOptions;
      return new OperatingSystemCollection( ObjectSearcher.Get() );
    }

    [Browsable( true )]
    public static OperatingSystem CreateInstance()
    {
      System.Management.ManagementScope mgmtScope = null;
      if ( ( statMgmtScope == null ) )
      {
        mgmtScope = new System.Management.ManagementScope();
        mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
      }
      else
      {
        mgmtScope = statMgmtScope;
      }
      System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath( CreatedClassName );
      return new OperatingSystem( new System.Management.ManagementClass( mgmtScope, mgmtPath, null ).CreateInstance() );
    }

    [Browsable( true )]
    public void Delete()
    {
      PrivateLateBoundObject.Delete();
    }

    public System.UInt32 Reboot()
    {
      if ( ( isEmbedded == false ) )
      {
        System.Management.ManagementBaseObject inParams = null;
        bool EnablePrivileges = PrivateLateBoundObject.Scope.Options.EnablePrivileges;
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = true;
        System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod( "Reboot", inParams, null );
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = EnablePrivileges;
        return System.Convert.ToUInt32( outParams.Properties[ "ReturnValue" ].Value );
      }
      else
      {
        return System.Convert.ToUInt32( 0 );
      }
    }
    //http://msdn.microsoft.com/en-us/library/aa394058(VS.85).aspx
    //    Win32Shutdown Method of the Win32_OperatingSystem Class
    //    The Win32Shutdown WMI class method provides the full set of shutdown options supported by Win32 operating systems. These include logoff, shutdown, reboot, and forcing a logoff, shutdown, or reboot. The calling process must have the SE_SHUTDOWN_NAME privilege.
    //    Windows NT 4.0 and Windows Me/98/95:  SE_SHUTDOWN_NAME privilege is not required.
    //This topic uses Managed Object Format (MOF) syntax. For more information about using this method, see Calling a Method.
    //Syntax:
    //uint32 Win32Shutdown(
    //    sint32 Flags,
    //    sint32 Reserved
    //);
    //Parameters:
    //- Flags:
    //    Bitmapped set of flags to shut the computer down. To force a command, add the Force flag (4) to the command value. Using Force in conjunction with Shutdown or Reboot on a remote computer immediately shuts down everything (including WMI, COM, and so on), or reboots the remote computer. This results in an indeterminate return value.
    //    Value 	Meaning

    //    0     0x0     Log Off
    //    4     0x4     Forced Log Off (0 + 4)
    //    1     0x1     Shutdown
    //    5     0x5     Forced Shutdown (1 + 4)
    //    2     0x2    Reboot
    //    6     0x6    Forced Reboot (2 + 4)
    //    8     0x8    Power Off
    //    12    0xC    Forced Power Off (8 + 4)
    //- Reserved
    //    A means to extend Win32Shutdown. Currently, the Reserved parameter is ignored.

    public System.UInt32 ForceReboot()
    {
      if ( ( isEmbedded == false ) )
      {
        System.Management.ManagementBaseObject inParams = PrivateLateBoundObject.GetMethodParameters("Win32Shutdown");
        inParams[ "Flags" ] = "6"; // System forced reboot
        inParams[ "Reserved" ] = "0";
        bool EnablePrivileges = PrivateLateBoundObject.Scope.Options.EnablePrivileges;
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = true;
        System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod( "Win32Shutdown", inParams, null );
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = EnablePrivileges;
        return System.Convert.ToUInt32( outParams.Properties[ "ReturnValue" ].Value );
      }
      else
      {
        return System.Convert.ToUInt32( 0 );
      }
    }

    public System.UInt32 Shutdown()
    {
      if ( ( isEmbedded == false ) )
      {
        System.Management.ManagementBaseObject inParams = null;
        bool EnablePrivileges = PrivateLateBoundObject.Scope.Options.EnablePrivileges;
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = true;
        System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod( "Shutdown", inParams, null );
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = EnablePrivileges;
        return System.Convert.ToUInt32( outParams.Properties[ "ReturnValue" ].Value );
      }
      else
      {
        return System.Convert.ToUInt32( 0 );
      }
    }

    public System.UInt32 Win32Shutdown( int Flags, int Reserved )
    {
      if ( ( isEmbedded == false ) )
      {
        System.Management.ManagementBaseObject inParams = null;
        bool EnablePrivileges = PrivateLateBoundObject.Scope.Options.EnablePrivileges;
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = true;
        inParams = PrivateLateBoundObject.GetMethodParameters( "Win32Shutdown" );
        inParams[ "Flags" ] = Flags;
        inParams[ "Reserved" ] = Reserved;
        System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod( "Win32Shutdown", inParams, null );
        PrivateLateBoundObject.Scope.Options.EnablePrivileges = EnablePrivileges;
        return System.Convert.ToUInt32( outParams.Properties[ "ReturnValue" ].Value );
      }
      else
      {
        return System.Convert.ToUInt32( 0 );
      }
    }

    public enum ForegroundApplicationBoostValues: int
    {

      Brak,

      Minimum,

      Maksimum,
    }

    public enum OSProductSuiteValues: int
    {

      Pakiet_Small_Business = 1,

      Przedsiębiorstwo = 2,

      BackOffice = 4,

      Serwer_komunikacyjny = 8,

      Serwer_terminali = 16,

      Pakiet_Small_Business_ograniczony_ = 32,

      Osadzone_NT = 64,

      Centrum_danych = 128,
    }

    public enum OSTypeValues: int
    {

      Nieznane,

      Inne,

      MACOS,

      ATTUNIX,

      DGUX,

      DECNT,

      Digital_Unix,

      OpenVMS,

      HPUX,

      AIX,

      MVS,

      OS400,

      OS_2,

      JavaVM,

      MSDOS,

      WIN3x,

      WIN95,

      WIN98,

      WINNT,

      WINCE,

      NCR3000,

      NetWare,

      OSF,

      DC_OS,

      Reliant_UNIX,

      SCO_UnixWare,

      SCO_OpenServer,

      Sequent,

      IRIX,

      Solaris,

      SunOS,

      U6000,

      ASERIES,

      TandemNSK,

      TandemNT,

      BS2000,

      LINUX,

      Lynx,

      XENIX,

      VM_ESA,

      Interactive_UNIX,

      BSDUNIX,

      FreeBSD,

      NetBSD,

      GNU_Hurd,

      OS9,

      MACH_Kernel,

      Inferno,

      QNX,

      EPOC,

      IxWorks,

      VxWorks,

      MiNT,

      BeOS,

      HP_MPE,

      NextStep,

      PalmPilot,

      Rhapsody,
    }

    public enum QuantumLengthValues: int
    {

      Nieznane,

      Jedno_tyknięcie,

      Dwa_takty,
    }

    public enum QuantumTypeValues: int
    {

      Nieznany,

      Stały,

      Zmienny,
    }

    // Enumerator implementation for enumerating instances of the class.
    public class OperatingSystemCollection: object, ICollection
    {

      private ManagementObjectCollection ObjectCollection;

      public OperatingSystemCollection( ManagementObjectCollection objCollection )
      {
        ObjectCollection = objCollection;
      }

      public int Count
      {
        get
        {
          return ObjectCollection.Count;
        }
      }

      public bool IsSynchronized
      {
        get
        {
          return ObjectCollection.IsSynchronized;
        }
      }

      public object SyncRoot
      {
        get
        {
          return this;
        }
      }

      public void CopyTo( System.Array array, int index )
      {
        ObjectCollection.CopyTo( array, index );
        int nCtr;
        for ( nCtr = 0; ( nCtr < array.Length ); nCtr = ( nCtr + 1 ) )
        {
          array.SetValue( new OperatingSystem( ( (System.Management.ManagementObject)( array.GetValue( nCtr ) ) ) ), nCtr );
        }
      }

      public System.Collections.IEnumerator GetEnumerator()
      {
        return new OperatingSystemEnumerator( ObjectCollection.GetEnumerator() );
      }

      public class OperatingSystemEnumerator: object, System.Collections.IEnumerator
      {

        private ManagementObjectCollection.ManagementObjectEnumerator ObjectEnumerator;

        public OperatingSystemEnumerator( ManagementObjectCollection.ManagementObjectEnumerator objEnum )
        {
          ObjectEnumerator = objEnum;
        }

        public object Current
        {
          get
          {
            return new OperatingSystem( ( (System.Management.ManagementObject)( ObjectEnumerator.Current ) ) );
          }
        }

        public bool MoveNext()
        {
          return ObjectEnumerator.MoveNext();
        }

        public void Reset()
        {
          ObjectEnumerator.Reset();
        }
      }
    }

    // TypeConverter to handle null values for ValueType properties
    public class WMIValueTypeConverter: TypeConverter
    {

      private TypeConverter baseConverter;

      public WMIValueTypeConverter( System.Type baseType )
      {
        baseConverter = TypeDescriptor.GetConverter( baseType );
      }

      public override bool CanConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Type srcType )
      {
        return baseConverter.CanConvertFrom( context, srcType );
      }

      public override bool CanConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType )
      {
        return baseConverter.CanConvertTo( context, destinationType );
      }

      public override object ConvertFrom( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
      {
        return baseConverter.ConvertFrom( context, culture, value );
      }

      public override object CreateInstance( System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary )
      {
        return baseConverter.CreateInstance( context, dictionary );
      }

      public override bool GetCreateInstanceSupported( System.ComponentModel.ITypeDescriptorContext context )
      {
        return baseConverter.GetCreateInstanceSupported( context );
      }

      public override PropertyDescriptorCollection GetProperties( System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar )
      {
        return baseConverter.GetProperties( context, value, attributeVar );
      }

      public override bool GetPropertiesSupported( System.ComponentModel.ITypeDescriptorContext context )
      {
        return baseConverter.GetPropertiesSupported( context );
      }

      public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues( System.ComponentModel.ITypeDescriptorContext context )
      {
        return baseConverter.GetStandardValues( context );
      }

      public override bool GetStandardValuesExclusive( System.ComponentModel.ITypeDescriptorContext context )
      {
        return baseConverter.GetStandardValuesExclusive( context );
      }

      public override bool GetStandardValuesSupported( System.ComponentModel.ITypeDescriptorContext context )
      {
        return baseConverter.GetStandardValuesSupported( context );
      }

      public override object ConvertTo( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType )
      {
        if ( ( context != null ) )
        {
          if ( ( context.PropertyDescriptor.ShouldSerializeValue( context.Instance ) == false ) )
          {
            return "";
          }
        }
        return baseConverter.ConvertTo( context, culture, value, destinationType );
      }
    }

    // Embedded class to represent WMI system Properties.
    [TypeConverter( typeof( System.ComponentModel.ExpandableObjectConverter ) )]
    public class ManagementSystemProperties
    {

      private System.Management.ManagementBaseObject PrivateLateBoundObject;

      public ManagementSystemProperties( System.Management.ManagementBaseObject ManagedObject )
      {
        PrivateLateBoundObject = ManagedObject;
      }

      [Browsable( true )]
      public int GENUS
      {
        get
        {
          return ( (int)( PrivateLateBoundObject[ "__GENUS" ] ) );
        }
      }

      [Browsable( true )]
      public string CLASS
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__CLASS" ] ) );
        }
      }

      [Browsable( true )]
      public string SUPERCLASS
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__SUPERCLASS" ] ) );
        }
      }

      [Browsable( true )]
      public string DYNASTY
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__DYNASTY" ] ) );
        }
      }

      [Browsable( true )]
      public string RELPATH
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__RELPATH" ] ) );
        }
      }

      [Browsable( true )]
      public int PROPERTY_COUNT
      {
        get
        {
          return ( (int)( PrivateLateBoundObject[ "__PROPERTY_COUNT" ] ) );
        }
      }

      [Browsable( true )]
      public string[] DERIVATION
      {
        get
        {
          return ( (string[])( PrivateLateBoundObject[ "__DERIVATION" ] ) );
        }
      }

      [Browsable( true )]
      public string SERVER
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__SERVER" ] ) );
        }
      }

      [Browsable( true )]
      public string NAMESPACE
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__NAMESPACE" ] ) );
        }
      }

      [Browsable( true )]
      public string PATH
      {
        get
        {
          return ( (string)( PrivateLateBoundObject[ "__PATH" ] ) );
        }
      }
    }
  }
}
