﻿#pragma checksum "..\..\..\Pages\CreateLeaguePage - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "72FE803DD4343FC5D91C413B0B97F47A99E45C4908FFF381BB69539B9F1BFEBC"
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
using TableFootball.Frames;
using TableFootball.Pages;


namespace TableFootball.Pages {
    
    
    /// <summary>
    /// CreateLeaguePage
    /// </summary>
    public partial class CreateLeaguePage : TableFootball.Frames.FramedPage, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridContainer;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBack;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxLeagueName;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxTeamsInLeague;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBoxTeamsNotInLeague;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCommit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TableFootball;component/pages/createleaguepage%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.gridContainer = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.btnBack = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.txtBoxLeagueName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.listBoxTeamsInLeague = ((System.Windows.Controls.ListBox)(target));
            
            #line 41 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
            this.listBoxTeamsInLeague.Drop += new System.Windows.DragEventHandler(this.listBoxTeamsInLeague_Drop);
            
            #line default
            #line hidden
            return;
            case 5:
            this.listBoxTeamsNotInLeague = ((System.Windows.Controls.ListBox)(target));
            
            #line 49 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
            this.listBoxTeamsNotInLeague.Drop += new System.Windows.DragEventHandler(this.listBoxTeamsNotInLeague_Drop);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCommit = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\..\Pages\CreateLeaguePage - Copy.xaml"
            this.btnCommit.Click += new System.Windows.RoutedEventHandler(this.btnCommit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

