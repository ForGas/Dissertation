namespace Dissertation.Common.Services.CSIRP;

///<summary>Интерфейс плана по реагированию</summary>
public interface IPlan 
{
    string PathMap { get; set; }
    TimeSpan Duration { get; set; }
    string Content { get; set; }
}
