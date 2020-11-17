using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectorProMobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCreateConfirmation : ContentPage
    {
        public PageCreateConfirmation(int code)
        {
            InitializeComponent();
            string strCode = code.ToString();
            code1.Text = strCode.Substring(0, 1);
            code2.Text = strCode.Substring(1, 1);
            code3.Text = strCode.Substring(2, 1);
            code4.Text = strCode.Substring(3, 1);
        }
    }
}