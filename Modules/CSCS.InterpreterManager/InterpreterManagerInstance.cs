﻿using SplitAndMerge;

namespace CSCS.InterpreterManager
{
    public class InterpreterManagerInstance : ICscsModuleInstance
    {
        public InterpreterManagerInstance(InterpreterManagerModule module, Interpreter interpreter)
        {
            interpreter.RegisterFunction("NewInterpreter", new NewInterpreterFunction(module));
            interpreter.RegisterFunction("RemoveInterpreter", new RemoveInterpreterFunction(module));
            interpreter.RegisterFunction("SetInterpreter", new SetInterpreterFunction(module));
            interpreter.RegisterFunction("GetInterpreterHandle", new GetInterpreterHandleFunction(module));
        }
    }
}
