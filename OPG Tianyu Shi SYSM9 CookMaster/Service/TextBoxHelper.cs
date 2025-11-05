using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace OPG_Tianyu_Shi_SYSM9_CookMaster.Service
{
    public static class TextBoxHelper
    {
        public static string GetPlaceholder(DependencyObject obj) =>
            (string)obj.GetValue(PlaceholderProperty);

        public static void SetPlaceholder(DependencyObject obj, string value) =>
            obj.SetValue(PlaceholderProperty, value);

        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new FrameworkPropertyMetadata(
                    defaultValue: null,
                    propertyChangedCallback: OnPlaceholderChanged)
                );

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBoxControl)
            {
                if (!textBoxControl.IsLoaded)
                {
                    // Ensure that the events are not added multiple times
                    textBoxControl.Loaded -= TextBoxControl_Loaded;
                    textBoxControl.Loaded += TextBoxControl_Loaded;
                }

                textBoxControl.TextChanged -= TextBoxControl_TextChanged;
                textBoxControl.TextChanged += TextBoxControl_TextChanged;

                // If the adorner exists, invalidate it to draw the current text
                if (GetOrCreateAdorner(textBoxControl, out PlaceholderAdorner adorner))
                    adorner.InvalidateVisual();
            }
            else if (d is PasswordBox passwordBox)
            {
                if (!passwordBox.IsLoaded)
                {
                    passwordBox.Loaded -= PasswordBoxControl_Loaded;
                    passwordBox.Loaded += PasswordBoxControl_Loaded;
                }

                passwordBox.PasswordChanged -= PasswordBoxControl_PasswordChanged;
                passwordBox.PasswordChanged += PasswordBoxControl_PasswordChanged;

                if (GetOrCreateAdorner(passwordBox, out PlaceholderAdorner adorner))
                    adorner.InvalidateVisual();
            }
        }

        private static void TextBoxControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBoxControl)
            {
                textBoxControl.Loaded -= TextBoxControl_Loaded;
                GetOrCreateAdorner(textBoxControl, out _);
            }
        }

        private static void TextBoxControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBoxControl
                && GetOrCreateAdorner(textBoxControl, out PlaceholderAdorner adorner))
            {
                // Control has text. Hide the adorner.
                if (textBoxControl.Text.Length > 0)
                    adorner.Visibility = Visibility.Hidden;

                // Control has no text. Show the adorner.
                else
                    adorner.Visibility = Visibility.Visible;
            }
        }

        private static void PasswordBoxControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.Loaded -= PasswordBoxControl_Loaded;
                GetOrCreateAdorner(passwordBox, out _);
            }
        }

        private static void PasswordBoxControl_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox &&
                GetOrCreateAdorner(passwordBox, out PlaceholderAdorner adorner))
            {
                if (passwordBox.Password.Length > 0)
                    adorner.Visibility = Visibility.Hidden;
                else
                    adorner.Visibility = Visibility.Visible;
            }
        }

        private static bool GetOrCreateAdorner(Control control, out PlaceholderAdorner adorner)
        {
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(control);
            if (layer == null)
            {
                adorner = null;
                return false;
            }

            adorner = layer.GetAdorners(control)?.OfType<PlaceholderAdorner>().FirstOrDefault();
            if (adorner == null)
            {
                adorner = new PlaceholderAdorner(control);
                layer.Add(adorner);
            }
            return true;
        }


        public class PlaceholderAdorner : Adorner
        {
            public PlaceholderAdorner(Control control) : base(control) { }

            protected override void OnRender(DrawingContext drawingContext)
            {
                Control control = (Control)AdornedElement;

                string placeholderValue = TextBoxHelper.GetPlaceholder(control);
                if (string.IsNullOrEmpty(placeholderValue))
                    return;

                // Skip drawing if control has content (PasswordBox handled separately)
                bool hasText = control switch
                {
                    TextBox t => !string.IsNullOrEmpty(t.Text),
                    PasswordBox p => !string.IsNullOrEmpty(p.Password),
                    _ => true
                };

                if (hasText)
                    return;

                FormattedText text = new FormattedText(
                    placeholderValue,
                    System.Globalization.CultureInfo.CurrentCulture,
                    control.FlowDirection,
                    new Typeface(control.FontFamily, control.FontStyle, control.FontWeight, control.FontStretch),
                    control.FontSize,
                    SystemColors.InactiveCaptionBrush,
                    VisualTreeHelper.GetDpi(control).PixelsPerDip);

                Point renderingOffset = new Point(control.Padding.Left + 2, control.Padding.Top + 2);
                drawingContext.DrawText(text, renderingOffset);
            }

        }
    }
    }

    //public static class PasswordBoxHelper
    //{
    //    public static readonly DependencyProperty WatermarkProperty =
    //        DependencyProperty.RegisterAttached(
    //            "Watermark",
    //            typeof(string),
    //            typeof(PasswordBoxHelper),
    //            new PropertyMetadata(string.Empty, OnWatermarkChanged));

    //    public static void SetWatermark(DependencyObject element, string value)
    //        => element.SetValue(WatermarkProperty, value);

    //    public static string GetWatermark(DependencyObject element)
    //        => (string)element.GetValue(WatermarkProperty);

    //    private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        if (d is PasswordBox passwordBox)
    //        {
    //            passwordBox.PasswordChanged += (s, ev) =>
    //            {
    //                passwordBox.Tag = string.IsNullOrEmpty(passwordBox.Password)
    //                    ? e.NewValue
    //                    : null;
    //            };
    //        }
    //    }
    //}

