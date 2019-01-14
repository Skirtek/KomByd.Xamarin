using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using KomByd.Repository.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KomByd.Views.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LinesGridControl : ContentView
    {
        private bool WasGridPopulated { get; set; }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LinesGridControl));

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                typeof(IEnumerable<Line>),
                typeof(LinesGridControl),
                defaultValue: new List<Line>(),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    LinesGridControl self = bindable as LinesGridControl;
                    if (self == null || newVal == null || self.WasGridPopulated)
                    {
                        return;
                    }
                     self.GenerateLinesGrid((IEnumerable<Line>)newVal);
                    self.WasGridPopulated = true;
                });

        public IEnumerable<Line> ItemsSource
        {
            get => (IEnumerable<Line>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public LinesGridControl() => InitializeComponent();           

        private void GenerateLinesGrid(IEnumerable<Line> linesList)
        {
            var lines = linesList.ToList();
            for (int i = 0; i < Math.Ceiling((double)lines.Count/4); i++)
            {
                LinesGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            }
            for (int i = 0; i < 4; i++)
            {
                LinesGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            var rowIndex = 0;
            var columnIndex = 0;
            foreach (var line in lines)
            {
                LinesGrid.Children.Add(PrepareLabel(line.LineNumber), columnIndex, rowIndex);
                columnIndex++;
                if (columnIndex == 4)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }
        }

        private Label PrepareLabel(string text)
        {
            var label = new Label
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 20,
                TextColor = Color.FromHex("#37466F"),
                FontAttributes = FontAttributes.Bold,
                Text = text
            };

            TapGestureRecognizer tap = new TapGestureRecognizer();
            tap.Tapped += (sender, e) =>
            {
                Command?.Execute(((Label)sender).Text);
            };
            label.GestureRecognizers.Add(tap);

            return label;
        }
    }
}