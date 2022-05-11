using Dissertation.Persistence.Entities;

namespace Dissertation.Common.Services.CSIRP;

/// <summary>Определение инцидента безопасности</summary>
public interface IDefineSecurityIncident
{
    void Define(IIncident incident);
}

/// <summary>Эскалация инцидента безопасности</summary>
/// <typeparam name="TIncident"></typeparam>
public interface IEscalateSecurityIncident<TIncident>
    where TIncident : IIncident
{
    TIncident Incident { get; set; }
    void ResponsibilityVision();
    void SetPriory();
    void AnalyzePriorities(TIncident incident);
    void Communicate(TIncident incident);
}






