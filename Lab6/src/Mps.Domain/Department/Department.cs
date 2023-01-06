using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Mps.Domain.Device;
using Mps.Domain.Exceptions;
using Mps.Domain.Message;
using Mps.Domain.ValueObjects;

namespace Mps.Domain.Department;

public class Department
{
    private readonly List<Employee> _plebEmployees = new List<Employee>();
    private readonly List<DeviceBase> _controlledDevices = new List<DeviceBase>();
    private readonly List<Report> _reports = new List<Report>();

    public Department(Guid id, DepartmentName departmentName)
    {
        ArgumentNullException.ThrowIfNull(departmentName);
        Id = id;
        DepartmentName = departmentName;
    }

    protected Department()
    {
        DepartmentName = null!;
    }

    public Guid Id { get; private set; }
    public DepartmentName DepartmentName { get; private set; }
    public virtual BossEmployee? DepartmentBoss { get; private set; }
    public virtual IReadOnlyCollection<Employee> PlebEmployees => _plebEmployees;
    public virtual IReadOnlyCollection<Report> Reports => _reports;
    public virtual IReadOnlyCollection<DeviceBase> ControlledDevices => _controlledDevices;

    public void SetDepartmentBoss(BossEmployee departmentBoss)
    {
        ArgumentNullException.ThrowIfNull(departmentBoss);
        DepartmentBoss = departmentBoss;
    }

    public void AddPlebEmployee(Employee plebEmplyee)
    {
        ArgumentNullException.ThrowIfNull(plebEmplyee);
        _plebEmployees.Add(plebEmplyee);
    }

    public void AddControlledDevice(DeviceBase device)
    {
        ArgumentNullException.ThrowIfNull(device);
        _controlledDevices.Add(device);
    }

    public IReadOnlyCollection<DeviceBase> GetControlledDevices()
    {
        return ControlledDevices;
    }

    public void AddReport(Report report)
    {
        ArgumentNullException.ThrowIfNull(report);
        _reports.Add(report);
    }

    public IReadOnlyCollection<Report> GetReportsFromDate(DateTime date)
    {
        return Reports.Where(r => r.DateCreated >= date).ToImmutableList();
    }

    public Report GetReportBy(Guid reportId)
    {
        var report = Reports.FirstOrDefault(r => r.Id.Equals(reportId));
        if (report is null)
        {
            throw new MpsDomainException($"Report with id {reportId} not found");
        }

        return report;
    }

    public Report FormAndAddReportFromMessageDate(DateTime messageDate, DateTime reportDate)
    {
        var messagesTotal = ControlledDevices
            .SelectMany(d => d.Messages)
            .Where(message => message.DateReceived >= messageDate).ToList();

        var messagesRead = messagesTotal.Where(m => m.MessageState == MessageState.Read).ToList();
        var messagesProcessed = messagesTotal.Where(m => m.MessageState == MessageState.Processed).ToList();
        var messageCountByDevices = ControlledDevices
            .Select(device => new
                {
                    deviceId = device.Id,
                    messages = device.Messages.Where(m => m.DateReceived >= messageDate).ToList(),
                })
            .Select(d => new MessageCountByDevice(d.deviceId, d.messages.Count));

        var report = new Report(
            Guid.NewGuid(),
            reportDate,
            new MessageCount(messagesTotal.Count),
            new MessageCount(messagesRead.Count),
            new MessageCount(messagesProcessed.Count),
            messageCountByDevices.ToImmutableList());

        AddReport(report);
        return report;
    }
}