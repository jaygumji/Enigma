/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/. */
namespace Enigma.Db.Embedded
{
    public class EmbeddedEnigmaConnection : IEnigmaConnection
    {
        public IEnigmaCommand CreateCommand()
        {
            return new EmbeddedEnigmaCommand(this);
        }
    }

    public class EmbeddedEnigmaCommand : IEnigmaCommand
    {
        private readonly EmbeddedEnigmaConnection _connection;

        public EmbeddedEnigmaCommand(EmbeddedEnigmaConnection connection)
        {
            _connection = connection;
        }

        public void Add(string name, BufferedCommandHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public void Modify(string name, BufferedCommandHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string name, BufferedCommandHandler handler)
        {
            throw new System.NotImplementedException();
        }
    }
}
