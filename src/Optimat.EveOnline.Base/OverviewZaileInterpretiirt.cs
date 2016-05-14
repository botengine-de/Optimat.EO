using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimat.EveOnline.Base;
using Newtonsoft.Json;

namespace Optimat.EveOnline.AuswertGbs
{
	/*
	 * 2015.02.17
	 * 
	[JsonObject(MemberSerialization.OptIn)]
	public class SictAusGbsOverviewZaileInterpretiirt : GbsElement
	{
		[JsonProperty]
		readonly public SictAusGbsOverviewZaile AusOverviewZaile;

		/// <summary>
		/// untere Scranke unter berüksictigung der genauigkait der Anzaige.
		/// </summary>
		[JsonProperty]
		readonly public Int64? DistanceScrankeMin;

		/// <summary>
		/// oobere Scranke unter berüksictigung der genauigkait der Anzaige.
		/// </summary>
		[JsonProperty]
		readonly public Int64? DistanceScrankeMax;

		/// <summary>
		/// Name des Tab im Overview in dem das Objekt gesictet wurde.
		/// </summary>
		[JsonProperty]
		readonly public string OverviewTabName;

		/// <summary>
		/// Name der Type selection im Overview in der das Objekt gesictet wurde.
		/// </summary>
		[JsonProperty]
		readonly public string OverviewTypeSelectionName;

		public bool? IconTargetedByMeSictbar
		{
			get
			{
				var AusOverviewZaile = this.AusOverviewZaile;

				if (null == AusOverviewZaile)
				{
					return null;
				}

				return AusOverviewZaile.IconTargetedByMeSictbar;
			}
		}

		public SictAusGbsOverviewZaileInterpretiirt(
			SictAusGbsOverviewZaile AusOverviewZaile,
			string WindowOverviewTypeSelectionName,
			Int64? DistanceScrankeMin,
			Int64? DistanceScrankeMax
			)
			:
			base(AusOverviewZaile)
		{
			this.AusOverviewZaile = AusOverviewZaile;

			this.OverviewTypeSelectionName = WindowOverviewTypeSelectionName;

			this.DistanceScrankeMin = DistanceScrankeMin;
			this.DistanceScrankeMax = DistanceScrankeMax;
		}

		public SictAusGbsOverviewZaileInterpretiirt()
		{
		}

		static public bool IdentiscPerTypeUndName(
			SictAusGbsOverviewZaileInterpretiirt Operand0,
			SictAusGbsOverviewZaileInterpretiirt Operand1,
			bool IgnoreCase = false)
		{
			if (Operand0 == Operand1)
			{
				return true;
			}

			if (null == Operand0)
			{
				return false;
			}

			if (null == Operand1)
			{
				return false;
			}

			return Operand0.IdentiscPerTypeUndName(Operand1, IgnoreCase);
		}

		public bool IdentiscPerTypeUndName(
			SictAusGbsOverviewZaileInterpretiirt ZuVerglaicende,
			bool IgnoreCase = false)
		{
			if (null == ZuVerglaicende)
			{
				return false;
			}

			var AusOverviewZaile = this.AusOverviewZaile;
			var ZuVerglaicendeAusOverviewZaile = ZuVerglaicende.AusOverviewZaile;

			if (object.Equals(AusOverviewZaile, ZuVerglaicendeAusOverviewZaile))
			{
				return true;
			}

			if (null == AusOverviewZaile)
			{
				return false;
			}

			return AusOverviewZaile.IdentiscPerTypeUndName(
				ZuVerglaicende.AusOverviewZaile,
				IgnoreCase);
		}

		static public bool HatInputFookus(
			SictAusGbsOverviewZaileInterpretiirt OverviewZaile)
		{
			if (null == OverviewZaile)
			{
				return false;
			}

			return OverviewZaile.AusOverviewZaile.HatInputFookus();
		}

	}
	 * */
}
