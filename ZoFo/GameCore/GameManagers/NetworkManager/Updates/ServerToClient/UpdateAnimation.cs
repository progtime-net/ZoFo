﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZoFo.GameCore.GameManagers.NetworkManager.Updates.ServerToClient
{
    public class UpdateAnimation : UpdateData          //хранит новую анимации
    {
        public UpdateAnimation() { UpdateType = "UpdateAnimation"; }
    }
}
