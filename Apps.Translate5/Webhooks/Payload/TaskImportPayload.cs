using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Translate5.Webhooks.Payload
{
    public class TaskImportPayload
    {
        public Task Task { get; set; }
        public List<Tua> Tua { get; set; }
    }

    public class Task
    {
        public string Id { get; set; }
        public string EntityVersion { get; set; }
        public string Modified { get; set; }
        public string TaskGuid { get; set; }
        public string TaskNr { get; set; }
        public string ForeignId { get; set; }
        public string TaskName { get; set; }
        public string ForeignName { get; set; }
        public string SourceLang { get; set; }
        public string TargetLang { get; set; }
        public string RelaisLang { get; set; }
        public string State { get; set; }
        public string Workflow { get; set; }
        public string WorkflowStep { get; set; }
        public string WorkflowStepName { get; set; }
        public string PmGuid { get; set; }
        public string PmName { get; set; }
        public string WordCount { get; set; }
        public string UserCount { get; set; }
        public string ReferenceFiles { get; set; }
        public string Terminologie { get; set; }
        public string Orderdate { get; set; }
        public string EnableSourceEditing { get; set; }
        public string Edit100PercentMatch { get; set; }
        public string LockLocked { get; set; }
        public string EmptyTargets { get; set; }
        public string ImportAppVersion { get; set; }
        public string CustomerId { get; set; }
        public string UsageMode { get; set; }
        public string SegmentCount { get; set; }
        public string SegmentFinishCount { get; set; }
        public string TaskType { get; set; }
        public string ProjectId { get; set; }
        public string DiffExportUsable { get; set; }
        public string Reimportable { get; set; }
        public string Description { get; set; }
        public string Created { get; set; }
    }

    public class Tua
    {
        public string Id { get; set; }
        public string TaskGuid { get; set; }
        public string UserGuid { get; set; }
        public string State { get; set; }
        public string Role { get; set; }
        public string WorkflowStepName { get; set; }
        public string Workflow { get; set; }
        public string Segmentrange { get; set; }
        public string IsPmOverride { get; set; }
        public object DeadlineDate { get; set; }
        public string AssignmentDate { get; set; }
        public string TrackchangesShow { get; set; }
        public string TrackchangesShowAll { get; set; }
        public string TrackchangesAcceptReject { get; set; }
    }
}
