using System;
using System.Windows.Input;
using KomByd.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KomByd.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LineRouteControl : ContentView
    {
        private bool WasGridPopulated { get; set; }

        private bool IsListVisible { get; set; }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                typeof(object),
                typeof(LineDetailsPage),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    LineRouteControl self = bindable as LineRouteControl;
                    if (self == null || newVal == null || self.WasGridPopulated)
                    {
                        return;
                    }
                    self.LoadList(newVal);
                    self.WasGridPopulated = true;
                });

        private void LoadList(object value)
        {
            StopsList.IsVisible = false;
            var source = (LineDetails) value;
            DirectionLabel.Text = source.Direction;
            StopsList.ItemsSource = source.Stops;
        }

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }
        public LineRouteControl()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty GoToStopCommandProperty = BindableProperty.Create(nameof(Command),
            typeof(ICommand),
            typeof(LineRouteControl),
            default(ICommand));

        public ICommand GoToStopCommand
        {
            get => (ICommand)GetValue(GoToStopCommandProperty);
            set => SetValue(GoToStopCommandProperty, value);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            IsListVisible = !IsListVisible;
            StopsList.IsVisible = IsListVisible;
            ArrowImage.RotateTo(IsListVisible ? 180 : 0, 100);
        }
    }
}