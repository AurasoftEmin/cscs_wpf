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
    /// Interaction logic for ASNumericBox.xaml
    /// </summary>
    public partial class ASNumericBox : UserControl
    {

        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(int?), typeof(ASNumericBox));
        public int? Size
        {
            get
            {
                return (int?)base.GetValue(SizeProperty);
            }
            set
            {
                base.SetValue(SizeProperty, value);
            }
        }

        public static readonly DependencyProperty DecProperty = DependencyProperty.Register("Dec", typeof(int?), typeof(ASNumericBox));
        public int? Dec
        {
            get
            {
                return (int?)base.GetValue(DecProperty);
            }
            set
            {
                base.SetValue(DecProperty, value);
            }
        }
        
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double?), typeof(ASNumericBox));
        public double? MinValue
        {
            get
            {
                return (double?)base.GetValue(MinValueProperty);
            }
            set
            {
                base.SetValue(MinValueProperty, value);
            }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double?), typeof(ASNumericBox));
        public double? MaxValue
        {
            get
            {
                return (double?)base.GetValue(MaxValueProperty);
            }
            set
            {
                base.SetValue(MaxValueProperty, value);
            }
        }

        public static readonly DependencyProperty ButtonSizeProperty = DependencyProperty.Register("ButtonSize", typeof(int), typeof(ASNumericBox));
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
        
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ASNumericBox));
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

        public static readonly DependencyProperty ThousandsProperty = DependencyProperty.Register("Thousands", typeof(bool), typeof(ASNumericBox));
        public bool Thousands
        {
            get
            {
                return (bool)base.GetValue(ThousandsProperty);
            }
            set
            {
                base.SetValue(ThousandsProperty, value);
            }
        }

        
        public static readonly DependencyProperty FieldNameProperty = DependencyProperty.Register("FieldName", typeof(string), typeof(ASNumericBox));
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

        public static readonly DependencyProperty KeyTrapsProperty = DependencyProperty.Register("KeyTraps", typeof(string), typeof(ASNumericBox));
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

        public static readonly DependencyProperty FontWeightProperty = DependencyProperty.Register("FontWeight", typeof(FontWeight), typeof(ASNumericBox));
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

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Brush), typeof(ASNumericBox));
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

        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register("Foreground", typeof(Brush), typeof(ASNumericBox));
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
        
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ASNumericBox));
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
        
        public static readonly DependencyProperty IsInGridProperty = DependencyProperty.Register("IsInGrid", typeof(bool), typeof(ASNumericBox));
        public bool IsInGrid
        {
            get
            {
                return (bool)base.GetValue(IsInGridProperty);
            }
            set
            {
                base.SetValue(IsInGridProperty, value);
            }
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(ASNumericBox));
        public string Value
        {
            get
            {
                return (string)base.GetValue(ValueProperty);
            }
            set
            {
                base.SetValue(ValueProperty, value);
            }
        }
        
        public static readonly DependencyProperty HorizontalContentAlignmentProperty = DependencyProperty.Register("HorizontalContentAlignment", typeof(HorizontalAlignment), typeof(ASNumericBox));
        public HorizontalAlignment HorizontalContentAlignment
        {
            get
            {
                return (HorizontalAlignment)base.GetValue(HorizontalContentAlignmentProperty);
            }
            set
            {
                base.SetValue(HorizontalContentAlignmentProperty, value);
            }
        }


        public ASNumericBox()
        {
            InitializeComponent();
        }



        bool loaded = false;


        //routed event
        public static readonly RoutedEvent ButtonClickEvent = EventManager.RegisterRoutedEvent(
        "ButtonClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ASNumericBox));

        public event RoutedEventHandler ButtonClick
        {
            add { AddHandler(ButtonClickEvent, value); }
            remove { RemoveHandler(ButtonClickEvent, value); }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!loaded)
            {
                numBoxTextBox.Size = Size == null ? 500 : (int)Size;
                numBoxTextBox.Dec = Dec == null ? 0 : (int)Dec;
                numBoxTextBox.MinValue = MinValue != null ? (double)MinValue : double.MinValue;
                numBoxTextBox.MaxValue = MaxValue != null ? (double)MaxValue : double.MaxValue;

                numBoxTextBox.IsReadOnly = this.IsReadOnly;
                numBoxTextBox.IsTabStop = this.IsTabStop;

                ////novo
                Binding bind = new Binding("Value");
                bind.Mode = BindingMode.TwoWay;
                bind.UpdateSourceTrigger = UpdateSourceTrigger.LostFocus;
                bind.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ASNumericBox), 1);
                numBoxTextBox.SetBinding(ASNumericTextBox.TextProperty, bind);

                numBoxTextBox.HorizontalContentAlignment = HorizontalContentAlignment;

                numBoxTextBox.Thousands = Thousands;
                numBoxButton.Width = ButtonSize;
                numBoxTextBox.FontWeight = FontWeight;
                numBoxTextBox.Background = Background == null ? new SolidColorBrush() { Color = Colors.White } : Background;
                numBoxTextBox.Foreground = Foreground == null ? new SolidColorBrush() { Color = Colors.Black } : Foreground;
                
                numBoxTextBox.IsReadOnly = IsReadOnly;

                numBoxTextBox.LoadedEvent();

                if (IsInGrid) numBoxTextBox.IsInGrid = true;

                loaded = true;

            }

        }

        public void FormatNumericTextBox()
        {
            numBoxTextBox.FormatOnLostFocus();
        }


        private void numBoxButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(ASNumericBox.ButtonClickEvent));
        }
    }
}
