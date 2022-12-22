namespace Themo.Wpf.Events
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public object NewTheme { get; set; }

        public ThemeChangedEventArgs(object newTheme)
        {
            this.NewTheme = newTheme;
        }
    }
}