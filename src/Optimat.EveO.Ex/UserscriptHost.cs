using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bib3;
using Optimat.EveOnline;
using Optimat.EveOnline.Base;


namespace Optimat.EveO.Nuzer
{
	public class UserscriptHost : IDisposable
	{
		readonly public Int64 BeginZait;

		readonly object Lock = new object();

		static readonly Bib3.SictIdentInt64Fabrik IdentFabrik = new Bib3.SictIdentInt64Fabrik(1);

		readonly public Int64 Ident = IdentFabrik.IdentBerecne();

		readonly byte[] AssemblySerial = null;

		readonly byte[] AssemblySymbolStoreSerial = null;

		AppDomain ScriptAppDomain = null;

		System.Reflection.Assembly AssemblyLoaded;

		Type ScriptInterfaceType;

		System.Runtime.Remoting.ObjectHandle ScriptInterfaceWrapped;

		IUserscript ScriptInterface;

		System.Runtime.Remoting.Lifetime.ILease ScriptInterfaceLease;

		readonly Optimat.EveOnline.Userscript.Serializer.ISerializer Serializer = new Optimat.EveOnline.Userscript.Serializer.NewtonsoftJson();

		SictWertMitZait<string> FromScriptMessageLezte;

		public SictWertMitZait<System.Exception> ScriptExceptionLezte
		{
			private set;
			get;
		}

		static public bool PrädikaatTypeIstScriptInterface(Type Kandidaat)
		{
			if (null == Kandidaat)
			{
				return false;
			}

			return Kandidaat.InheritsOrImplementsOrEquals(typeof(IUserscript)) && !Kandidaat.IsAbstract;
		}

		public UserscriptHost(
			byte[] AssemblySerial,
			byte[] AssemblySymbolStoreSerial)
		{
			BeginZait = Bib3.Glob.StopwatchZaitMiliSictInt();

			this.AssemblySerial = AssemblySerial;
			this.AssemblySymbolStoreSerial = AssemblySymbolStoreSerial;

			if (null == AssemblySerial)
			{
				return;
			}

			if (false)
			{
				//	Weege Probleme mit Debuge (Visual Studio Debugger überspringt Method in Dll) verzict auf AppDomain

				ScriptAppDomain = AppDomain.CreateDomain("UserScript.Instance[" + Ident.ToString() + "]");

				AssemblyLoaded = ScriptAppDomain.Load(AssemblySerial, AssemblySymbolStoreSerial);
			}
			else
			{
				AssemblyLoaded = System.Reflection.Assembly.Load(AssemblySerial, AssemblySymbolStoreSerial);
			}

			var MengeType = AssemblyLoaded.GetTypes();

			ScriptInterfaceType =
				MengeType
				.FirstOrDefaultNullable(PrädikaatTypeIstScriptInterface);

			if (null == ScriptInterfaceType)
			{
				return;
			}

			if (false)
			{
				//	Weege Probleme mit Debuge (Visual Studio Debugger überspringt Method in Dll) verzict auf AppDomain

				ScriptInterfaceWrapped = ScriptAppDomain.CreateInstance(AssemblyLoaded.FullName, ScriptInterfaceType.FullName);

				ScriptInterfaceLease = (System.Runtime.Remoting.Lifetime.ILease)ScriptInterfaceWrapped.InitializeLifetimeService();

				ScriptInterface = (IUserscript)ScriptInterfaceWrapped.Unwrap();
			}
			else
			{
				ScriptInterface = (IUserscript)Activator.CreateInstance(ScriptInterfaceType);
			}
		}

		public void Dispose()
		{
			if (null != ScriptAppDomain)
			{
				AppDomain.Unload(ScriptAppDomain);
			}

			ScriptAppDomain = null;
		}

		public VonAutomatMeldungZuusctand NaacUserscriptMeldung(Optimat.EveOnline.Base.NaacAutomatMeldungZuusctand Zuusctand)
		{
			var ScriptInterface = this.ScriptInterface;

			if (null == ScriptInterface)
			{
				return null;
			}

			var Message = Serializer.ToScriptMessageSerialize(Zuusctand);

			ToScriptMessageSerialized(Message);

			return VonUserscriptMeldung();
		}

		string FromScriptMessageSerialized()
		{
			if (null == ScriptInterface)
			{
				return null;
			}

			return FromScriptMessageLezte.Wert;

			lock (Lock)
			{
				return ScriptInterface.FromScriptMessageSerialized();
			}
		}

		public Optimat.EveOnline.Base.VonAutomatMeldungZuusctand VonUserscriptMeldung()
		{
			return Serializer.FromScriptMessageDeSerialize(this.FromScriptMessageSerialized());
		}

		void ToScriptMessageSerialized(string MessageSerialized)
		{
			if (null == ScriptInterface)
			{
				return;
			}

			var ZaitMili = Bib3.Glob.StopwatchZaitMiliSictInt();

			var	Aktioon	= new	Action(() =>
				{
					lock (Lock)
					{
						try
						{
							ScriptInterface.ToScriptMessageSerialized(MessageSerialized);

							FromScriptMessageLezte = new SictWertMitZait<string>(ZaitMili, ScriptInterface.FromScriptMessageSerialized());
						}
						catch (System.Exception Exception)
						{
							ScriptExceptionLezte = new SictWertMitZait<System.Exception>(ZaitMili, Exception);
						}
					}
				});

			var Task = new Task(Aktioon);

			Task.Start();

			while(!Task.IsCompleted)
			{
				System.Threading.Thread.Sleep(4);
			}
		}
	}
}
