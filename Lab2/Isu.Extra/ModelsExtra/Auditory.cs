using Isu.Extra.Exceptions;

namespace Isu.Extra.ModelsExtra;

public class Auditory
{
    public Auditory(int auditoryNum)
    {
        if (auditoryNum < 0)
        {
            throw LessonException.InvalidAuditory(auditoryNum);
        }

        AuditoryNum = auditoryNum;
    }

    public int AuditoryNum { get; }
}