using System.Collections.Generic;
using Xamarin.Forms;

namespace KomByd.Views.Controls
{
    public class CustomListControl : StackLayout
    {
        public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty.Create(
               nameof(ItemsSource),
               typeof(IEnumerable<object>),
               typeof(CustomListControl));

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(
                nameof(ItemTemplate),
                typeof(DataTemplate),
                typeof(CustomListControl));

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            if (propertyName == ItemsSourceProperty.PropertyName)
            {
                if (ItemsSource != null)
                {
                    BuildStack();
                }
            }

            base.OnPropertyChanged(propertyName);
        }
        private void BuildStack()
        {
            Children.Clear();
            Spacing = 0;

            foreach (var item in ItemsSource)
            {
                Children.Add(CreateCellView(item));
            }
        }

        private View CreateCellView(object item)
        {
            var view = (View)ItemTemplate.CreateContent();
            var bindableObject = (BindableObject)view;

            bindableObject.BindingContext = item;

            return view;
        }
    }
}