using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZoFo.GameCore.GameManagers;
using ZoFo.GameCore.GUI;

namespace ZoFo.GameFolder
{
    public class AppManager_Inst : AppManager
    {
        public virtual void OnAppStart_PostInit() => SoundManager.StartAmbientSound("Background menu music");
        public virtual void InitializationGUI() => currentGUI = new MainMenuGUI();
    }
}
