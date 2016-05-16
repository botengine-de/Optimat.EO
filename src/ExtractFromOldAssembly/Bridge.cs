using Bib3.Geometrik;
using ExtractFromOldAssembly.Bib3;

namespace ExtractFromOldAssembly
{
	static public class Bridge
	{
		static public OrtogoonInt AsOrtogoonInt(this RectInt rect) =>
			new OrtogoonInt(rect.Min0, rect.Min1, rect.Max0, rect.Max1);
	}
}
