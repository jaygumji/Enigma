/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System.Reflection.Emit;

namespace Enigma.Reflection.Emit
{

    public class DelegatedILHandler<T> where T : ILPointer
    {
        private readonly ILGenerationMethodHandler<T> _handler;
        private readonly ILGenerationHandler<T> _parameterlessHandler;

        public DelegatedILHandler(ILGenerationMethodHandler<T> handler)
            : this(handler, null)
        {
        }

        public DelegatedILHandler(ILGenerationHandler<T> parameterlessHandler)
            : this(null, parameterlessHandler)
        {
        }

        public DelegatedILHandler(ILGenerationMethodHandler<T> handler, ILGenerationHandler<T> parameterlessHandler)
        {
            _handler = handler;
            _parameterlessHandler = parameterlessHandler;
        }

        public void Invoke(ILGenerator il, T parameter)
        {
            if (_handler != null) _handler.Invoke(il, parameter);
            else {
                _parameterlessHandler?.Invoke(parameter);
            }
        }
    }

    public class DelegatedILHandler
    {
        private readonly ILGenerationMethodHandler _handler;
        private readonly ILGenerationHandler _parameterlessHandler;

        public DelegatedILHandler(ILGenerationMethodHandler handler) : this(handler, null)
        {
        }

        public DelegatedILHandler(ILGenerationHandler parameterlessHandler) : this(null, parameterlessHandler)
        {
        }

        public DelegatedILHandler(ILGenerationMethodHandler handler, ILGenerationHandler parameterlessHandler)
        {
            _handler = handler;
            _parameterlessHandler = parameterlessHandler;
        }

        public void Invoke(ILGenerator il)
        {
            if (_handler != null) _handler.Invoke(il);
            else {
                _parameterlessHandler?.Invoke();
            }
        }

    }
}