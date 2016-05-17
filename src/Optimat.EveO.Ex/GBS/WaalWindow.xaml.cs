using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bib3.Windows;

namespace Optimat.EveO.Nuzer.GBS
{
	/// <summary>
	/// Interaction logic for WahlWindow.xaml
	/// </summary>
	public partial class WaalWindow : UserControl
	{
		public int	VerzöögerungNaacWexlFokusMili
		{
			set;
			get;
		}

		public WaalWindow()
		{
			VerzöögerungNaacWexlFokusMili = 400;

			InitializeComponent();
		}

		IntPtr InternWaalWindowHandle;
		public IntPtr WaalWindowHandle
		{
			private set
			{
				this.InternWaalWindowHandle = value;

				this.NaacWaalGeändert();
			}
			get
			{
				return this.InternWaalWindowHandle;
			}
		}

		System.Windows.Window EnthaltendeWindow
		{
			get
			{
				return System.Windows.Window.GetWindow(this);
			}
		}

		bool WartendAufWexlFokus
		{
			set
			{
				this.CheckBoxWahlWindowPerWechselFokus.IsChecked = value;
			}

			get
			{
				return this.CheckBoxWahlWindowPerWechselFokus.IsChecked == true;
			}
		}

		void NaacWaalGeändert()
		{
			this.Aktualisiire();
		}

		void Aktualisiire()
		{
			string WindowTitel = User32.GetWindowText(this.WaalWindowHandle);

			this.TextBoxWindowTitel.Text = WindowTitel;
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			var EnthaltendeWindow = this.EnthaltendeWindow;

			if (EnthaltendeWindow != null)
			{
				EnthaltendeWindow.Deactivated += new EventHandler(EnthaltendesFenster_Deactivated);
			}
		}

		void EnthaltendesFenster_Deactivated(object sender, EventArgs e)
		{
			if (this.WartendAufWexlFokus)
			{
				if (sender == this.EnthaltendeWindow)
				{
					this.WartendAufWexlFokus = false;

					System.Threading.Thread.Sleep(VerzöögerungNaacWexlFokusMili);

					var	AktiveWindow	= User32.GetForegroundWindow();

					this.WaalWindowHandle = AktiveWindow;
				}
			}
		}

		private void ButtonWahlWindowEntferne_Click(object sender, RoutedEventArgs e)
		{
			this.WaalWindowHandle = IntPtr.Zero;
		}
	}
}
