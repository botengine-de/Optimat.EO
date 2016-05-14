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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Optimat.EveOnline.GBS
{
	/// <summary>
	/// Interaction logic for AnimZiilPunkt.xaml
	/// </summary>
	public partial class AnimZiilPunkt : UserControl
	{
		public AnimZiilPunkt()
		{
			InitializeComponent();

			AktualisiireTailAnim();
		}

		public static readonly DependencyProperty AnimEnabledProperty =
			 DependencyProperty.Register("AnimEnabled", typeof(bool?),
			 typeof(AnimZiilPunkt),
			 new FrameworkPropertyMetadata(
				 null,
				 OnAnimEnabledPropertyChanged));

		private static void OnAnimEnabledPropertyChanged(
			DependencyObject source,
			DependencyPropertyChangedEventArgs e)
		{
			var control = source as AnimZiilPunkt;

			if (null == control)
			{
				return;
			}

			control.AktualisiireTailAnim();
		}

		// .NET Property wrapper
		public bool? AnimEnabled
		{
			get { return (bool?)GetValue(AnimEnabledProperty); }
			set { SetValue(AnimEnabledProperty, value); }
		}

		Brush InternFill;

		public Brush Fill
		{
			set
			{
				InternFill = value;

				BrushRicte();
			}

			get
			{
				return InternFill;
			}
		}

		void BrushRicte()
		{
			if (!IsInitialized)
			{
				return;
			}

			Path.Fill = InternFill;
		}

		Storyboard StoryboardRotation()
		{
			return FindResource("StoryboardRotation") as Storyboard;
		}

		public void AnimateBegin()
		{
			var StoryboardRotation = this.StoryboardRotation();

			StoryboardRotation.Begin(this);
		}

		public void AnimateResume()
		{
			var StoryboardRotation = this.StoryboardRotation();

			StoryboardRotation.Resume();
		}

		public void AnimatePause()
		{
			StoryboardRotation().Pause();
		}

		void AktualisiireTailAnim()
		{
			if ((AnimEnabled ?? false) && IsLoaded && System.Windows.Visibility.Visible == Visibility)
			{
				AnimateBegin();
			}
			else
			{
				AnimatePause();
			}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			AktualisiireTailAnim();
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
		{
			AktualisiireTailAnim();
		}

		private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			AktualisiireTailAnim();
		}
	}
}
