using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace CustomControls
{
    public class ModernEntry : Entry
    {
        public static readonly BindableProperty TextPaddingRightProperty =
            BindableProperty.Create("TextPaddingRight",
                                    typeof(int),
                                    typeof(ModernEntry),
                                    0);

        public int TextPaddingRight
        {
            get => (int)GetValue(TextPaddingRightProperty);
            set => SetValue(TextPaddingRightProperty, value);
        }

        public static readonly BindableProperty TextPaddingLeftProperty =
            BindableProperty.Create("TextPaddingLeft",
                                    typeof(int),
                                    typeof(ModernEntry),
                                    0);

        public int TextPaddingLeft
        {
            get => (int)GetValue(TextPaddingLeftProperty);
            set => SetValue(TextPaddingLeftProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create("CornerRadius",
                                    typeof(int),
                                    typeof(ModernEntry),
                                    0);
    }

    public class PinEntry : Entry
    {

    }
    public class DisplayLabel : Label
    {

    }

    public class NumericValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isNumeric = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers
                bool isShort = args.NewTextValue.Length <= 4;

                ((Entry)sender).Text = (isNumeric && isShort) ? args.NewTextValue : args.NewTextValue.Remove(args.NewTextValue.Length - 1);
            }
        }


    }

    public class FontValidationBehavior : Behavior<Entry>
    {

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                bool isNumeric = args.NewTextValue.ToCharArray().All(x => char.IsDigit(x)); //Make sure all characters are numbers
                if (!isNumeric)
                {
                    ((Entry)sender).Text = args.NewTextValue.Remove(args.NewTextValue.Length - 1);
                }

                if (int.Parse(args.NewTextValue) > 70)
                {
                    ((Entry)sender).Text = "70";
                }

            }
        }


    }
}
