using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimat.EveO.Nuzer
{
	class Glob
	{
		static readonly IEnumerable<KeyValuePair<System.Windows.Input.Key, WindowsInput.Native.VirtualKeyCode>> MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCode =
			MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCodeBerecne();

		static public IEnumerable<KeyValuePair<System.Windows.Input.Key, WindowsInput.Native.VirtualKeyCode>> MengeVonWindowsInputKeyNaacWindowsInputVirtualKeyCodeBerecne()
		{
			var MengeSystemWindowsInputKey = Enum.GetValues(typeof(System.Windows.Input.Key)).Cast<System.Windows.Input.Key>();

			foreach (var WindowsInputKey in MengeSystemWindowsInputKey)
			{
				var WindowsInputVirtualKeyCode = WindowsInputVirtualKeyCodeBerecneAusSystemWindowsInputKey(WindowsInputKey);

				if (!WindowsInputVirtualKeyCode.HasValue)
				{
					continue;
				}

				yield return new KeyValuePair<System.Windows.Input.Key, WindowsInput.Native.VirtualKeyCode>(WindowsInputKey, WindowsInputVirtualKeyCode.Value);
			}
		}

		static public WindowsInput.Native.VirtualKeyCode? WindowsInputVirtualKeyCodeBerecneAusSystemWindowsInputKey(
			System.Windows.Input.Key WindowsInputKey)
		{
			if (System.Windows.Input.Key.F1 <= WindowsInputKey &&
				WindowsInputKey <= System.Windows.Input.Key.F24)
			{
				return (WindowsInput.Native.VirtualKeyCode)(((int)WindowsInputKey - (int)System.Windows.Input.Key.F1) + (int)WindowsInput.Native.VirtualKeyCode.F1);
			}

			if (System.Windows.Input.Key.D0 <= WindowsInputKey &&
				WindowsInputKey <= System.Windows.Input.Key.D9)
			{
				return (WindowsInput.Native.VirtualKeyCode)(((int)WindowsInputKey - (int)System.Windows.Input.Key.D0) + (int)WindowsInput.Native.VirtualKeyCode.VK_0);
			}

			var WindowsInputKeySictString = WindowsInputKey.ToString();

			WindowsInput.Native.VirtualKeyCode Result;

			if (Enum.TryParse(WindowsInputKeySictString, false, out Result))
			{
				return Result;
			}

			if (Enum.TryParse(WindowsInputKeySictString, true, out Result))
			{
				return Result;
			}


			return null;
		}

		static public WindowsInput.Native.VirtualKeyCode VonWindowsInputKeyNaacInputSimulatorVirtualKeyCode(
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
