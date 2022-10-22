using Isu.Entities;
using Isu.Extra.EntitiesExtra;
using Isu.Extra.ModelsExtra;

namespace Isu.Extra.ServicesExtra;

public interface IIsuServiceExtra
{
    OgnpCourse AddOgnpCourse(string ognpName, Faculty faculty);
    void AddStudentToOgnp(OgnpCourse course, OgnpFlow flow, StudentExtra student);
    void RemoveStudentFromOgnp(OgnpFlow flow, StudentExtra student);
    IReadOnlyCollection<OgnpFlow> GetFlowsFromOgnp(OgnpCourse course);
    IReadOnlyCollection<Student> GetStudentsFromOgnpFlow(OgnpFlow flow);
    IReadOnlyCollection<Student> GetStudentsNotEnrolledInOgnp(GroupExtra group);
}