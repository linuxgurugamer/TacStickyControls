// The  GameParameters.CustomFloatParameterUI  is broken, so am not using that now, see
// bug report: http://bugs.kerbalspaceprogram.com/issues/15699?next_issue_id=15695
#define BROKEN_FLOAT
//#define USE_ALT_FLOAT

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;



namespace Tac
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings
    public class TSC : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "General"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Tac Sticky Controls"; } }
        public override string DisplaySection { get { return "Tac Sticky Controls"; } }
        public override int SectionOrder { get { return 3; } }
        public override bool HasPresets { get { return true; } }



        [GameParameters.CustomParameterUI("Show window when toggling on")]
        public bool showWinWhenTogOn = true;

        [GameParameters.CustomParameterUI("Keep Enabled setting between games",
            toolTip = "If the Sticky Controls are enabled when exiting a game, reenable when reentering the game")]
        public bool keepEnabledBetweenGames = false;

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Speed", minValue = .025f, maxValue = 2.00f,
            toolTip = "How fast the controls move while pressing the key")]
#endif
        public float Speed = 1f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Speed", minValue = 25f, maxValue = 200f,
            toolTip = "How fast the controls move while pressing the key")]
        public float speed
        {
            get { return Speed * 100; }
            set { Speed = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Step", toolTip = "How fast the controls move while pressing the key", minValue = .005f, maxValue = .250f)]      
#endif
        public float Step = 0.025f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Step", toolTip = "How fast the controls move while pressing the key", minValue = 5f, maxValue = 250f)]
        public float step
        {
            get { return Step * 100; }
            set { Step = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Precision Controls Modifier", 
            toolTip = "Scales both Speed and Step by this amount when precision controls is turned on", minValue = .005f, maxValue = .250f)]
#endif
        public float PrecisionControlsModifier = 0.1f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Precision Controls Modifier", 
            toolTip = "Scales both Speed and Step by this amount when precision controls is turned on", minValue = 5f, maxValue = 250f)]
        public float precisionControlsModifier
        {
            get { return PrecisionControlsModifier * 100; }
            set { PrecisionControlsModifier = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Minimum Time (hundredths of a second)",
           toolTip = "The time in seconds a key has to be held down in order for the controls to move more than a single step.", minValue = .005f, maxValue = .250f)]
#endif
        public float MinTime = 0.2f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Minimum Time (hundredths of a second)",
           toolTip = "The time in seconds a key has to be held down in order for the controls to move more than a single step.", minValue = 5f, maxValue = 250f)]
        public float minTime
        {
            get { return MinTime * 100; }
            set { MinTime = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Exponent", minValue = 0.0f, maxValue = 4.00f, 
          toolTip = "Larger values make the controls move much faster when holding a key down longer.\nWhen set to 100, the controls move the same amount per unit of time regardless of how long the key has been held down.")]
#endif
        public float Exponent = 1f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Exponent", minValue = 0.0f, maxValue = 400f, 
          toolTip = "Larger values make the controls move much faster when holding a key down longer.\nWhen set to 100, the controls move the same amount per unit of time regardless of how long the key has been held down.")]
        public float exponent
        {
            get { return Exponent * 100; }
            set { Exponent = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Position Dead Zone", minValue = .005f, maxValue = .250f,
          toolTip = "Controls the dead zone around zero where the controls have no effect")]
#endif
        public float PositionDeadZone = 0.025f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Position Dead Zone", minValue = 5f, maxValue = 250f,
          toolTip = "Controls the dead zone around zero where the controls have no effect")]
        public float positionDeadZone
        {
            get { return PositionDeadZone * 100; }
            set { PositionDeadZone = value / 100f; }
        }
#endif

#if !BROKEN_FLOAT
        [GameParameters.CustomFloatParameterUI("Position Exponent", minValue = 0.0f, maxValue = 4.00f, 
          toolTip = "Larger values make it so controls have less effect when near zero and a much larger effect when farther from zero")]
#endif
        public float PositionExponent = 1.5f;
#if USE_ALT_FLOAT
        [GameParameters.CustomFloatParameterUI("Position Exponent", minValue = 0.0f, maxValue = 400.0f, 
          toolTip = "Larger values make it so controls have less effect when near zero and a much larger effect when farther from zero")]
        public float positionExponent
        {
            get { return PositionExponent * 100; }
            set { PositionExponent = value / 100f; }
        }
#endif

        [GameParameters.CustomStringParameterUI("", autoPersistance = true, lines = 2, title = "Zero Control Key", 
            toolTip = "Immediately center the controls by pressing this key")]
        public string ZeroControlsKey = "`";
  
        [GameParameters.CustomStringParameterUI("", autoPersistance = true, lines = 2, title = "Set Control Key", 
            toolTip = "Set to the current control positions by pressing ")]
        public string SetControlsKey = "y";


        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {
        }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }

}
