﻿#pragma checksum "..\..\..\Stagiaire.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "945707B88CFABFEAF55989E952081133BD09F87B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ProjetHarold;
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


namespace ProjetHarold {
    
    
    /// <summary>
    /// Stagiaire
    /// </summary>
    public partial class Stagiaire : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 97 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HomeBtn;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddBtn;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifierBtn;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteBtn;
        
        #line default
        #line hidden
        
        
        #line 131 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ProgrammeComboBox;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateNaissancePicker;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PrenomTextbox;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NomFamilleTextBox;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView laliste;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SexeComboBox;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\Stagiaire.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ResetBTN;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ProjetHarold;component/stagiaire.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Stagiaire.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HomeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 97 "..\..\..\Stagiaire.xaml"
            this.HomeBtn.Click += new System.Windows.RoutedEventHandler(this.HomeBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AddBtn = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\Stagiaire.xaml"
            this.AddBtn.Click += new System.Windows.RoutedEventHandler(this.AddBtn_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ModifierBtn = ((System.Windows.Controls.Button)(target));
            
            #line 117 "..\..\..\Stagiaire.xaml"
            this.ModifierBtn.Click += new System.Windows.RoutedEventHandler(this.ModifierBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DeleteBtn = ((System.Windows.Controls.Button)(target));
            
            #line 122 "..\..\..\Stagiaire.xaml"
            this.DeleteBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ProgrammeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 131 "..\..\..\Stagiaire.xaml"
            this.ProgrammeComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ProgrammeComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DateNaissancePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.PrenomTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.NomFamilleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.laliste = ((System.Windows.Controls.ListView)(target));
            return;
            case 10:
            this.SexeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.ResetBTN = ((System.Windows.Controls.Button)(target));
            
            #line 166 "..\..\..\Stagiaire.xaml"
            this.ResetBTN.Click += new System.Windows.RoutedEventHandler(this.ResetBTN_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

