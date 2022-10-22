using Isu.Entities;
using Isu.Extra.EntitiesExtra;
using Isu.Extra.ModelsExtra;

namespace Isu.Extra.ServicesExtra;

public interface IIsuServiceExtra
{
    OgnpCourse AddOgnpCourse(string ognpName, Faculty faculty);
    void AddStudentToOgnp(OgnpCourse course, OgnpFlow flow, StudentExtra student);
    void RemoveStudentFromOgnp(OgnpFlow flow, StudentExtra student);
    IEnumerable<OgnpFlow> GetFlowsFromOgnp(OgnpCourse course);
    IEnumerable<Student> GetStudentsFromOgnpFlow(OgnpFlow flow);
    IEnumerable<Student> GetStudentsNotEnrolledInOgnp(GroupExtra group);
}