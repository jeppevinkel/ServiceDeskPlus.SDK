using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ServiceDeskPlus.SDK.Models;

public class ResponseMessage
{
    [JsonPropertyName("status_code")] public int StatusCode { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("message")] public string? Message { get; set; }
}

public class ResponseStatus
{
    [JsonPropertyName("status_code")] public int StatusCode { get; set; }
    [JsonPropertyName("status")] public string? Status { get; set; }
    [JsonPropertyName("messages")] public List<ResponseMessage>? Messages { get; set; }
}

public class ListInfo
{
    [JsonPropertyName("has_more_rows")] public bool HasMoreRows { get; set; }
    [JsonPropertyName("start_index")] public int StartIndex { get; set; }
    [JsonPropertyName("row_count")] public int RowCount { get; set; }
}

// ---- Request model scaffold (minimal, based on samples/docs) ----
public class ContentUrlRef
{
    [JsonPropertyName("content-url")] public string? ContentUrl { get; set; }
}

public class BasicRef
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
}

public class ColoredRef : BasicRef
{
    [JsonPropertyName("color")] public string? Color { get; set; }
}

public class SiteRef
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
}

public class DepartmentRef : BasicRef
{
    [JsonPropertyName("site")] public SiteRef? Site { get; set; }
    [JsonPropertyName("deleted")] public bool? Deleted { get; set; }
}

public class GroupRef : BasicRef
{
    [JsonPropertyName("site")] public SiteRef? Site { get; set; }
}

public class ServiceCategoryRef : BasicRef
{
    [JsonPropertyName("icon_name")] public ContentUrlRef? IconName { get; set; }
}

public class TemplateRef : BasicRef
{
    [JsonPropertyName("is_service_template")] public bool? IsServiceTemplate { get; set; }
    [JsonPropertyName("service_category")] public ServiceCategoryRef? ServiceCategory { get; set; }
}

public class TimeValue
{
    [JsonPropertyName("display_value")] public string? DisplayValue { get; set; }
    // value appears as string in samples (epoch millis as string)
    [JsonPropertyName("value")] public string? Value { get; set; }
}

public class UserRef
{
    [JsonPropertyName("email_id")] public string? EmailId { get; set; }
    [JsonPropertyName("phone")] public string? Phone { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("mobile")] public string? Mobile { get; set; }
    [JsonPropertyName("profile_pic")] public ContentUrlRef? ProfilePic { get; set; }
    [JsonPropertyName("is_vipuser")] public bool? IsVipUser { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("department")] public DepartmentRef? Department { get; set; }
}

public class Resolution
{
    [JsonPropertyName("resolution_attachments")] public List<object>? ResolutionAttachments { get; set; }
    [JsonPropertyName("content")] public string? Content { get; set; }
}

