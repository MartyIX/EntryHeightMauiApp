using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace EntryHeightMauiApp;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

    void OnEntryHandlerChanging(object sender, HandlerChangingEventArgs e)
    {
        if (e.OldHandler != null)
        {
#if IOS || MACCATALYST
            (e.OldHandler.PlatformView as UITextField).EditingDidBegin -= OnEditingDidBegin;
#elif WINDOWS
            (e.OldHandler.PlatformView as TextBox).GotFocus -= OnGotFocus;
#endif
        }
    }

    void OnEntryHandlerChanged(object sender, EventArgs e)
    {
        Entry entry = sender as Entry;
#if ANDROID
        (entry.Handler.PlatformView as AppCompatEditText).SetSelectAllOnFocus(true);
#elif IOS || MACCATALYST
        (entry.Handler.PlatformView as UITextField).EditingDidBegin += OnEditingDidBegin;
#elif WINDOWS
        entry.Text = "Text #1"; // This is not what is shown.
        (entry.Handler.PlatformView as TextBox).GotFocus += OnGotFocus;
#endif
    }

#if WINDOWS
    void OnGotFocus(object sender, RoutedEventArgs e)
    {
        TextBox nativeView = sender as TextBox;
        nativeView.Text = "Text #2"; // This is what is shown.
        nativeView.Height = 200; // Height of the entry itself is not affected.
    }
#endif

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

