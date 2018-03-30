/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
using System;
using Enigma.Testing.Fakes.Entities;

namespace Enigma.Test.Serialization.Fakes
{
    public class NullableValuesEntity
    {
        public int Id { get; set; }
        public bool? MayBool { get; set; }
        public int? MayInt { get; set; }
        public DateTime? MayDateTime { get; set; }
        public TimeSpan? MayTimeSpan { get; set; }
        public ApplicationType? Type { get; set; }
    }
}
