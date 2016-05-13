using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveO.Nuzer
{
	class Glob
	{
		static readonly IEnumerable<KeyValuePair<System.Windows.Input.Key, WindowsInput.VirtualKeyCode>> MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode =
			MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCodeBerecne();

		static public IEnumerable<KeyValuePair<System.Windows.Input.Key, WindowsInput.VirtualKeyCode>> MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCodeBerecne()
		{
			var MengeSystemWindowsInputKey = Enum.GetValues(typeof(System.Windows.Input.Key)).Cast<System.Windows.Input.Key>();

			foreach (var WindowsInputKey in MengeSystemWindowsInputKey)
			{
				var WindowsInputVirtualKeyCode = WindowsInputVirtualKeyCodeBerecneAusSystemWindowsInputKey(WindowsInputKey);

				if (!WindowsInputVirtualKeyCode.HasValue)
				{
					continue;
				}

				yield return new KeyValuePair<System.Windows.Input.Key, WindowsInput.VirtualKeyCode>(WindowsInputKey, WindowsInputVirtualKeyCode.Value);
			}
		}

		static public WindowsInput.VirtualKeyCode? WindowsInputVirtualKeyCodeBerecneAusSystemWindowsInputKey(
			System.Windows.Input.Key WindowsInputKey)
		{
			if (System.Windows.Input.Key.F1 <= WindowsInputKey &&
				WindowsInputKey <= System.Windows.Input.Key.F24)
			{
				return (WindowsInput.VirtualKeyCode)(((int)WindowsInputKey - (int)System.Windows.Input.Key.F1) + (int)WindowsInput.VirtualKeyCode.F1);
			}

			if (System.Windows.Input.Key.D0 <= WindowsInputKey &&
				WindowsInputKey <= System.Windows.Input.Key.D9)
			{
				return (WindowsInput.VirtualKeyCode)(((int)WindowsInputKey - (int)System.Windows.Input.Key.D0) + (int)WindowsInput.VirtualKeyCode.VK_0);
			}

			var	WindowsInputKeySictString	= WindowsInputKey.ToString();

			WindowsInput.VirtualKeyCode	Result;

			if (Enum.TryParse<WindowsInput.VirtualKeyCode>(WindowsInputKeySictString, false, out	Result))
			{
				return Result;
			}

			if (Enum.TryParse<WindowsInput.VirtualKeyCode>(WindowsInputKeySictString, true, out	Result))
			{
				return Result;
			}

			/*
			 * 2014.07.29
			 * 
			switch (WindowsInputKey)
			{
				case System.Windows.Input.Key.AbntC1:
					break;
				case System.Windows.Input.Key.AbntC2:
					break;
				case System.Windows.Input.Key.Add:
					break;
				case System.Windows.Input.Key.Apps:
					break;
				case System.Windows.Input.Key.Attn:
					break;
				case System.Windows.Input.Key.Back:
					return WindowsInput.VirtualKeyCode.BACK;

				case System.Windows.Input.Key.BrowserBack:
					break;
				case System.Windows.Input.Key.BrowserFavorites:
					break;
				case System.Windows.Input.Key.BrowserForward:
					break;
				case System.Windows.Input.Key.BrowserHome:
					break;
				case System.Windows.Input.Key.BrowserRefresh:
					break;
				case System.Windows.Input.Key.BrowserSearch:
					break;
				case System.Windows.Input.Key.BrowserStop:
					break;

				case System.Windows.Input.Key.Cancel:
					break;
				case System.Windows.Input.Key.CapsLock:
					break;
				case System.Windows.Input.Key.Clear:
					break;
				case System.Windows.Input.Key.CrSel:
					break;
				case System.Windows.Input.Key.D:
					break;
				case System.Windows.Input.Key.D0:
					break;
				case System.Windows.Input.Key.D1:
					break;
				case System.Windows.Input.Key.D2:
					break;
				case System.Windows.Input.Key.D3:
					break;
				case System.Windows.Input.Key.D4:
					break;
				case System.Windows.Input.Key.D5:
					break;
				case System.Windows.Input.Key.D6:
					break;
				case System.Windows.Input.Key.D7:
					break;
				case System.Windows.Input.Key.D8:
					break;
				case System.Windows.Input.Key.D9:
					break;
				case System.Windows.Input.Key.DbeAlphanumeric:
					break;
				case System.Windows.Input.Key.DbeCodeInput:
					break;
				case System.Windows.Input.Key.DbeDbcsChar:
					break;
				case System.Windows.Input.Key.DbeDetermineString:
					break;
				case System.Windows.Input.Key.DbeEnterDialogConversionMode:
					break;
				case System.Windows.Input.Key.DbeEnterImeConfigureMode:
					break;
				case System.Windows.Input.Key.DbeFlushString:
					break;
				case System.Windows.Input.Key.DbeHiragana:
					break;
				case System.Windows.Input.Key.DbeKatakana:
					break;
				case System.Windows.Input.Key.DbeNoCodeInput:
					break;
				case System.Windows.Input.Key.DbeRoman:
					break;
				case System.Windows.Input.Key.DbeSbcsChar:
					break;
				case System.Windows.Input.Key.DeadCharProcessed:
					break;
				case System.Windows.Input.Key.Decimal:
					break;
				case System.Windows.Input.Key.Delete:
					break;
				case System.Windows.Input.Key.Divide:
					break;
				case System.Windows.Input.Key.Down:
					return WindowsInput.VirtualKeyCode.DOWN;

				case System.Windows.Input.Key.End:
					break;
				case System.Windows.Input.Key.Enter:
					return WindowsInput.VirtualKeyCode.RETURN;

				case System.Windows.Input.Key.Escape:
					return WindowsInput.VirtualKeyCode.ESCAPE;

				case System.Windows.Input.Key.Execute:
					break;

				case System.Windows.Input.Key.FinalMode:
					break;
				case System.Windows.Input.Key.HangulMode:
					break;
				case System.Windows.Input.Key.HanjaMode:
					break;
				case System.Windows.Input.Key.Help:
					break;
				case System.Windows.Input.Key.Home:
					break;

				case System.Windows.Input.Key.ImeAccept:
					break;
				case System.Windows.Input.Key.ImeConvert:
					break;
				case System.Windows.Input.Key.ImeModeChange:
					break;
				case System.Windows.Input.Key.ImeNonConvert:
					break;
				case System.Windows.Input.Key.ImeProcessed:
					break;
				case System.Windows.Input.Key.Insert:
					break;
				case System.Windows.Input.Key.JunjaMode:
					break;
				case System.Windows.Input.Key.LWin:
					break;
				case System.Windows.Input.Key.LaunchApplication1:
					break;
				case System.Windows.Input.Key.LaunchApplication2:
					break;
				case System.Windows.Input.Key.LaunchMail:
					break;

				case System.Windows.Input.Key.Left:
					return WindowsInput.VirtualKeyCode.LEFT;

				case System.Windows.Input.Key.LeftAlt:
					break;
				case System.Windows.Input.Key.LeftCtrl:
					break;
				case System.Windows.Input.Key.LeftShift:
					break;
				case System.Windows.Input.Key.LineFeed:
					break;
				case System.Windows.Input.Key.MediaNextTrack:
					break;
				case System.Windows.Input.Key.MediaPlayPause:
					break;
				case System.Windows.Input.Key.MediaPreviousTrack:
					break;
				case System.Windows.Input.Key.MediaStop:
					break;
				case System.Windows.Input.Key.Multiply:
					break;
				case System.Windows.Input.Key.Next:
					break;
				case System.Windows.Input.Key.None:
					break;
				case System.Windows.Input.Key.NumLock:
					break;
				case System.Windows.Input.Key.NumPad0:
					break;
				case System.Windows.Input.Key.NumPad1:
					break;
				case System.Windows.Input.Key.NumPad2:
					break;
				case System.Windows.Input.Key.NumPad3:
					break;
				case System.Windows.Input.Key.NumPad4:
					break;
				case System.Windows.Input.Key.NumPad5:
					break;
				case System.Windows.Input.Key.NumPad6:
					break;
				case System.Windows.Input.Key.NumPad7:
					break;
				case System.Windows.Input.Key.NumPad8:
					break;
				case System.Windows.Input.Key.NumPad9:
					break;
				case System.Windows.Input.Key.Oem1:
					break;
				case System.Windows.Input.Key.Oem102:
					break;
				case System.Windows.Input.Key.Oem2:
					break;
				case System.Windows.Input.Key.Oem3:
					break;
				case System.Windows.Input.Key.Oem4:
					break;
				case System.Windows.Input.Key.Oem5:
					break;
				case System.Windows.Input.Key.Oem6:
					break;
				case System.Windows.Input.Key.Oem7:
					break;
				case System.Windows.Input.Key.Oem8:
					break;
				case System.Windows.Input.Key.P:
					break;
				case System.Windows.Input.Key.PageUp:
					break;
				case System.Windows.Input.Key.Pause:
					break;
				case System.Windows.Input.Key.Print:
					break;
				case System.Windows.Input.Key.PrintScreen:
					break;
				case System.Windows.Input.Key.Q:
					break;
				case System.Windows.Input.Key.R:
					break;
				case System.Windows.Input.Key.RWin:
					break;
				case System.Windows.Input.Key.Right:
					break;
				case System.Windows.Input.Key.RightAlt:
					break;
				case System.Windows.Input.Key.RightCtrl:
					break;
				case System.Windows.Input.Key.RightShift:
					break;
				case System.Windows.Input.Key.S:
					break;
				case System.Windows.Input.Key.Scroll:
					break;
				case System.Windows.Input.Key.Select:
					break;
				case System.Windows.Input.Key.SelectMedia:
					break;
				case System.Windows.Input.Key.Separator:
					break;
				case System.Windows.Input.Key.Sleep:
					break;
				case System.Windows.Input.Key.Space:
					break;
				case System.Windows.Input.Key.Subtract:
					break;
				case System.Windows.Input.Key.System:
					break;
				case System.Windows.Input.Key.Tab:
					break;
				case System.Windows.Input.Key.Up:
					break;
				case System.Windows.Input.Key.VolumeDown:
					break;
				case System.Windows.Input.Key.VolumeMute:
					break;
				case System.Windows.Input.Key.VolumeUp:
					break;
				default:
					break;
			}
			 * */

			return null;
		}

		static public	WindowsInput.VirtualKeyCode VonWindowsInputKeyNaacInputSimulatorVirtualKeyCode(
			System.Windows.Input.Key WindowsInputKey)
		{
			var MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode = Glob.MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode;

			if (null != MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode)
			{
				foreach (var item in MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode)
				{
					if (WindowsInputKey == item.Key)
					{
						return item.Value;
					}
				}
			}

			throw new NotImplementedException();
		}
	}
}
