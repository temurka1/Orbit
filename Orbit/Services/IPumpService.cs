using Orbit.Data;

namespace Orbit.Services
{
    public interface IPumpService
    {
        PumpCommands LoadCommands(string path);
        PumpStates   LoadStates(string path);
    }
}
