﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DirectOutput.Cab.Toys.LWEquivalent
{
    /// <summary>
    /// The LEDWizEquivalent toy provides a Ledwiz like interface to 32 outputs.<br />
    /// The outputs listes in the Outputs property can point to any IOutput in the Cabinet.<br />
    /// This toy is also used when legacy LedCOntrol.ini files are used to configure the framework.
    /// </summary>
    public class LedWizEquivalent:ToyBase,IToy
    {
        private xLedWizEquivalentOutputList _Outputs=new xLedWizEquivalentOutputList();


        /// <summary>
        /// Sets the value of a LedWizEquivalentOutput.
        /// </summary>
        /// <param name="OutputNumber">The number of the the LedWizEquivalentOutput.</param>
        /// <param name="Value">The value for the LedWizEquivalentOutput.</param>
        public void SetOutputValue(int OutputNumber, int Value)
        {
            xLedWizEquivalentOutput LWO= Outputs.First(O => O.LedWizEquivalentOutputNumber == OutputNumber);
            if (LWO != null)
            {
                LWO.Value = Value;
            }
        }

        /// <summary>
        /// Gets or sets the outputs of the LedWizEquivalent toy.
        /// </summary>
        /// <value>
        /// The outputs of the LedWizEquivalent toy.
        /// </value>
        public xLedWizEquivalentOutputList Outputs
        {
            get { return _Outputs; }
            set { _Outputs = value; }
        }

        private int _LedWizNumber=-1;

        /// <summary>
        /// Gets or sets the number of the virtual LedWiz emulated by the LedWizEquivalentToy.
        /// </summary>
        /// <value>
        /// The number of the virtual LedWiz emulated by the LedWizEquivalentToy.
        /// </value>
        public int LedWizNumber
        {
            get { return _LedWizNumber; }
            set { _LedWizNumber = value; }
        }


        /// <summary>
        /// Initializes the LedwizEquivalent toy.
        /// </summary>
        /// <param name="Pinball"><see cref="Pinball"/> object containing the <see cref="Cabinet"/> to which the <see cref="LedWizEquivalent"/> belongs.</param>
        public override void Init(Pinball Pinball)
        {
            Outputs.Init(Pinball.Cabinet); 
        }

        /// <summary>
        /// Resets all output to their default state.
        /// </summary>
        public override void Reset()
        {
            Outputs.Reset();
        }

        /// <summary>
        ///Finishes the LedWizEquivalent toy.
        /// </summary>
        public override void Finish()
        {
            Outputs.Finish();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LedWizEquivalent"/> class.
        /// </summary>
        public LedWizEquivalent() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LedWizEquivalent"/> class.
        /// </summary>
        /// <param name="LedWiz">Reference to a LedWiz object used to configure the LedWizEquivalent.</param>
        public LedWizEquivalent(DirectOutput.Cab.Out.LW.LedWiz LedWiz)
        {
            this.LedWizNumber = LedWiz.Number;
            this.Name = "LedWizEquivalent {0}".Build(LedWiz.Number);
            foreach (DirectOutput.Cab.Out.IOutput O in LedWiz.Outputs)
            {
                Outputs.Add(new xLedWizEquivalentOutput() {OutputName="{0}\\{1}".Build(LedWiz.Name,O.Name),LedWizEquivalentOutputNumber=((DirectOutput.Cab.Out.LW.LedWizOutput)O).LedWizOutputNumber});

            }
        }
       
    }
}