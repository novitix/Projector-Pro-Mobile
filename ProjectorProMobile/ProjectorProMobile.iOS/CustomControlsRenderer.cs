using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ProjectorProMobile.iOS;
using UIKit;
using CoreGraphics;
using System.ComponentModel;
using CustomControls;

[assembly: ExportRenderer(typeof(CustomControls.ModernEntry), typeof(ModernEntryRenderer))]
namespace ProjectorProMobile.iOS
{
    class ModernEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                ModernEntry caller = e.NewElement as ModernEntry;

                Control.LeftView = new UIView(new CGRect(0, 0, caller.TextPaddingLeft + 8, 0));
                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIView(new CGRect(0, 0, caller.TextPaddingRight + 8, 0));
                    
                Control.RightViewMode = UITextFieldViewMode.Always;
            }
        }
    }
}