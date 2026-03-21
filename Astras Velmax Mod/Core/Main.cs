using Astras_Velmax_Mod.Core.GUIHelpers;
using Astras_Velmax_Mod.Core.Libs;
using Astras_Velmax_Mod.Core.Other;
using GorillaLocomotion;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

namespace Astras_Velmax_Mod.Core;

public class Main : MonoBehaviour
{
    private Rect MRect = new Rect(155, 155, 360, 460);
    private bool Open = false;
    private bool Init = false;
    private bool OpenDropdown = false;
    private Texture2D? Windowtex, Background, Slidertex, SliderThumbtex;
    private GUIStyle? WindowStyle, Buttonss, SliderStyle, SliderThumbStyle;
    private Color WindowColor = new Color(0.1f, 0.1f, 0.1f, 1f);
    private Color ButtonColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    private Color sliderTrackColor = new Color(0.15f, 0.15f, 0.15f, 1f);
    private Color sliderThumbColor = new Color(0.0f, 0.6f, 1f, 1f);
    private bool Velmax = false;
    private float VelMulti = 0f;
    private float VelMax = 0f;

    private void OnGUI()
    {
        if (!Init)
        {
            INIT();
            Init = true;
        }
        if (Open)
        {
            MRect.height = OpenDropdown ? 520 : 460;
            MRect = GUILayout.Window(6978545, MRect, UIM, "Astras Velmax Mod", WindowStyle);
        }
    }

    private void Update()
    {
        if (Keyboard.current.vKey.wasPressedThisFrame)
        {
            Open = !Open;
        }
    }

    private void FixedUpdate()
    {
        if (Velmax)
        {
            VelMaxMod();
        }
    }

    private void UIM(int id)
    {
        MMod();
        GUILayout.Space(5f);
        if (GUILayout.Button("Close", Buttonss))
        {
            Open = !Open;
        }
        GUI.DragWindow();
    }

    private void MMod()
    {
        GUILayout.Label("Enable Velmax");
        Velmax = GUILayout.Toggle(Velmax, "Enable Velmax");
        GUILayout.Space(5f);
        GUILayout.Label("Sliders");
        VelMulti = GUILayout.HorizontalSlider(VelMulti, 0.001f, 12f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"VelMult: {VelMulti:F3}");
        VelMax = GUILayout.HorizontalSlider(VelMax, 0.001f, 10f, SliderStyle, SliderThumbStyle);
        GUILayout.Label($"VelMax: {VelMax:F3}");
        GUILayout.Space(5f);
        GUILayout.Label("Change Input:");
        int OldIndex = InputSelector.SelectedIndex;
        InputSelector.SelectedIndex = MenuHelper.Dropdown(
            "VelMax",
            InputSelector.InputNames,
            InputSelector.SelectedIndex,
            GUILayout.Width(200)
        );
        OpenDropdown = OldIndex != InputSelector.SelectedIndex;
        GUILayout.Label($"Current input: {InputSelector.InputNames[InputSelector.SelectedIndex]}");
        GUILayout.Label("Presets:");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset", Buttonss))
        {
            VelMulti = 0.001f;
            VelMax = 0.001f;
        }
        if (GUILayout.Button("Max", Buttonss))
        {
            VelMulti = 12f;
            VelMax = 10f;
        }
        if (GUILayout.Button("Random", Buttonss))
        {
            VelMulti = UnityEngine.Random.Range(0.5f, 12f);
            VelMax = UnityEngine.Random.Range(0.5f, 10f);
        }
        GUILayout.EndHorizontal();
    }

    private void VelMaxMod()
    {
        if (GTPlayer.Instance == null) return;

        float mult = InputSelector.Pressed ? VelMulti : 1f;
        float max = InputSelector.Pressed ? VelMax : 1f;
        var Left = GTPlayer.Instance.LeftHand.surfaceOverride;
        var Right = GTPlayer.Instance.RightHand.surfaceOverride;
        if (Left != null)
        {
            Left.extraVelMultiplier = mult;
            Left.extraVelMaxMultiplier = max;
        }

        if (Right != null)
        {
            Right.extraVelMultiplier = mult;
            Right.extraVelMaxMultiplier = max;
        }
    }


    private void INIT()
    {
        Windowtex = Texturing.MakeTextures(1, 1, WindowColor);
        Background = Texturing.MakeTextures(1, 1, ButtonColor);
        Slidertex = Texturing.MakeTextures(1, 1, sliderTrackColor);
        SliderThumbtex = Texturing.MakeTextures(1, 1, sliderThumbColor);
        WindowStyle = new GUIStyle(GUI.skin.window);
        WindowStyle.normal.background = Windowtex;
        WindowStyle.hover.background = Windowtex;
        WindowStyle.active.background = Windowtex;
        WindowStyle.focused.background = Windowtex;
        WindowStyle.onNormal.background = Windowtex;
        WindowStyle.onHover.background = Windowtex;
        WindowStyle.onActive.background = Windowtex;
        WindowStyle.onFocused.background = Windowtex;
        WindowStyle.normal.textColor = Color.white;
        WindowStyle.fontStyle = FontStyle.Normal;
        Buttonss = new GUIStyle(GUI.skin.button);
        Buttonss.normal.background = Background;
        Buttonss.active.background = Background;
        Buttonss.hover.background = Background;
        Buttonss.focused.background = Background;
        Buttonss.onNormal.background = Background;
        Buttonss.onActive.background = Background;
        Buttonss.onHover.background = Background;
        Buttonss.onFocused.background = Background;
        Buttonss.normal.textColor = Color.white;
        Buttonss.hover.textColor = Color.blue;
        Buttonss.active.textColor = Color.red;
        Buttonss.focused.textColor = Color.white;
        Buttonss.onNormal.textColor = Color.blue;
        Buttonss.onHover.textColor = Color.blue;
        Buttonss.onActive.textColor = Color.blue;
        Buttonss.onFocused.textColor = Color.blue;
        SliderStyle = new GUIStyle(GUI.skin.horizontalSlider);
        SliderThumbStyle = new GUIStyle(GUI.skin.horizontalSliderThumb);
        SliderStyle.normal.background = Slidertex;
        SliderStyle.active.background = Slidertex;
        SliderStyle.hover.background = Slidertex;
        SliderThumbStyle.normal.background = SliderThumbtex;
        SliderThumbStyle.active.background = SliderThumbtex;
        SliderThumbStyle.hover.background = SliderThumbtex;
    }
}