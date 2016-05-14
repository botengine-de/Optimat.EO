using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.CustomBot
{
	static	public	class TempApi
	{
		static	public string ApiUrlConstructTempLocalhost(int? ServerAddressTcp)
		{
			if (!ServerAddressTcp.HasValue)
			{
				return null;
			}

			return "http://localhost:" + ServerAddressTcp.ToString() + "/api/";
		}

	}
}
