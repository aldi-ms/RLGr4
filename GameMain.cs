#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;

#elif __IOS__
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
using System.IO;

namespace RLG
{
    #region Using Statements

    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Linq;

    using Newtonsoft.Json;
    using RLG.Contracts;
    using RLG.Entities;
    using RLG.Enumerations;
    using RLG.Framework;
    using RLG.Utilities;

    #endregion

    #if __IOS__
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	

#else
    static class GameMain
	#endif
    {
        private static CanasUvighi game;

        internal static void RunGame()
        {
            game = new CanasUvighi();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        #if !MONOMAC && !__IOS__		 
        [STAThread]
        #endif
		static void Main(string[] args)
        {
            #if MONOMAC
			NSApplication.Init ();

			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate();
				NSApplication.Main(args);
			}
            #elif __IOS__
			UIApplication.Main(args, null, "AppDelegate");
            #else
            if (true)
            {
                RunGame();
            }
            else
            {
             /*   // Testing
                Actor actor = new Actor("scienide", "@", new PropertyBag(),);
                actor.Properties["intellect"] = 10;
                actor.Volume = 85;

                string serializedOutput = JsonConvert.SerializeObject(actor);

                Actor deserialized = JsonConvert.DeserializeObject<Actor>(serializedOutput);*/
            }
            #endif
        }

        #if __IOS__
		public override void FinishedLaunching(UIApplication app)
		{
			RunGame();
		}
        #endif
        public static void Testing()
        {
            throw new NotImplementedException();
        }
    }

    #if MONOMAC
	class AppDelegate : NSApplicationDelegate
	{
		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
				if (a.Name.StartsWith("MonoMac")) {
					return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
				}
				return null;
			};
			Program.RunGame();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}  
	#endif

}

