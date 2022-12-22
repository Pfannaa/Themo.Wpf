using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Themo.Wpf.Events;
using Themo.Wpf.Services.Interfaces;

namespace Themo.Wpf.Services.Impl
{
    public class ThemeService<TEnum> : IThemeService<TEnum> where TEnum : struct
    {
        #region events
        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        #endregion

        #region fields
        private TEnum? _currentTheme;

        private ResourceDictionary ThemeDictionary
        {
            get => Application.Current.Resources.MergedDictionaries[0];
            set => Application.Current.Resources.MergedDictionaries[0] = value;
        }
        #endregion

        #region props
        public TEnum? CurrentTheme
        {
            get
            {
                if (_currentTheme == null) _currentTheme = GetCurrentTheme();
                return _currentTheme;
            }
            set => _currentTheme = value;
        }

        #endregion

        #region methods

        private void ChangeTheme(Uri uri, TEnum theme)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
            ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(theme));
        }

        /// <summary>
        /// Sets the theme according to the passed in enum value
        /// Note: It is expected, your themes are located in your project root in a directory called Themes
        /// </summary>
        /// <param name="theme">The desired enum value, which has to exactly match your .xaml file</param>
        public void SetTheme(TEnum theme)
        {
            try
            {
                ChangeTheme(new Uri($"/Themes/{theme}.xaml", UriKind.Relative), theme);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks which resource dictionary is currently loaded
        /// </summary>
        /// <returns></returns>
        public TEnum GetCurrentTheme()
        {
            var loadedDictionary = Application.Current.Resources.MergedDictionaries[0];
            string dictionaryContent = loadedDictionary.Source.ToString();
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().FirstOrDefault(x => dictionaryContent.EndsWith(x.ToString()));
        }

        #endregion
    }
}
