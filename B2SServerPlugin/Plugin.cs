﻿using System.ComponentModel.Composition;
using System.IO;
using DirectOutput;
using DirectOutput.GlobalConfig;
using System;

/// <summary>
/// DirectOutputPlugin is the namespace of the Dll implementing the actual plugin interface for the B2S Server.
/// </summary> 
namespace B2SServerPlugin
{

    /// <summary>
    /// Plugin is IDirectPlugin interface implementation required by the B2S Server.
    /// </summary>
    [Export(typeof(B2S.IDirectPlugin))]
    public class Plugin : B2S.IDirectPlugin, B2S.IDirectPluginFrontend
    {


        #region IDirectPluginFrontend Member


        /// <summary>
        /// Shows the Frontend of the Plugin.<br/>
        /// The IDirectPluginFrontend interface requires the implementation of this method.
        /// </summary>
        public void PluginShowFrontend()
        {
            DirectOutput.Frontend.MainMenu.Open(Pinball);
        }

        #endregion


        #region IDirectPlugin Members

        /// <summary>
        /// Gets the name of this IDirectPlugin.<br/>
        /// This property returns the version of the DirectOutput.dll, NOT the version of the B2SServer plugin.
        /// </summary>
        /// <value>
        /// The name of this IDirectPlugin (Name is DirectOutput (V: VersionNumber) af of TimeStamp).
        /// </value>
        public string Name
        {
            get
            {
                Version V = typeof(Pinball).Assembly.GetName().Version;
                DateTime BuildDate = new DateTime(2000, 1, 1).AddDays(V.Build).AddSeconds(V.Revision * 2);
                return "DirectOutput (V: {0} as of {1})".Build(V.ToString(), BuildDate.ToString("yyyy.MM.dd hh:mm"));
            }
        }


        /// <summary>
        /// This method is called, when the property Pause of Pinmame gets set to false.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// DirectOutput implements only a method stub for this method.
        /// </summary>
        public void PinMameContinue() { }

        /// <summary>
        /// This method is called, when new data from Pinmame becomes available.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.
        /// </summary>
        /// <param name="TableElementTypeChar">Char representing the table element type (S=Solenoid, W=Switch, L=Lamp, M=Mech, G=GI).</param>
        /// <param name="Number">The number of the table element.</param>
        /// <param name="Value">The value of the table element.</param>
        public void PinMameDataReceive(char TableElementTypeChar, int Number, int Value)
        {
            Pinball.ReceivePinmameData(TableElementTypeChar, Number, Value);
        }

        /// <summary>
        /// This method is called, when the property Pause of Pinmame gets set to true.
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// DirectOutput implements only a method stub for this method.        /// </summary>
        public void PinMamePause() { }

        /// <summary>
        /// This method is called, when the Run method of PinMame gets called.
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// DirectOutput implements only a method stub for this method.        /// </summary>
        public void PinMameRun() { }

        /// <summary>
        /// This method is called, when the Stop method of Pinmame is called.
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// DirectOutput implements only a method stub for this method.
        /// </summary>
        public void PinMameStop() { }


        /// <summary>
        /// Finishes the plugin.<br />
        /// This is the last method called, before a plugin is discared. This method is also called, after a undhandled exception has occured in a plugin.
        /// </summary>
        public void PluginFinish()
        {

            Pinball.Finish();
        }

        /// <summary>
        /// Initializes the Plugin.<br/>
        /// The IDirectPlugin interface requires the implementation of this method.<br/>
        /// </summary>
        /// <param name="TableFilename">The table filename.</param>
        /// <param name="RomName">Name of the rom.</param>
        public void PluginInit(string TableFilename, string RomName)
        {
            Pinball.Init(new FileInfo(TableFilename), RomName);
        }

        #endregion

        #region Properties



        private Pinball _Pinball = new Pinball();

        /// <summary>
        /// Gets or sets the Pinball object for the plugin.
        /// </summary>
        /// <value>
        /// The Pinball object for the plugin.
        /// </value>
        public Pinball Pinball
        {
            get { return _Pinball; }
            set { _Pinball = value; }
        }

        #endregion





        /// <summary>
        /// Initializes a new instance of the <see cref="Plugin"/> class.
        /// </summary>
        public Plugin()
        {


        }











    }
}