﻿#pragma checksum "..\..\..\..\RealtimeStatus\ElectricShow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A313F4B9489BEB353B444BF252DCDDAD"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using FBGEMSystem.RealtimeStatus;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Visifire.Charts;


namespace FBGEMSystem.RealtimeStatus {
    
    
    /// <summary>
    /// ElectricShow
    /// </summary>
    public partial class ElectricShow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_Elc;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox_typeNum;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_channel1;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox_CHNum;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Visifire.Charts.Chart chart;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Visifire.Charts.Axis SingleAy;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Visifire.Charts.Axis SingleAx;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Visifire.Charts.DataSeries ds;
        
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
            System.Uri resourceLocater = new System.Uri("/FBGEMSystem;component/realtimestatus/electricshow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
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
            
            #line 9 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
            ((FBGEMSystem.RealtimeStatus.ElectricShow)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbl_Elc = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.comboBox_typeNum = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
            this.comboBox_typeNum.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Type_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.lbl_channel1 = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.comboBox_CHNum = ((System.Windows.Controls.ComboBox)(target));
            
            #line 27 "..\..\..\..\RealtimeStatus\ElectricShow.xaml"
            this.comboBox_CHNum.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CHNum_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.chart = ((Visifire.Charts.Chart)(target));
            return;
            case 8:
            this.SingleAy = ((Visifire.Charts.Axis)(target));
            return;
            case 9:
            this.SingleAx = ((Visifire.Charts.Axis)(target));
            return;
            case 10:
            this.ds = ((Visifire.Charts.DataSeries)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

