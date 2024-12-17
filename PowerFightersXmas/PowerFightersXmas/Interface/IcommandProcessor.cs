﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Interface
{
    public interface ICommandProcessor
    {
        bool ProcessCommand(string command);
    }
}
