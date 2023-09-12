using Blackbird.Applications.Sdk.Common;

namespace Apps.Translate5.Webhooks.Payload;

public class TaskPayload
{
    [Display("ID")]
    public string Id { get; set; }

    [Display("Entity version")]
    public string EntityVersion { get; set; }

    [Display("Modified")]
    public string Modified { get; set; }

    [Display("Task GUID")]
    public string TaskGuid { get; set; }

    [Display("Task number")]
    public string TaskNr { get; set; }

    [Display("Foreign ID")]
    public string ForeignId { get; set; }

    [Display("Task name")]
    public string TaskName { get; set; }

    [Display("Foreign name")]
    public string ForeignName { get; set; }

    [Display("Source language")]
    public string SourceLang { get; set; }

    [Display("Target language")]
    public string TargetLang { get; set; }

    [Display("Relais language")]
    public string RelaisLang { get; set; }

    [Display("State")]
    public string State { get; set; }

    [Display("Workflow")]
    public string Workflow { get; set; }

    [Display("Workflow step")]
    public string WorkflowStep { get; set; }

    [Display("Workflow step name")]
    public string WorkflowStepName { get; set; }

    [Display("Project manager GUID")]
    public string PmGuid { get; set; }

    [Display("Project manager name")]
    public string PmName { get; set; }

    [Display("Word count")]
    public string WordCount { get; set; }

    [Display("User count")]
    public string UserCount { get; set; }

    [Display("Reference files")]
    public string ReferenceFiles { get; set; }

    [Display("Terminologie")]
    public string Terminologie { get; set; }

    [Display("Order date")]
    public string Orderdate { get; set; }

    [Display("Enable source editing")]
    public string EnableSourceEditing { get; set; }

    [Display("Edit 100 percent match")]
    public string Edit100PercentMatch { get; set; }

    [Display("Lock locked")]
    public string LockLocked { get; set; }

    [Display("Empty targets")]
    public string EmptyTargets { get; set; }

    [Display("Import app version")]
    public string ImportAppVersion { get; set; }

    [Display("Customer ID")]
    public string CustomerId { get; set; }

    [Display("Usage mode")]
    public string UsageMode { get; set; }

    [Display("Segment count")]
    public string SegmentCount { get; set; }

    [Display("Segment finish count")]
    public string SegmentFinishCount { get; set; }

    [Display("Task type")]
    public string TaskType { get; set; }

    [Display("Project ID")]
    public string ProjectId { get; set; }

    [Display("Diff export usable")]
    public string DiffExportUsable { get; set; }

    [Display("Reimportable")]
    public string Reimportable { get; set; }

    [Display("Description")]
    public string Description { get; set; }

    [Display("Created")]
    public string Created { get; set; }
}