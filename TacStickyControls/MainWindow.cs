﻿/**
 * MainWindow.cs
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
using UnityEngine;

namespace Tac.StickyControls
{
    internal class MainWindow : Window<MainWindow>
    {
        private readonly StickyControls main;
        private readonly Settings settings;
        private readonly string version;

        private GUIStyle labelStyle;
        private GUIStyle valueStyle;
        private GUIStyle buttonStyle;
        private GUIStyle versionStyle;

        private bool showSettings = false;

        internal MainWindow(StickyControls main, Settings settings)
            : base("TAC Sticky Controls", 155, 100)
        {
            base.HideCloseButton = true;
            base.Resizable = false;
            base.HideWhenPaused = false;

            this.main = main;
            this.settings = settings;
            this.version = Utilities.GetDllVersion(this);

            this.Log(this.GetType().Assembly.Location);
        }

        protected override void ConfigureStyles()
        {
            base.ConfigureStyles();

            if (labelStyle == null)
            {
                labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.alignment = TextAnchor.MiddleLeft;
                labelStyle.fontStyle = FontStyle.Normal;
                labelStyle.normal.textColor = Color.white;
                labelStyle.margin.top = 0;
                labelStyle.margin.bottom = 0;
                labelStyle.padding.top = 0;
                labelStyle.padding.bottom = 1;
                labelStyle.wordWrap = false;

                valueStyle = new GUIStyle(labelStyle);
                valueStyle.alignment = TextAnchor.MiddleRight;
                valueStyle.stretchWidth = true;

                buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.alignment = TextAnchor.MiddleCenter;
                buttonStyle.fontStyle = FontStyle.Normal;
                buttonStyle.normal.textColor = Color.white;
                buttonStyle.padding = new RectOffset(6, 2, 4, 2);
                buttonStyle.wordWrap = false;

                versionStyle = Utilities.GetVersionStyle();
            }
        }

        protected override void DrawWindowContents(int windowId)
        {
            Debug.Log("TacStickyControls.DrawWindowContents");
            settings.Enabled = GUILayout.Toggle(settings.Enabled, "Enabled");

            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical();
            GUILayout.Label("Yaw", labelStyle);
            GUILayout.Label("Pitch", labelStyle);
            GUILayout.Label("Roll", labelStyle);
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            GUILayout.Label(main.GetYaw().ToString("0.000") + " (" + main.GetRawYaw().ToString("0.000") + ")", valueStyle);
            GUILayout.Label(main.GetPitch().ToString("0.000") + " (" + main.GetRawPitch().ToString("0.000") + ")", valueStyle);
            GUILayout.Label(main.GetRoll().ToString("0.000") + " (" + main.GetRawRoll().ToString("0.000") + ")", valueStyle);
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            bool newShowSettings = GUILayout.Toggle(showSettings, "Settings", buttonStyle, GUILayout.ExpandWidth(true));
            if (newShowSettings != showSettings)
            {
                showSettings = newShowSettings;
                this.SetSize(155, 100);
            }

            if (showSettings)
            {
                float newFloat;

                // Movement scaling
                GUILayout.BeginHorizontal();
                GUILayout.Label("Speed", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed.ToString("0.000"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Speed, 0.025f, 2.0f, GUILayout.ExpandWidth(true)), 0.025f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Step", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step.ToString("0.000"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Step, 0.005f, 0.25f, GUILayout.ExpandWidth(true)), 0.005f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Precision Controls Modifier", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier.ToString("0.000"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PrecisionControlsModifier, 0.005f, 0.25f, GUILayout.ExpandWidth(true)), 0.005f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Minimum Time (s)", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime.ToString("0.00"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().MinTime, 0.01f, 0.5f, GUILayout.ExpandWidth(true)), 0.01f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Exponent", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent.ToString("0.00"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().Exponent, 0.0f, 4.0f, GUILayout.ExpandWidth(true)), 0.05f);

                // Position scaling
                GUILayout.BeginHorizontal();
                GUILayout.Label("Position Dead Zone", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone.ToString("0.000"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionDeadZone, 0.005f, 0.25f, GUILayout.ExpandWidth(true)), 0.005f);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Position Exponent", labelStyle, GUILayout.ExpandHeight(true));
                if (float.TryParse(GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent.ToString("0.00"), GUILayout.ExpandWidth(true)), out newFloat))
                {
                    HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent = newFloat;
                }
                GUILayout.EndHorizontal();
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent = StickyUtilities.RoundUp(GUILayout.HorizontalSlider((float)HighLogic.CurrentGame.Parameters.CustomParams<TSC>().PositionExponent, 0.0f, 4.0f, GUILayout.ExpandWidth(true)), 0.05f);

                // Key bindings
                GUILayout.BeginHorizontal();
                GUILayout.Label("Zero Controls key", labelStyle);
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey = GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().ZeroControlsKey, GUILayout.ExpandWidth(true));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Set Controls key", labelStyle);
                HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey = GUILayout.TextField(HighLogic.CurrentGame.Parameters.CustomParams<TSC>().SetControlsKey, GUILayout.ExpandWidth(true));
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                GUI.Label(new Rect(4, windowPos.height - 13, windowPos.width - 20, 12), "TAC Sticky Controls v" + version, versionStyle);
            }
        }
    }
}
