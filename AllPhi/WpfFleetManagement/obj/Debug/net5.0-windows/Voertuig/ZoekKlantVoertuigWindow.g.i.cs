// Updated by XamlIntelliSenseFileGenerator 22/12/2021 12:16:57
#pragma checksum "..\..\..\..\Voertuig\ZoekKlantVoertuigWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C5064E4287B4738C2F3A112B0E9F7E4B98652244"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfFleetManagement.Voertuig;


namespace WpfFleetManagement.Voertuig {


    /// <summary>
    /// ZoekKlantVoertuigWindow
    /// </summary>
    public partial class ZoekKlantVoertuigWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfFleetManagement;V1.0.0.0;component/voertuig/zoekklantvoertuigwindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Voertuig\ZoekKlantVoertuigWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.TextBox Filter_VoornaamTextbox;
        internal System.Windows.Controls.TextBox Filter_NaamTextbox;
        internal System.Windows.Controls.DatePicker Filter_GeboortedatumDatePicker;
        internal System.Windows.Controls.TextBox Filter_RijksregisternummerTextbox;
        internal System.Windows.Controls.TextBox Filter_BestuurderIdTextbox;
        internal System.Windows.Controls.Button FilterButton;
        internal System.Windows.Controls.DataGrid DatagridBestuurder;
    }
}

