/**
 * StickyControlSettings.cs
 * 
 * Thunder Aerospace Corporation's Flight Computer for the Kerbal Space Program, by Taranis Elsu
 * 
 * (C) Copyright 2014, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Apache License Version 2.0. See the LICENSE.txt and NOTICE.txt
 * files for more information.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tac.StickyControls
{
    internal class Settings
    {
        //internal float Speed { get; set; }
        //internal float Step { get; set; }
        //internal float PrecisionControlsModifier { get; set; }
        //internal string ZeroControlsKey { get; set; }
        //internal string SetControlsKey { get; set; }
        internal bool Enabled { get; set; }

        //internal float MinTime { get; set; }
        //internal float Exponent { get; set; }

        //internal float PositionDeadZone { get; set; }
        //internal float PositionExponent { get; set; }

        internal Settings()
        {
            // Set defaults
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed = 1.0f;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step = 0.025f;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier = 0.1f;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey = "`";
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey = "y";


            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime = 0.2f;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent = 1.5f;

            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone = 0.025f;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent = 1.5f;
        }

        internal void Load(ConfigNode config)
        {
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed = Utilities.GetValue(config, "Speed", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed);
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step = Utilities.GetValue(config, "Step", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step);
            if (HighLogic.CurrentGame.Parameters.CustomParams<TSC>().keepEnabledBetweenGames)
                Enabled = Utilities.GetValue(config, "Enabled", Enabled);
            else
                Enabled = false;
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier = Utilities.GetValue(config, "PrecisionControlsModifier", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier);
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey = Utilities.GetValue(config, "ZeroControlsKey", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey);
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey = Utilities.GetValue(config, "SetControlsKey", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey);

            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime = Utilities.GetValue(config, "MinTime", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime);
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent = Utilities.GetValue(config, "Exponent", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent);

            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone = Utilities.GetValue(config, "PositionDeadZone", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone);
            HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent = Utilities.GetValue(config, "PositionExponent", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent);
        }

        internal void Save(ConfigNode config)
        {
            config.AddValue("Speed", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed);
            config.AddValue("Step", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step);
            config.AddValue("Enabled", Enabled);
            config.AddValue("PrecisionControlsModifier", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier);
            config.AddValue("ZeroControlsKey", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey);
            config.AddValue("SetControlsKey", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey);

            config.AddValue("MinTime", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime);
            config.AddValue("Exponent", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent);

            config.AddValue("PositionDeadZone", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone);
            config.AddValue("PositionExponent", HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent);
        }
    }
}
