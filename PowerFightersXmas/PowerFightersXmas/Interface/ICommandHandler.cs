using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Interface
{
    public interface ICommandHandler
    {
        bool ProcessCommand(string command);
    }
}
