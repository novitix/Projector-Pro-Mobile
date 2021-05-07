using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using ProjectorProMobile;
using ProjectorProMobile.Droid;
using Android.Content;
using Android.Graphics.Drawables;
using and = Android.Graphics;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomControls.ModernEntry), typeof(ModernEntryRenderer))]
[assembly: ExportRenderer(typeof(CustomControls.PinEntry), typeof(PinEntryRenderer))]
//[assembly: ExportRenderer(typeof(CustomControls.DisplayEditor), typeof(DisplayEditorRenderer))]
namespace ProjectorProMobile.Droid
{
    class ModernEntryRenderer : EntryRenderer
    {
        public ModernEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.Background = new ColorDrawable(and.Color.Transparent);
                ColorDrawable currentBackgroundColor = null;
                if (Control.Background is ColorDrawable)
                {
                    currentBackgroundColor = Control.Background as ColorDrawable;
                }

                GradientDrawable gd = new GradientDrawable();
                if (currentBackgroundColor != null)
                {
                    gd.SetCornerRadius(90);
                    gd.SetColor(currentBackgroundColor.Color);
                }
                
                //gd.SetStroke(2, and.Color.LightGray);
                Control.Background = gd;

                this.Control.SetPaddingRelative(35, 0, 35, 0);
                
            }
        }
    }


    class PinEntryRenderer : EntryRenderer
    {
        public PinEntryRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            //if (Control != null)
            //{
            //    SetColor("#b8c5d6");
            //    e.NewElement.Focused += (sender, evt) => {
            //        SetColor("#c8d2e0");
            //    };
            //    e.NewElement.Unfocused += (sender, evt) => {
            //        SetColor("#b8c5d6");
            //    };
            //}
        }

        private void SetColor(string hex)
        {
            if (Control != null)
            {
                Control.Background = new ColorDrawable(and.Color.Transparent);
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(and.Color.ParseColor(hex));
                //gd.SetStroke(2, and.Color.LightGray);
                this.Control.Background = gd;
            }
        }
    }

    //class DisplayEditorRenderer : EditorRenderer
    //{
    //    public DisplayEditorRenderer(Context context) : base(context) { }
    //    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        base.OnElementPropertyChanged(sender, e);
    //        if (Control != null)
    //        {
    //            Control.TextAlignment = Android.Views.TextAlignment.Center;
    //            Control.Gravity = Android.Views.GravityFlags.Center;
    //            Control.Background = null;
    //            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
    //        }
    //    }
    //}
}