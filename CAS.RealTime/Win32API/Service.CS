namespace ROOT.CIMV2.Win32 {
    using System;
    using System.ComponentModel;
    using System.Management;
    using System.Collections;
    using System.Globalization;
    
    
    // Functions ShouldSerialize<PropertyName> are functions used by VS property browser to check if a particular property has to be serialized. These functions are added for all ValueType properties ( properties of type Int32, BOOL etc.. which cannot be set to null). These functions use Is<PropertyName>Null function. These functions are also used in the TypeConverter implementation for the properties to check for NULL value of property so that an empty value can be shown in Property browser in case of Drag and Drop in Visual studio.
    // Functions Is<PropertyName>Null() are used to check if a property is NULL.
    // Functions Reset<PropertyName> are added for Nullable Read/Write properties. These functions are used by VS designer in property browser to set a property to NULL.
    // Every property added to the class for WMI property has attributes set to define its behavior in Visual Studio designer and also to define a TypeConverter to be used.
    // Datetime conversion functions ToDateTime and ToDmtfDateTime are added to the class to convert DMTF datetime to System.DateTime and vice-versa.
    // An Early Bound class generated for the WMI class.Win32_Service
    public class Service : System.ComponentModel.Component {
        
        // Private property to hold the WMI namespace in which the class resides.
        private static string CreatedWmiNamespace = "root\\CimV2";
        
        // Private property to hold the name of WMI class which created this class.
        private static string CreatedClassName = "Win32_Service";
        
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
        public Service() : 
                this(((System.Management.ManagementScope)(null)), ((System.Management.ManagementPath)(null)), ((System.Management.ObjectGetOptions)(null))) {
        }
        
        public Service(string keyName) : 
                this(((System.Management.ManagementScope)(null)), ((System.Management.ManagementPath)(new System.Management.ManagementPath(Service.ConstructPath(keyName)))), ((System.Management.ObjectGetOptions)(null))) {
        }
        
        public Service(System.Management.ManagementScope mgmtScope, string keyName) : 
                this(((System.Management.ManagementScope)(mgmtScope)), ((System.Management.ManagementPath)(new System.Management.ManagementPath(Service.ConstructPath(keyName)))), ((System.Management.ObjectGetOptions)(null))) {
        }
        
        public Service(System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) : 
                this(((System.Management.ManagementScope)(null)), ((System.Management.ManagementPath)(path)), ((System.Management.ObjectGetOptions)(getOptions))) {
        }
        
        public Service(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path) : 
                this(((System.Management.ManagementScope)(mgmtScope)), ((System.Management.ManagementPath)(path)), ((System.Management.ObjectGetOptions)(null))) {
        }
        
        public Service(System.Management.ManagementPath path) : 
                this(((System.Management.ManagementScope)(null)), ((System.Management.ManagementPath)(path)), ((System.Management.ObjectGetOptions)(null))) {
        }
        
        public Service(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions getOptions) {
            if ((path != null)) {
                if ((CheckIfProperClass(mgmtScope, path, getOptions) != true)) {
                    throw new System.ArgumentException("Class name does not match.");
                }
            }
            PrivateLateBoundObject = new System.Management.ManagementObject(mgmtScope, path, getOptions);
            PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
            curObj = PrivateLateBoundObject;
        }
        
        public Service(System.Management.ManagementObject theObject) {
            if ((CheckIfProperClass(theObject) == true)) {
                PrivateLateBoundObject = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(PrivateLateBoundObject);
                curObj = PrivateLateBoundObject;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        public Service(System.Management.ManagementBaseObject theObject) {
            if ((CheckIfProperClass(theObject) == true)) {
                embeddedObj = theObject;
                PrivateSystemProperties = new ManagementSystemProperties(theObject);
                curObj = embeddedObj;
                isEmbedded = true;
            }
            else {
                throw new System.ArgumentException("Class name does not match.");
            }
        }
        
        // Property returns the namespace of the WMI class.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OriginatingNamespace {
            get {
                return "root\\CimV2";
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ManagementClassName {
            get {
                string strRet = CreatedClassName;
                if ((curObj != null)) {
                    if ((curObj.ClassPath != null)) {
                        strRet = ((string)(curObj["__CLASS"]));
                        if (((strRet == null) 
                                    || (strRet == System.String.Empty))) {
                            strRet = CreatedClassName;
                        }
                    }
                }
                return strRet;
            }
        }
        
        // Property pointing to an embedded object to get System properties of the WMI object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ManagementSystemProperties SystemProperties {
            get {
                return PrivateSystemProperties;
            }
        }
        
        // Property returning the underlying lateBound object.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementBaseObject LateBoundObject {
            get {
                return curObj;
            }
        }
        
        // ManagementScope of the object.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Management.ManagementScope Scope {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Scope;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    PrivateLateBoundObject.Scope = value;
                }
            }
        }
        
        // Property to show the commit behavior for the WMI object. If true, WMI object will be automatically saved after each property modification.(ie. Put() is called after modification of a property).
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AutoCommit {
            get {
                return AutoCommitProp;
            }
            set {
                AutoCommitProp = value;
            }
        }
        
        // The ManagementPath of the underlying WMI object.
        [Browsable(true)]
        public System.Management.ManagementPath Path {
            get {
                if ((isEmbedded == false)) {
                    return PrivateLateBoundObject.Path;
                }
                else {
                    return null;
                }
            }
            set {
                if ((isEmbedded == false)) {
                    if ((CheckIfProperClass(null, value, null) != true)) {
                        throw new System.ArgumentException("Class name does not match.");
                    }
                    PrivateLateBoundObject.Path = value;
                }
            }
        }
        
        // Public static scope property which is used by the various methods.
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static System.Management.ManagementScope StaticScope {
            get {
                return statMgmtScope;
            }
            set {
                statMgmtScope = value;
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAcceptPauseNull {
            get {
                if ((curObj["AcceptPause"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość AcceptPause wskazuje, czy usługa może być wstrzymana.\nWartości: PRAWDA" +
" lub FAŁSZ. Wartość właściwości równa PRAWDA oznacza, że usługa może być wstrzym" +
"ana.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AcceptPause {
            get {
                if ((curObj["AcceptPause"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["AcceptPause"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsAcceptStopNull {
            get {
                if ((curObj["AcceptStop"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość AcceptStop wskazuje, czy usługa może być zatrzymana.\nWartości: PRAWDA " +
"lub FAŁSZ. Wartość właściwości równa PRAWDA oznacza, że usługa może być zatrzyma" +
"na.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool AcceptStop {
            get {
                if ((curObj["AcceptStop"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["AcceptStop"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość Caption jest zwięzłym (ciąg w pojedynczym wierszu) tekstowym opisem ob" +
"iektu.")]
        public string Caption {
            get {
                return ((string)(curObj["Caption"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsCheckPointNull {
            get {
                if ((curObj["CheckPoint"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość CheckPoint określa wartość, którą usługa okresowo zwiększa, aby informować o postępach podczas czasochłonnych operacji uruchamiania, zatrzymywania, wstrzymywania lub kontynuowania. Na przykład, usługa powinna zwiększać tę wartość wykonując poszczególne kroki inicjowania podczas uruchamiania. Program interfejsu użytkownika wywołujący operację na usłudze wykorzystuje tę wartość do śledzenia postępów usługi podczas czasochłonnych operacji. Wartość ta nie jest prawidłowa i powinna być równa zero wówczas, gdy usługa nie ma zaległych operacji uruchamiania, zatrzymywania, wstrzymywania lub kontynuowania.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 CheckPoint {
            get {
                if ((curObj["CheckPoint"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["CheckPoint"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("CreationClassName wskazuje nazwę klasy lub podklasy używanej do tworzenia wystąpi" +
"enia. Kiedy jest używana wraz z innymi kluczowymi właściwościami tej klasy, właś" +
"ciwość ta umożliwia jednoznaczną identyfikację wszystkich wystąpień tej klasy i " +
"jej podklas.")]
        public string CreationClassName {
            get {
                return ((string)(curObj["CreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość Description udostępnia tekstowy opis obiektu. ")]
        public string Description {
            get {
                return ((string)(curObj["Description"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDesktopInteractNull {
            get {
                if ((curObj["DesktopInteract"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość DesktopInteract wskazuje, czy usługa może tworzyć okna na pulpicie lub komunikować się z tymi oknami.
Wartości: PRAWDA lub FAŁSZ. Wartość właściwości równa PRAWDA oznacza, że usługa może tworzyć okna na pulpicie lub komunikować się z tymi oknami.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool DesktopInteract {
            get {
                if ((curObj["DesktopInteract"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["DesktopInteract"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość DisplayName wskazuje nazwę wyświetlacza usługi. Ciąg ten może zawierać maksymalnie 256 znaków. Wielkość liter w nazwie jest zachowana w menedżerze sterowania usługą. Porównania właściwości DisplayName są zawsze wykonywane z uwzględnieniem wielkości liter.
Ograniczenia: Właściwość DisplayName akceptuje taką samą wartość jak właściwość Name.
Przykład: Atdisk")]
        public string DisplayName {
            get {
                return ((string)(curObj["DisplayName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Jeśli w czasie uruchamiania systemu uruchomienie tej usługi nie powiedzie się, parametr ErrorControl przekazuje poziom zagrożenia przez błąd. Wartość wskazuje czynność wykonaną przez program autostart, jeśli wystąpi awaria. Wszystkie błędy są rejestrowane przez system komputerowy. System komputerowy nie powiadamia użytkownika o błędach ""Ignoruj"". Użytkownik jest powiadamiany o błędach ""Normalny"". W razie wystąpienia błędów ""Poważny"", system jest ponownie uruchamiany z wykorzystaniem ostatniej znanej jako poprawnej konfiguracji. Wreszcie w razie wystąpienia błędów ""Krytyczny"", system próbuje uruchomić się ponownie z wykorzystaniem dobrej konfiguracji.")]
        public string ErrorControl {
            get {
                return ((string)(curObj["ErrorControl"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsExitCodeNull {
            get {
                if ((curObj["ExitCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość ExitCode określa kod błędów systemu Win32 definiujący wszystkie problemy występujące podczas uruchamiania lub zatrzymywania usługi. Wartość tej właściwości jest równa ERROR_SERVICE_SPECIFIC_ERROR (1066), jeżeli błąd jest unikatowy dla usługi reprezentowanej przez tę klasę, a informacje o błędzie są dostępne w elemencie ServiceSpecificExitCode. Usługa ustawia wartość tej właściwości równą NO_ERROR podczas pracy i ponownie po prawidłowym zakończeniu.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 ExitCode {
            get {
                if ((curObj["ExitCode"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["ExitCode"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsInstallDateNull {
            get {
                if ((curObj["InstallDate"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość InstallDate jest wartością typu data/godzina wskazującą datę i godzinę" +
" zainstalowania obiektu. Brak tej wartości nie oznacza, że obiekt nie jest zains" +
"talowany.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.DateTime InstallDate {
            get {
                if ((curObj["InstallDate"] != null)) {
                    return ToDateTime(((string)(curObj["InstallDate"])));
                }
                else {
                    return System.DateTime.MinValue;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość Name jednoznacznie identyfikuje usługę i udostępnia wskazanie zarządza" +
"nej funkcji. Funkcja jest opisana szczegółowo we właściwości Description obiektu" +
". ")]
        public string Name {
            get {
                return ((string)(curObj["Name"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość PathName zawiera w pełni kwalifikowaną ścieżkę pliku binarnego usługi," +
" który implementuje usługę.\nPrzykład: \\SystemRoot\\System32\\drivers\\afd.sys")]
        public string PathName {
            get {
                return ((string)(curObj["PathName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsProcessIdNull {
            get {
                if ((curObj["ProcessId"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość ProcessId określa identyfikator procesu usługi.\nPrzykład: 324")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 ProcessId {
            get {
                if ((curObj["ProcessId"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["ProcessId"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsServiceSpecificExitCodeNull {
            get {
                if ((curObj["ServiceSpecificExitCode"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość ServiceSpecificExitCode określa kod błędu usługi dla błędów występujących podczas uruchamiania albo zatrzymywania usługi. Kody zakończenia są zdefiniowane przez usługę reprezentowaną przez tą klasę. Wartość ta jest ustawiana tylko wtedy, gdy wartość ExitCodeproperty jest równa ERROR_SERVICE_SPECIFIC_ERROR, 1066.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 ServiceSpecificExitCode {
            get {
                if ((curObj["ServiceSpecificExitCode"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["ServiceSpecificExitCode"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość ServiceType zapewnia typ usługi dostarczany dla procesów wywołujących." +
"")]
        public string ServiceType {
            get {
                return ((string)(curObj["ServiceType"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsStartedNull {
            get {
                if ((curObj["Started"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Started jest wartością logiczną wskazującą, czy usługa została uruchomiona (PRAWD" +
"A), czy zatrzymana (FAŁSZ).")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public bool Started {
            get {
                if ((curObj["Started"] == null)) {
                    return System.Convert.ToBoolean(0);
                }
                return ((bool)(curObj["Started"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość StartMode przekazuje początkowy tryb usługi podstawowej Win32. Wartość ""Rozruch"" określa sterownik urządzenia uruchamiany przez program ładujący systemu operacyjnego. Wartość ta jest prawidłowa jedynie dla usług sterowników. Wartość ""System"" określa sterownik urządzenia uruchamiany przez funkcję IoInitSystem. Wartość ta jest prawidłowa jedynie dla usług sterowników. Wartość ""Automatyczny"" określa, że usługa ma być uruchamiana automatycznie przez menedżera sterowania usługami podczas uruchamiania systemu. Wartość ""Ręczny"" określa, że usługa ma być uruchamiana przez menedżera sterowania usługami, gdy proces wywołuje funkcję StartService. Wartość ""Wyłączony"" określa, że usługi nie można już uruchomić.")]
        public string StartMode {
            get {
                return ((string)(curObj["StartMode"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość StartName wskazuje nazwę konta, pod którym działa usługa. Zależnie od typu usługi nazwa konta może mieć postać ""NazwaDomeny\NazwaUżytkownika"".Proces usługi, gdy działa, jest logowany przy użyciu jednej z tych dwóch postaci.  Jeśli konto należy do wbudowanej domeny, można określić "".\NazwęUżytkownika"". Jeśli podana jest wartość NULL, usługa będzie logowana jako konto LocalSystem. Dla jądra lub sterowników na poziomie systemu właściwość StartName zawiera nazwę obiektu sterownika (to jest \SystemPlików\Rdr lub \Sterownik\Xns), który jest używany przez system wejścia/wyjścia (We/Wy) do ładowania sterownika urządzenia. Dodatkowo, jeśli podana jest wartość NULL, sterownik działa z domyślną nazwą obiektu utworzoną przez system We/Wy na podstawie nazwy usługi.
Przykład: DWDOM\Admin.")]
        public string StartName {
            get {
                return ((string)(curObj["StartName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Właściwość Stan wskazuje bieżący stan usługi podstawowej.")]
        public string State {
            get {
                return ((string)(curObj["State"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość Stan jest ciągiem wskazującym bieżący stan obiektu. Można zdefiniować rozmaite stany operacyjne. Stany działania to ""OK"", ""Obniżony"" i ""P. awaria"". Stan ""P. awaria"" wskazuje, że element może działać poprawnie, ale w najbliższej przyszłości może nastąpić awaria. Przykładem może być dysk twardy SMART. Można także określić stany braku działania. Są nimi ""Błąd"", ""Uruchamianie"", ""Zatrzymywanie"" i ""Serwis"". Ostatni, ""Serwis"", może się odnosić do odświeżania dublowania dysku, ponownego ładowania listy uprawnień użytkowników lub innych prac administracyjnych. Nie każda taka praca jest typu on-line, a jednak zarządzany element nie jest w stanie ""OK"" ani żadnym innym.")]
        public string Status {
            get {
                return ((string)(curObj["Status"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Nazwa typu systemu, na którym jest uruchomiona ta usługa.")]
        public string SystemCreationClassName {
            get {
                return ((string)(curObj["SystemCreationClassName"]));
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Nazwa systemu, na którym jest uruchomiona ta usługa.")]
        public string SystemName {
            get {
                return ((string)(curObj["SystemName"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsTagIdNull {
            get {
                if ((curObj["TagId"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość TagId określa unikatową wartość znacznika dla tej usługi w grupie. Wartość 0 wskazuje, że usługa nie ma przypisanego znacznika. Znacznik może być używany do ustalania kolejności uruchamiania usług w załadowanej grupie przez podanie wektora kolejności znaczników w kluczu rejestru: HKEY_LOCAL_MACHINE\System\CurrentControlSet\Control\GroupOrderList. Znaczniki są szacowane jedynie dla usług typu sterownik jądra i sterownik systemu plików mających tryby uruchamiania ""Rozruch"" lub ""System"".")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 TagId {
            get {
                if ((curObj["TagId"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["TagId"]));
            }
        }
        
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsWaitHintNull {
            get {
                if ((curObj["WaitHint"] == null)) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description(@"Właściwość WaitHint określa szacowany czas (w milisekundach) potrzebny na oczekiwanie na uruchomienie, zatrzymanie, wstrzymanie lub kontynuowanie operacji. Po upływie tegoczasu usługa wykonuje swoje następne wywołanie funkcji SetServiceStatus ze zwiększoną wartością CheckPoint albo ze zmienioną wartością CurrentState. Jeśli określony przez właściwość WaitHint czas upłynął, a wartość CheckPoint nie została zwiększona lub nie zmieniła się wartość CurrentState, menedżer sterowania usługami lub program sterujący usługami stwierdza, że wystąpił błąd.")]
        [TypeConverter(typeof(WMIValueTypeConverter))]
        public System.UInt32 WaitHint {
            get {
                if ((curObj["WaitHint"] == null)) {
                    return System.Convert.ToUInt32(0);
                }
                return ((System.UInt32)(curObj["WaitHint"]));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementScope mgmtScope, System.Management.ManagementPath path, System.Management.ObjectGetOptions OptionsParam) {
            if (((path != null) 
                        && (System.String.Compare(path.ClassName, ManagementClassName, true, CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                return CheckIfProperClass(new System.Management.ManagementObject(mgmtScope, path, OptionsParam));
            }
        }
        
        private bool CheckIfProperClass(System.Management.ManagementBaseObject theObj) {
            if (((theObj != null) 
                        && (System.String.Compare(((string)(theObj["__CLASS"])), ManagementClassName, true, CultureInfo.InvariantCulture) == 0))) {
                return true;
            }
            else {
                System.Array parentClasses = ((System.Array)(theObj["__DERIVATION"]));
                if ((parentClasses != null)) {
                    Int32 count = 0;
                    for (count = 0; (count < parentClasses.Length); count = (count + 1)) {
                        if ((System.String.Compare(((string)(parentClasses.GetValue(count))), ManagementClassName, true, CultureInfo.InvariantCulture) == 0)) {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        private bool ShouldSerializeAcceptPause() {
            if ((IsAcceptPauseNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeAcceptStop() {
            if ((IsAcceptStopNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeCheckPoint() {
            if ((IsCheckPointNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeDesktopInteract() {
            if ((IsDesktopInteractNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeExitCode() {
            if ((IsExitCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        // Converts a given datetime in DMTF format to System.DateTime object.
        static System.DateTime ToDateTime(string dmtfDate) {
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
            if ((dmtf == null)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length == 0)) {
                throw new System.ArgumentOutOfRangeException();
            }
            if ((dmtf.Length != 25)) {
                throw new System.ArgumentOutOfRangeException();
            }
            try {
                tempString = dmtf.Substring(0, 4);
                if (("****" != tempString)) {
                    year = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(4, 2);
                if (("**" != tempString)) {
                    month = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(6, 2);
                if (("**" != tempString)) {
                    day = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(8, 2);
                if (("**" != tempString)) {
                    hour = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(10, 2);
                if (("**" != tempString)) {
                    minute = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(12, 2);
                if (("**" != tempString)) {
                    second = System.Int32.Parse(tempString);
                }
                tempString = dmtf.Substring(15, 6);
                if (("******" != tempString)) {
                    ticks = (System.Int64.Parse(tempString) 
                                * (System.TimeSpan.TicksPerMillisecond / 1000));
                }
                if (((((((((year < 0) 
                            || (month < 0)) 
                            || (day < 0)) 
                            || (hour < 0)) 
                            || (minute < 0)) 
                            || (minute < 0)) 
                            || (second < 0)) 
                            || (ticks < 0))) {
                    throw new System.ArgumentOutOfRangeException();
                }
            }
            catch (System.Exception e) {
                e = e;
                throw new System.ArgumentOutOfRangeException();
            }
            datetime = new System.DateTime(year, month, day, hour, minute, second, 0);
            datetime = datetime.AddTicks(ticks);
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(datetime);
            int UTCOffset = 0;
            long OffsetToBeAdjusted = 0;
            long OffsetMins = (tickOffset.Ticks / System.TimeSpan.TicksPerMinute);
            tempString = dmtf.Substring(22, 3);
            if ((tempString != "***")) {
                tempString = dmtf.Substring(21, 4);
                try {
                    UTCOffset = System.Int32.Parse(tempString);
                }
                catch (System.Exception e) {
                    e = e;
                    throw new System.ArgumentOutOfRangeException();
                }
                OffsetToBeAdjusted = (OffsetMins - UTCOffset);
                datetime = datetime.AddMinutes(OffsetToBeAdjusted);
            }
            return datetime;
        }
        
        // Converts a given System.DateTime object to DMTF datetime format.
        static string ToDmtfDateTime(System.DateTime date) {
            string utcString = System.String.Empty;
            System.TimeSpan tickOffset = System.TimeZone.CurrentTimeZone.GetUtcOffset(date);
            long OffsetMins = (tickOffset.Ticks / System.TimeSpan.TicksPerMinute);
            if ((System.Math.Abs(OffsetMins) > 999)) {
                date = date.ToUniversalTime();
                utcString = "+000";
            }
            else {
                if ((tickOffset.Ticks >= 0)) {
                    utcString = ("+" + ((tickOffset.Ticks / System.TimeSpan.TicksPerMinute)).ToString().PadLeft(3, '0'));
                }
                else {
                    string strTemp = OffsetMins.ToString();
                    utcString = ("-" + strTemp.Substring(1, (strTemp.Length - 1)).PadLeft(3, '0'));
                }
            }
            string dmtfDateTime = date.Year.ToString().PadLeft(4, '0');
            dmtfDateTime = (dmtfDateTime + date.Month.ToString().PadLeft(2, '0'));
            dmtfDateTime = (dmtfDateTime + date.Day.ToString().PadLeft(2, '0'));
            dmtfDateTime = (dmtfDateTime + date.Hour.ToString().PadLeft(2, '0'));
            dmtfDateTime = (dmtfDateTime + date.Minute.ToString().PadLeft(2, '0'));
            dmtfDateTime = (dmtfDateTime + date.Second.ToString().PadLeft(2, '0'));
            dmtfDateTime = (dmtfDateTime + ".");
            System.DateTime dtTemp = new System.DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0);
            long microsec = (((date.Ticks - dtTemp.Ticks) 
                        * 1000) 
                        / System.TimeSpan.TicksPerMillisecond);
            string strMicrosec = microsec.ToString();
            if ((strMicrosec.Length > 6)) {
                strMicrosec = strMicrosec.Substring(0, 6);
            }
            dmtfDateTime = (dmtfDateTime + strMicrosec.PadLeft(6, '0'));
            dmtfDateTime = (dmtfDateTime + utcString);
            return dmtfDateTime;
        }
        
        private bool ShouldSerializeInstallDate() {
            if ((IsInstallDateNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeProcessId() {
            if ((IsProcessIdNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeServiceSpecificExitCode() {
            if ((IsServiceSpecificExitCodeNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeStarted() {
            if ((IsStartedNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeTagId() {
            if ((IsTagIdNull == false)) {
                return true;
            }
            return false;
        }
        
        private bool ShouldSerializeWaitHint() {
            if ((IsWaitHintNull == false)) {
                return true;
            }
            return false;
        }
        
        [Browsable(true)]
        public void CommitObject() {
            if ((isEmbedded == false)) {
                PrivateLateBoundObject.Put();
            }
        }
        
        private static string ConstructPath(string keyName) {
            string strPath = "root\\CimV2:Win32_Service";
            strPath = (strPath 
                        + (".Name=" 
                        + ("\"" 
                        + (keyName + "\""))));
            return strPath;
        }
        
        // Different overloads of GetInstances() help in enumerating instances of the WMI class.
        public static ServiceCollection GetInstances() {
            return GetInstances(((System.Management.ManagementScope)(null)), ((System.Management.EnumerationOptions)(null)));
        }
        
        public static ServiceCollection GetInstances(string condition) {
            return GetInstances(null, condition, null);
        }
        
        public static ServiceCollection GetInstances(System.String [] selectedProperties) {
            return GetInstances(null, null, selectedProperties);
        }
        
        public static ServiceCollection GetInstances(string condition, System.String [] selectedProperties) {
            return GetInstances(null, condition, selectedProperties);
        }
        
        public static ServiceCollection GetInstances(System.Management.ManagementScope mgmtScope, System.Management.EnumerationOptions enumOptions) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementPath pathObj = new System.Management.ManagementPath();
            pathObj.ClassName = "Win32_Service";
            pathObj.NamespacePath = "root\\CimV2";
            System.Management.ManagementClass clsObject = new System.Management.ManagementClass(mgmtScope, pathObj, null);
            if ((enumOptions == null)) {
                enumOptions = new System.Management.EnumerationOptions();
                enumOptions.EnsureLocatable = true;
            }
            return new ServiceCollection(clsObject.GetInstances(enumOptions));
        }
        
        public static ServiceCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition) {
            return GetInstances(mgmtScope, condition, null);
        }
        
        public static ServiceCollection GetInstances(System.Management.ManagementScope mgmtScope, System.String [] selectedProperties) {
            return GetInstances(mgmtScope, null, selectedProperties);
        }
        
        public static ServiceCollection GetInstances(System.Management.ManagementScope mgmtScope, string condition, System.String [] selectedProperties) {
            if ((mgmtScope == null)) {
                if ((statMgmtScope == null)) {
                    mgmtScope = new System.Management.ManagementScope();
                    mgmtScope.Path.NamespacePath = "root\\CimV2";
                }
                else {
                    mgmtScope = statMgmtScope;
                }
            }
            System.Management.ManagementObjectSearcher ObjectSearcher = new System.Management.ManagementObjectSearcher(mgmtScope, new SelectQuery("Win32_Service", condition, selectedProperties));
            System.Management.EnumerationOptions enumOptions = new System.Management.EnumerationOptions();
            enumOptions.EnsureLocatable = true;
            ObjectSearcher.Options = enumOptions;
            return new ServiceCollection(ObjectSearcher.Get());
        }
        
        [Browsable(true)]
        public static Service CreateInstance() {
            System.Management.ManagementScope mgmtScope = null;
            if ((statMgmtScope == null)) {
                mgmtScope = new System.Management.ManagementScope();
                mgmtScope.Path.NamespacePath = CreatedWmiNamespace;
            }
            else {
                mgmtScope = statMgmtScope;
            }
            System.Management.ManagementPath mgmtPath = new System.Management.ManagementPath(CreatedClassName);
            return new Service(new System.Management.ManagementClass(mgmtScope, mgmtPath, null).CreateInstance());
        }
        
        [Browsable(true)]
        public void Delete() {
            PrivateLateBoundObject.Delete();
        }
        
        public System.UInt32 Change(bool DesktopInteract, string DisplayName, System.Byte ErrorControl, string LoadOrderGroup, string[] LoadOrderGroupDependencies, string PathName, string[] ServiceDependencies, System.Byte ServiceType, string StartMode, string StartName, string StartPassword) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("Change");
                inParams["DesktopInteract"] = DesktopInteract;
                inParams["DisplayName"] = DisplayName;
                inParams["ErrorControl"] = ErrorControl;
                inParams["LoadOrderGroup"] = LoadOrderGroup;
                inParams["LoadOrderGroupDependencies"] = LoadOrderGroupDependencies;
                inParams["PathName"] = PathName;
                inParams["ServiceDependencies"] = ServiceDependencies;
                inParams["ServiceType"] = ServiceType;
                inParams["StartMode"] = StartMode;
                inParams["StartName"] = StartName;
                inParams["StartPassword"] = StartPassword;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Change", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 ChangeStartMode(string StartMode) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("ChangeStartMode");
                inParams["StartMode"] = StartMode;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ChangeStartMode", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 Create(bool DesktopInteract, string DisplayName, System.Byte ErrorControl, string LoadOrderGroup, string[] LoadOrderGroupDependencies, string Name, string PathName, string[] ServiceDependencies, System.Byte ServiceType, string StartMode, string StartName, string StartPassword) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("Create");
                inParams["DesktopInteract"] = DesktopInteract;
                inParams["DisplayName"] = DisplayName;
                inParams["ErrorControl"] = ErrorControl;
                inParams["LoadOrderGroup"] = LoadOrderGroup;
                inParams["LoadOrderGroupDependencies"] = LoadOrderGroupDependencies;
                inParams["Name"] = Name;
                inParams["PathName"] = PathName;
                inParams["ServiceDependencies"] = ServiceDependencies;
                inParams["ServiceType"] = ServiceType;
                inParams["StartMode"] = StartMode;
                inParams["StartName"] = StartName;
                inParams["StartPassword"] = StartPassword;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Create", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 Delete0() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("Delete", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 InterrogateService() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("InterrogateService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 PauseService() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("PauseService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 ResumeService() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("ResumeService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 StartService() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StartService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 StopService() {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("StopService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        public System.UInt32 UserControlService(System.Byte ControlCode) {
            if ((isEmbedded == false)) {
                System.Management.ManagementBaseObject inParams = null;
                inParams = PrivateLateBoundObject.GetMethodParameters("UserControlService");
                inParams["ControlCode"] = ControlCode;
                System.Management.ManagementBaseObject outParams = PrivateLateBoundObject.InvokeMethod("UserControlService", inParams, null);
                return System.Convert.ToUInt32(outParams.Properties["ReturnValue"].Value);
            }
            else {
                return System.Convert.ToUInt32(0);
            }
        }
        
        // Enumerator implementation for enumerating instances of the class.
        public class ServiceCollection : object, ICollection {
            
            private ManagementObjectCollection ObjectCollection;
            
            public ServiceCollection(ManagementObjectCollection objCollection) {
                ObjectCollection = objCollection;
            }
            
            public int Count {
                get {
                    return ObjectCollection.Count;
                }
            }
            
            public bool IsSynchronized {
                get {
                    return ObjectCollection.IsSynchronized;
                }
            }
            
            public object SyncRoot {
                get {
                    return this;
                }
            }
            
            public void CopyTo(System.Array array, int index) {
                ObjectCollection.CopyTo(array, index);
                int nCtr;
                for (nCtr = 0; (nCtr < array.Length); nCtr = (nCtr + 1)) {
                    array.SetValue(new Service(((System.Management.ManagementObject)(array.GetValue(nCtr)))), nCtr);
                }
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return new ServiceEnumerator(ObjectCollection.GetEnumerator());
            }
            
            public class ServiceEnumerator : object, System.Collections.IEnumerator {
                
                private ManagementObjectCollection.ManagementObjectEnumerator ObjectEnumerator;
                
                public ServiceEnumerator(ManagementObjectCollection.ManagementObjectEnumerator objEnum) {
                    ObjectEnumerator = objEnum;
                }
                
                public object Current {
                    get {
                        return new Service(((System.Management.ManagementObject)(ObjectEnumerator.Current)));
                    }
                }
                
                public bool MoveNext() {
                    return ObjectEnumerator.MoveNext();
                }
                
                public void Reset() {
                    ObjectEnumerator.Reset();
                }
            }
        }
        
        // TypeConverter to handle null values for ValueType properties
        public class WMIValueTypeConverter : TypeConverter {
            
            private TypeConverter baseConverter;
            
            public WMIValueTypeConverter(System.Type baseType) {
                baseConverter = TypeDescriptor.GetConverter(baseType);
            }
            
            public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type srcType) {
                return baseConverter.CanConvertFrom(context, srcType);
            }
            
            public override bool CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) {
                return baseConverter.CanConvertTo(context, destinationType);
            }
            
            public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value) {
                return baseConverter.ConvertFrom(context, culture, value);
            }
            
            public override object CreateInstance(System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary dictionary) {
                return baseConverter.CreateInstance(context, dictionary);
            }
            
            public override bool GetCreateInstanceSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetCreateInstanceSupported(context);
            }
            
            public override PropertyDescriptorCollection GetProperties(System.ComponentModel.ITypeDescriptorContext context, object value, System.Attribute[] attributeVar) {
                return baseConverter.GetProperties(context, value, attributeVar);
            }
            
            public override bool GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetPropertiesSupported(context);
            }
            
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValues(context);
            }
            
            public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesExclusive(context);
            }
            
            public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
                return baseConverter.GetStandardValuesSupported(context);
            }
            
            public override object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, System.Type destinationType) {
                if ((context != null)) {
                    if ((context.PropertyDescriptor.ShouldSerializeValue(context.Instance) == false)) {
                        return "";
                    }
                }
                return baseConverter.ConvertTo(context, culture, value, destinationType);
            }
        }
        
        // Embedded class to represent WMI system Properties.
        [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public class ManagementSystemProperties {
            
            private System.Management.ManagementBaseObject PrivateLateBoundObject;
            
            public ManagementSystemProperties(System.Management.ManagementBaseObject ManagedObject) {
                PrivateLateBoundObject = ManagedObject;
            }
            
            [Browsable(true)]
            public int GENUS {
                get {
                    return ((int)(PrivateLateBoundObject["__GENUS"]));
                }
            }
            
            [Browsable(true)]
            public string CLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__CLASS"]));
                }
            }
            
            [Browsable(true)]
            public string SUPERCLASS {
                get {
                    return ((string)(PrivateLateBoundObject["__SUPERCLASS"]));
                }
            }
            
            [Browsable(true)]
            public string DYNASTY {
                get {
                    return ((string)(PrivateLateBoundObject["__DYNASTY"]));
                }
            }
            
            [Browsable(true)]
            public string RELPATH {
                get {
                    return ((string)(PrivateLateBoundObject["__RELPATH"]));
                }
            }
            
            [Browsable(true)]
            public int PROPERTY_COUNT {
                get {
                    return ((int)(PrivateLateBoundObject["__PROPERTY_COUNT"]));
                }
            }
            
            [Browsable(true)]
            public string[] DERIVATION {
                get {
                    return ((string[])(PrivateLateBoundObject["__DERIVATION"]));
                }
            }
            
            [Browsable(true)]
            public string SERVER {
                get {
                    return ((string)(PrivateLateBoundObject["__SERVER"]));
                }
            }
            
            [Browsable(true)]
            public string NAMESPACE {
                get {
                    return ((string)(PrivateLateBoundObject["__NAMESPACE"]));
                }
            }
            
            [Browsable(true)]
            public string PATH {
                get {
                    return ((string)(PrivateLateBoundObject["__PATH"]));
                }
            }
        }
    }
}
