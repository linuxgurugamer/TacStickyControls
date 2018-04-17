
using UnityEngine;
using ToolbarControl_NS;

namespace Tac.StickyControls
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(StickyControls.MODID, StickyControls.MODNAME);
        }
    }
}