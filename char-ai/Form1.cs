
using DiscordRPC;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Windows.Forms;

namespace char_ai;

public partial class MainForm : Form
{
    // Maybe I'll ask the people at char ai about the API
    [DllImport("dwmapi.dll", PreserveSig = true)]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
    // I don't even know it even supports light mode
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    private const int DWMWA_CAPTION_COLOR = 35;
    private const int DWMWA_TEXT_COLOR = 36;
    private WebView2 view;
    private const string cai = "https://character.ai";

    public static void SetDarkTitleBar(Form form)
    {
        var hwnd = form.Handle;
        int useDark = 1;
        DwmSetWindowAttribute(hwnd, DWMWA_USE_IMMERSIVE_DARK_MODE, ref useDark, sizeof(int));

        int captionColor = unchecked((int)0xFF1E1E1E);
        DwmSetWindowAttribute(hwnd, DWMWA_CAPTION_COLOR, ref captionColor, sizeof(int));

        int textColor = unchecked((int)0xFFFFFFFF);
        DwmSetWindowAttribute(hwnd, DWMWA_TEXT_COLOR, ref textColor, sizeof(int));
    }
    public MainForm()
    {
        Load += InitializeMain;
        SetDarkTitleBar(this);
        this.Width = 1280;
        this.Height = 720;
        view = new WebView2
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(view);
        
        InitializeComponent();
    }
    public void DebugMode() {
#if DEBUG
        //soon
        //MessageBox.Show("not implemented", "debug");
        return;
#endif
        return;
    }
    private async void InitializeMain(object sender, EventArgs e)
    {
        try
        {
            ///<summary>
            ///places webview data into a folder, so when updating, the data is not lost.
            ///</summary>
            string data = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "fråljer", "character-ai", "data");

            var env = await CoreWebView2Environment.CreateAsync(null, data);

            await view.EnsureCoreWebView2Async(env);

            view.CoreWebView2.NewWindowRequested += (s, args) =>
            {
                args.Handled = true; // to prevent against character.ai's opening in external windows
                view.CoreWebView2.Navigate(args.Uri);
            };

            await view.EnsureCoreWebView2Async();

            view.CoreWebView2.Navigate(cai);
            DebugMode();

            /*
            view.CoreWebView2.NavigationCompleted += (s, ev) =>
            {
                view.CoreWebView2.ExecuteScriptAsync("document.body.style.zoom = '1.10'");
            };
            */
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Failed to show {cai}. Is it down?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
