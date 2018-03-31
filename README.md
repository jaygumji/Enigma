# Enigma
Enigma libary is used primarily for the Enigma Document DB to add core functionality. There is however a focus on making all library functionality completely generic, which can be used in many other scenarios.

Most of the functionality of this library already exist in other projects. The reason for me building this is primarily because I wanted to limit the number of dependencies Engima has and experiment with making these kind of libraries myself as a learning experience.

Please note that it's early alpha and no performance tuning has been done as of yet.

## Serialization
Enigma contains a serialization engine, and although there are plenty of those around, this one has a nice feature. The core part of the Enigma serialization is completely generic and can therefor rather easily be extended with addictional output and input formats.

The current version has support for the following formats:
- JSON
- Enigma Binary

More documentation will be added soon, see the Enigma.Serialization.Json.JsonSerializer for an example on how to do it. The interesting interfaces are IReadVisitor and IWriteVisitor setting up the visitor pattern used when serializing.

## Dependency injection
An IoC framework used in the library.
