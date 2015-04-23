using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Alsolos Commons")]
[assembly: AssemblyProduct("Alsolos.Commons")]
[assembly: AssemblyDescription("Common types used by all Alsolos products.")]
[assembly: AssemblyCopyright("Copyright © Heiner Suter 2014")]
[assembly: AssemblyVersion("1.0.*")]
[assembly: CLSCompliant(true)]
[assembly: NeutralResourcesLanguageAttribute("en")]
[assembly: ComVisible(false)]

[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Behaviors.MultiselectBehavior")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.Enabled")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.FindInSelector")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.HierarchicalDataGrid")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.NoAutoSize")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.Progress")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Controls.SimpleStretchPanel")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Mvvm.Converters")]
[assembly: XmlnsDefinition("http://alsolos.ch/Commons", "Alsolos.Commons.Resources")]

[assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)]
