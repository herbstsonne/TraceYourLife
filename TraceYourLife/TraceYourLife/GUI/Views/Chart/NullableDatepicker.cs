using System;
using Xamarin.Forms;

namespace TraceYourLife.GUI.Views.Chart
{
    public class NullableDatepicker : DatePicker
    {
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create<NullableDatepicker, DateTime?>(p => p.NullableDate, null);

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        public NullableDatepicker()
        {
            Unfocused += NullableDatepicker_Unfocused;
        }

        private void NullableDatepicker_Unfocused(object sender, FocusEventArgs e)
        {
            NullableDate = Date;
            UpdateDate();
        }

        private void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                Date = NullableDate.Value;
            }
            else
            {
                Format = "<>";
            }
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            //if (propertyName == "Date") NullableDate = Date;
        }
    }
}