public class RequestRecord
{
    [JsonPropertyName("ola_due_by_time")] public TimeValue? OlaDueByTime { get; set; }
    [JsonPropertyName("resolution")] public Resolution? Resolution { get; set; }
    [JsonPropertyName("onhold_time")] public TimeValue? OnHoldTime { get; set; }
    [JsonPropertyName("is_trashed")] public bool? IsTrashed { get; set; }
    [JsonPropertyName("fr_sla_violated_group")] public GroupRef? FrSlaViolatedGroup { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("assigned_time")] public TimeValue? AssignedTime { get; set; }
    [JsonPropertyName("requester")] public UserRef? Requester { get; set; }
    [JsonPropertyName("cancel_requested_by")] public UserRef? CancelRequestedBy { get; set; }
    [JsonPropertyName("sla_violated_technician")] public UserRef? SlaViolatedTechnician { get; set; }
    [JsonPropertyName("item")] public BasicRef? Item { get; set; }
    [JsonPropertyName("has_resolution_attachments")] public bool? HasResolutionAttachments { get; set; }
    [JsonPropertyName("impact")] public BasicRef? Impact { get; set; }
    [JsonPropertyName("sla")] public BasicRef? Sla { get; set; }
    [JsonPropertyName("priority")] public ColoredRef? Priority { get; set; }
    [JsonPropertyName("sla_violated_group")] public GroupRef? SlaViolatedGroup { get; set; }
    [JsonPropertyName("tags")] public List<string>? Tags { get; set; }
    [JsonPropertyName("has_notes")] public bool? HasNotes { get; set; }
    [JsonPropertyName("is_current_ola_violated")] public bool? IsCurrentOlaViolated { get; set; }
    [JsonPropertyName("image_token")] public string? ImageToken { get; set; }
    [JsonPropertyName("status")] public ColoredRef? Status { get; set; }
    [JsonPropertyName("template")] public TemplateRef? Template { get; set; }
    [JsonPropertyName("primary_asset")] public BasicRef? PrimaryAsset { get; set; }
    [JsonPropertyName("attachments")] public List<object>? Attachments { get; set; }
    [JsonPropertyName("request_type")] public string? RequestType { get; set; }
    [JsonPropertyName("cancel_requested_time")] public TimeValue? CancelRequestedTime { get; set; }
    [JsonPropertyName("chat_type")] public int? ChatType { get; set; }
    [JsonPropertyName("is_service_request")] public bool? IsServiceRequest { get; set; }
    [JsonPropertyName("cancel_requested")] public bool? CancelRequested { get; set; }
    [JsonPropertyName("has_request_initiated_change")] public bool? HasRequestInitiatedChange { get; set; }
    [JsonPropertyName("has_attachments")] public bool? HasAttachments { get; set; }
    [JsonPropertyName("has_linked_requests")] public bool? HasLinkedRequests { get; set; }
    [JsonPropertyName("has_request_caused_by_change")] public bool? HasRequestCausedByChange { get; set; }
    [JsonPropertyName("has_problem")] public bool? HasProblem { get; set; }
    [JsonPropertyName("subject")] public string? Subject { get; set; }
    [JsonPropertyName("linked_to_request")] public BasicRef? LinkedToRequest { get; set; }
    [JsonPropertyName("mode")] public BasicRef? Mode { get; set; }
    [JsonPropertyName("is_read")] public bool? IsRead { get; set; }
    [JsonPropertyName("lifecycle")] public object? Lifecycle { get; set; }
    [JsonPropertyName("reason_for_cancel")] public string? ReasonForCancel { get; set; }
    [JsonPropertyName("assets")] public List<object>? Assets { get; set; }
    [JsonPropertyName("group")] public GroupRef? Group { get; set; }
    [JsonPropertyName("email_to")] public List<string>? EmailTo { get; set; }
    [JsonPropertyName("created_time")] public TimeValue? CreatedTime { get; set; }
    [JsonPropertyName("level")] public BasicRef? Level { get; set; }
    [JsonPropertyName("approval_status")] public string? ApprovalStatus { get; set; }
    [JsonPropertyName("service_category")] public ServiceCategoryRef? ServiceCategory { get; set; }
    [JsonPropertyName("created_by")] public UserRef? CreatedBy { get; set; }
    [JsonPropertyName("scheduled_end_time")] public TimeValue? ScheduledEndTime { get; set; }
    [JsonPropertyName("first_response_due_by_time")] public TimeValue? FirstResponseDueByTime { get; set; }
    [JsonPropertyName("last_updated_time")] public TimeValue? LastUpdatedTime { get; set; }
    [JsonPropertyName("impact_details")] public object? ImpactDetails { get; set; }
    [JsonPropertyName("subcategory")] public BasicRef? Subcategory { get; set; }
    [JsonPropertyName("email_cc")] public List<string>? EmailCc { get; set; }
    [JsonPropertyName("scheduled_start_time")] public TimeValue? ScheduledStartTime { get; set; }
    [JsonPropertyName("email_ids_to_notify")] public List<string>? EmailIdsToNotify { get; set; }
    [JsonPropertyName("notification_status")] public string? NotificationStatus { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("has_dependency")] public bool? HasDependency { get; set; }
    [JsonPropertyName("has_conversation")] public bool? HasConversation { get; set; }
    [JsonPropertyName("fr_sla_violated_technician")] public UserRef? FrSlaViolatedTechnician { get; set; }
    [JsonPropertyName("callback_url")] public string? CallbackUrl { get; set; }
    [JsonPropertyName("urgency")] public BasicRef? Urgency { get; set; }
    [JsonPropertyName("is_shared")] public bool? IsShared { get; set; }
    [JsonPropertyName("request_template_task_ids")] public List<string>? RequestTemplateTaskIds { get; set; }
    [JsonPropertyName("department")] public DepartmentRef? Department { get; set; }
    [JsonPropertyName("is_reopened")] public bool? IsReopened { get; set; }
    [JsonPropertyName("has_draft")] public bool? HasDraft { get; set; }
    [JsonPropertyName("is_overdue")] public bool? IsOverdue { get; set; }
    [JsonPropertyName("technician")] public UserRef? Technician { get; set; }
    [JsonPropertyName("due_by_time")] public TimeValue? DueByTime { get; set; }
    [JsonPropertyName("has_project")] public bool? HasProject { get; set; }
    [JsonPropertyName("is_first_response_overdue")] public bool? IsFirstResponseOverdue { get; set; }
    [JsonPropertyName("cancel_requested_is_pending")] public bool? CancelRequestedIsPending { get; set; }
    [JsonPropertyName("recommend_template")] public string? RecommendTemplate { get; set; }
    [JsonPropertyName("unreplied_count")] public int? UnrepliedCount { get; set; }
    [JsonPropertyName("category")] public BasicRef? Category { get; set; }
    [JsonPropertyName("maintenance")] public string? Maintenance { get; set; }

    // List response-only or summary fields
    [JsonPropertyName("short_description")] public string? ShortDescription { get; set; }
    [JsonPropertyName("time_elapsed")] public string? TimeElapsed { get; set; }
    [JsonPropertyName("response_time_elapsed")] public string? ResponseTimeElapsed { get; set; }
    [JsonPropertyName("site")] public SiteRef? Site { get; set; }
}

public class RequestGetResponse
{
    [JsonPropertyName("request")] public RequestRecord? Request { get; set; }

    [JsonPropertyName("response_status")] public ResponseStatus? ResponseStatus { get; set; }
}

public class RequestListResponse
{
    [JsonPropertyName("response_status")] public List<ResponseStatus>? ResponseStatus { get; set; }

    [JsonPropertyName("list_info")] public ListInfo? ListInfo { get; set; }

    [JsonPropertyName("requests")] public List<RequestRecord>? Requests { get; set; }
}
