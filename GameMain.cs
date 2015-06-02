namespace RLG
{
    #region Using Statements

    #if MONOMAC
    using MonoMac.AppKit;
    using MonoMac.Foundation;

    #elif __IOS__
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;
    #endif

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

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
    public static class GameMain
    #endif
    {
        private static CanasUvighi game;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        #if !MONOMAC && !__IOS__		 
        [STAThread]
        #endif
        public static void Main(string[] args)
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
            RunGame();
             /*   // Testing
                Actor actor = new Actor("scienide", "@", new PropertyBag(),);
                actor.Properties["intellect"] = 10;
                actor.Volume = 85;

                string serializedOutput = JsonConvert.SerializeObject(actor);

                Actor deserialized = JsonConvert.DeserializeObject<Actor>(serializedOutput);*/
            #endif
        }

        internal static void RunGame()
        {
            game = new CanasUvighi();
            game.Run();
        }

        #if __IOS__
		public override void FinishedLaunching(UIApplication app)
		{
			RunGame();
		}
        #endif
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
