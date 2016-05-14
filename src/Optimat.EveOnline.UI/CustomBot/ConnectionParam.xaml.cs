using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Bib3;

namespace Optimat.EveOnline.UI.CustomBot
{
	/// <summary>
	/// Interaction logic for ConnectionParam.xaml
	/// </summary>
	public partial class ConnectionParam : UserControl
	{
		public ConnectionParam()
		{
			InitializeComponent();
		}

		public	string	ApiUri
		{
			get
			{
				return TextBoxApiUri.Text;
			}
		}

		private void TextBoxServerAddressTcp_TextChanged(object sender, TextChangedEventArgs e)
		{
			TextBoxApiUri.Text =
				Optimat.EveOnline.CustomBot.TempApi.ApiUrlConstructTempLocalhost(
				TextBoxServerAddressTcp.Text.TryParseInt());
		}
	}
}
