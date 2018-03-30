﻿/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
namespace Enigma.IO
{
    /// <summary>
    /// The provider source state
    /// </summary>
    public enum StreamProviderSourceState
    {
        /// <summary>
        /// The provider created the source
        /// </summary>
        Created,

        /// <summary>
        /// The provider reconnected to the source
        /// </summary>
        Reconnected
    }
}