using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimat.EveOnline.VonSensor
{
	public	class OverviewRowState	: GbsElement
	{
		readonly public Int64 SeenFirstTime;

		readonly public Int64 SeenLastTime;

		readonly public OverviewZaile SeenLastState;
	}
}
