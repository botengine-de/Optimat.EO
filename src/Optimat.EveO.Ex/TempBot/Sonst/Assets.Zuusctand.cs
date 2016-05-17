using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Optimat.ScpezEveOnln;

namespace Optimat.EveOnline.Anwendung
{
	[JsonObject(MemberSerialization.OptIn)]
	public partial class SictAssets
	{
		[JsonProperty]
		public List<SictAsset> MengeAsset;

	}

	[JsonObject(MemberSerialization.OptIn)]
	public class SictAsset
	{
	}
}
