﻿#pragma checksum "..\..\..\TestPanel2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "76C19BEFEC1A07B1CA5C345608BC4A4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.18034
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Surface.Presentation;
using Microsoft.Surface.Presentation.Controls;
using Microsoft.Surface.Presentation.Controls.Primitives;
using Microsoft.Surface.Presentation.Controls.TouchVisualizations;
using Microsoft.Surface.Presentation.Input;
using Microsoft.Surface.Presentation.Palettes;
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


namespace Prototype_05 {
    
    
    /// <summary>
    /// TestPanel2
    /// </summary>
    public partial class TestPanel2 : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\TestPanel2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Prototype_05.TestPanel2 UserControl;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\TestPanel2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SolvingGrid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\TestPanel2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle ConfirmRectangle;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\TestPanel2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Surface.Presentation.Controls.SurfaceButton ConfirmButton;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\TestPanel2.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ConfirmMeasureImage;
        
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
            System.Uri resourceLocater = new System.Uri("/Prototype_05;component/testpanel2.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TestPanel2.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.UserControl = ((Prototype_05.TestPanel2)(target));
            return;
            case 2:
            this.SolvingGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 12 "..\..\..\TestPanel2.xaml"
            this.SolvingGrid.PreviewTouchDown += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OptionTouchDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\TestPanel2.xaml"
            this.SolvingGrid.PreviewTouchMove += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OptionTouchMove);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\TestPanel2.xaml"
            this.SolvingGrid.PreviewTouchUp += new System.EventHandler<System.Windows.Input.TouchEventArgs>(this.OptionTouchUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ConfirmRectangle = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.ConfirmButton = ((Microsoft.Surface.Presentation.Controls.SurfaceButton)(target));
            return;
            case 5:
            this.ConfirmMeasureImage = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

