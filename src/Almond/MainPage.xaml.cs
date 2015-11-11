using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Almond
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Display.Navigate( new Uri(@"http://192.168.1.162/cgi-bin/luci"));
        }

        public Library Library = new Library();

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Library.Back(ref Display);
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            Library.Forward(ref Display);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Display.Refresh();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Display.Stop();
        }

        private void Go_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            Library.Go(ref Display, Value.Text, e);
        }

        private async void Web_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
            {
                Value.Text = args.Uri.ToString();

                string result = await this.Display.InvokeScriptAsync("eval", new string[]
                { "window.alert = function (AlertMessage) {window.external.notify(AlertMessage)}" });

       
            }
        }
        
       
        private async void Display_ScriptNotify(object sender, NotifyEventArgs e)
        {
            Windows.UI.Popups.MessageDialog dialog = new Windows.UI.Popups.MessageDialog(e.Value);
            await dialog.ShowAsync();
        }
    }
}
