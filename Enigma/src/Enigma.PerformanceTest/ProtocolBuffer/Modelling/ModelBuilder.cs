/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
namespace Enigma.Modelling
{
    public class ModelBuilder
    {

        private readonly Model _model;

        public ModelBuilder()
        {
            _model = new Model();
        }

        public ModelBuilder(Model model)
        {
            _model = model;
        }

        public EntityBuilder<T> Entity<T>()
        {
            return new EntityBuilder<T>(_model.Entity<T>());
        }

    }
}
