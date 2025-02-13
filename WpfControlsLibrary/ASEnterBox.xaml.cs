﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfControlsLibrary
{
    /// <summary>
    /// Interaction logic for ASEnterBox.xaml
    /// </summary>
    /// 

    public partial class ASEnterBox : UserControl
    {
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(int), typeof(ASEnterBox));
        public int Size
        {
            get
            {
                return (int)base.GetValue(SizeProperty);
            }
            set
            {
                base.SetValue(SizeProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonSizeProperty = DependencyProperty.Register("ButtonSize", typeof(int), typeof(ASEnterBox));
        public int ButtonSize
        {
            get
            {
                return (int)base.GetValue(ButtonSizeProperty);
            }
            set
            {
                base.SetValue(ButtonSizeProperty, value);
            }
        }

        public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(ASEnterBox));
        public string FieldName
        {
            get
            {
                return (string)base.GetValue(FieldNameProperty);
            }
            set
            {
                base.SetValue(FieldNameProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ASEnterBox));
        public string Text
        {
            get
            {
                return (string)base.GetValue(TextProperty);
            }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty CaseProperty = DependencyProperty.Register("Case", typeof(string), typeof(ASEnterBox));
        public string Case
        {
            get
            {
                return (string)base.GetValue(CaseProperty);
            }
            set
            {
                base.SetValue(CaseProperty, value);
            }
        }
        
        public static readonly DependencyProperty KeyTrapsProperty = DependencyProperty.Register("KeyTraps", typeof(string), typeof(ASEnterBox));
        public string KeyTraps
        {
            get
            {
                return (string)base.GetValue(KeyTrapsProperty);
            }
            set
            {
                base.SetValue(KeyTrapsProperty, value);
            }
        }

        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ASEnterBox));
        public FontWeight FontWeight
        {
            get
            {
                return (FontWeight)base.GetValue(FontWeightProperty);
            }
            set
            {
                base.SetValue(FontWeightProperty, value);
            }
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(ASEnterBox));
        public Brush Background
        {
            get
            {
                return (Brush)base.GetValue(BackgroundProperty);
            }
            set
            {
                base.SetValue(BackgroundProperty, value);
            }
        }
        
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ASEnterBox));
        public Brush Foreground
        {
            get
            {
                return (Brush)base.GetValue(ForegroundProperty);
            }
            set
            {
                base.SetValue(ForegroundProperty, value);
            }
        }

        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ASEnterBox));
        public bool IsReadOnly
        {
            get
            {
                return (bool)base.GetValue(IsReadOnlyProperty);
            }
            set
            {
                base.SetValue(IsReadOnlyProperty, value);
            }
        }


        public ASEnterBox()
        {
            InitializeComponent();
        }


        bool loaded = false;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                enterBoxTextBox.Size = Size == 0 ? Int32.MaxValue : Size;
                enterBoxTextBox.Text = Text;
                enterBoxTextBox.CharacterCasing = Case?.ToLower() == "up" ? CharacterCasing.Upper : (Case?.ToLower() == "down" ? CharacterCasing.Lower : CharacterCasing.Normal);
                enterBoxTextBox.FontWeight = FontWeight;

                enterBoxTextBox.IsReadOnly = IsReadOnly;

                enterBoxTextBox.Background = Background == null ? new SolidColorBrush() { Color = Colors.White } : Background;
                enterBoxTextBox.Foreground = Foreground == null ? new SolidColorBrush() { Color = Colors.Black } : Foreground;

                enterBoxButton.Width = ButtonSize;

                loaded = true;
            }
        }
    }
}
