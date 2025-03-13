using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboJob
{
    public long Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string ModifiedBy { get; set; }

    public long? SysVersion { get; set; }

    public string Code { get; set; }

    public long Organization { get; set; }

    public long JobType { get; set; }

    public bool? IsHead { get; set; }

    public decimal? ProbationTerm { get; set; }

    public long? JobTitleLower { get; set; }

    public long? JobTitleUpper { get; set; }

    public decimal? JobOverLapTerm { get; set; }

    public int? JobOverLapTermType { get; set; }

    public int? DimissionBeforeDay { get; set; }

    public int? SalaryCalculateStyle { get; set; }

    public bool? IsBudgetControl { get; set; }

    public decimal? UpProportion { get; set; }

    public bool? IsGovControl { get; set; }

    public bool? IsAllowDeformity { get; set; }

    public bool? IsAllowRetire { get; set; }

    public bool? IsAllowForeigner { get; set; }

    public bool? EffectiveRangeIsEffective { get; set; }

    public DateTime? EffectiveRangeEffectiveDate { get; set; }

    public DateTime? EffectiveRangeDisableDate { get; set; }

    public int? AuditingStatus { get; set; }

    public long? KeyFlexFieldStru { get; set; }

    public string Segment1 { get; set; }

    public string Segment2 { get; set; }

    public string Segment3 { get; set; }

    public string Segment4 { get; set; }

    public string Segment5 { get; set; }

    public string Segment6 { get; set; }

    public string Segment7 { get; set; }

    public string Segment8 { get; set; }

    public string Segment9 { get; set; }

    public string Segment10 { get; set; }

    public string Segment11 { get; set; }

    public string Segment12 { get; set; }

    public string Segment13 { get; set; }

    public string Segment14 { get; set; }

    public string Segment15 { get; set; }

    public string Segment16 { get; set; }

    public string Segment17 { get; set; }

    public string Segment18 { get; set; }

    public string Segment19 { get; set; }

    public string Segment20 { get; set; }

    public int WfcurrentState { get; set; }

    public int WforiginalState { get; set; }

    public string DescFlexFieldPubDescSeg1 { get; set; }

    public string DescFlexFieldPubDescSeg2 { get; set; }

    public string DescFlexFieldPubDescSeg3 { get; set; }

    public string DescFlexFieldPubDescSeg4 { get; set; }

    public string DescFlexFieldPubDescSeg5 { get; set; }

    public string DescFlexFieldPubDescSeg6 { get; set; }

    public string DescFlexFieldPubDescSeg7 { get; set; }

    public string DescFlexFieldPubDescSeg8 { get; set; }

    public string DescFlexFieldPubDescSeg9 { get; set; }

    public string DescFlexFieldPubDescSeg10 { get; set; }

    public string DescFlexFieldPubDescSeg11 { get; set; }

    public string DescFlexFieldPubDescSeg12 { get; set; }

    public string DescFlexFieldPubDescSeg13 { get; set; }

    public string DescFlexFieldPubDescSeg14 { get; set; }

    public string DescFlexFieldPubDescSeg15 { get; set; }

    public string DescFlexFieldPubDescSeg16 { get; set; }

    public string DescFlexFieldPubDescSeg17 { get; set; }

    public string DescFlexFieldPubDescSeg18 { get; set; }

    public string DescFlexFieldPubDescSeg19 { get; set; }

    public string DescFlexFieldPubDescSeg20 { get; set; }

    public string DescFlexFieldPubDescSeg21 { get; set; }

    public string DescFlexFieldPubDescSeg22 { get; set; }

    public string DescFlexFieldPubDescSeg23 { get; set; }

    public string DescFlexFieldPubDescSeg24 { get; set; }

    public string DescFlexFieldPubDescSeg25 { get; set; }

    public string DescFlexFieldPubDescSeg26 { get; set; }

    public string DescFlexFieldPubDescSeg27 { get; set; }

    public string DescFlexFieldPubDescSeg28 { get; set; }

    public string DescFlexFieldPubDescSeg29 { get; set; }

    public string DescFlexFieldPubDescSeg30 { get; set; }

    public string DescFlexFieldPubDescSeg31 { get; set; }

    public string DescFlexFieldPubDescSeg32 { get; set; }

    public string DescFlexFieldPubDescSeg33 { get; set; }

    public string DescFlexFieldPubDescSeg34 { get; set; }

    public string DescFlexFieldPubDescSeg35 { get; set; }

    public string DescFlexFieldPubDescSeg36 { get; set; }

    public string DescFlexFieldPubDescSeg37 { get; set; }

    public string DescFlexFieldPubDescSeg38 { get; set; }

    public string DescFlexFieldPubDescSeg39 { get; set; }

    public string DescFlexFieldPubDescSeg40 { get; set; }

    public string DescFlexFieldPubDescSeg41 { get; set; }

    public string DescFlexFieldPubDescSeg42 { get; set; }

    public string DescFlexFieldPubDescSeg43 { get; set; }

    public string DescFlexFieldPubDescSeg44 { get; set; }

    public string DescFlexFieldPubDescSeg45 { get; set; }

    public string DescFlexFieldPubDescSeg46 { get; set; }

    public string DescFlexFieldPubDescSeg47 { get; set; }

    public string DescFlexFieldPubDescSeg48 { get; set; }

    public string DescFlexFieldPubDescSeg49 { get; set; }

    public string DescFlexFieldPubDescSeg50 { get; set; }

    public string DescFlexFieldContextValue { get; set; }

    public string DescFlexFieldPrivateDescSeg1 { get; set; }

    public string DescFlexFieldPrivateDescSeg2 { get; set; }

    public string DescFlexFieldPrivateDescSeg3 { get; set; }

    public string DescFlexFieldPrivateDescSeg4 { get; set; }

    public string DescFlexFieldPrivateDescSeg5 { get; set; }

    public string DescFlexFieldPrivateDescSeg6 { get; set; }

    public string DescFlexFieldPrivateDescSeg7 { get; set; }

    public string DescFlexFieldPrivateDescSeg8 { get; set; }

    public string DescFlexFieldPrivateDescSeg9 { get; set; }

    public string DescFlexFieldPrivateDescSeg10 { get; set; }

    public string DescFlexFieldPrivateDescSeg11 { get; set; }

    public string DescFlexFieldPrivateDescSeg12 { get; set; }

    public string DescFlexFieldPrivateDescSeg13 { get; set; }

    public string DescFlexFieldPrivateDescSeg14 { get; set; }

    public string DescFlexFieldPrivateDescSeg15 { get; set; }

    public string DescFlexFieldPrivateDescSeg16 { get; set; }

    public string DescFlexFieldPrivateDescSeg17 { get; set; }

    public string DescFlexFieldPrivateDescSeg18 { get; set; }

    public string DescFlexFieldPrivateDescSeg19 { get; set; }

    public string DescFlexFieldPrivateDescSeg20 { get; set; }

    public string DescFlexFieldPrivateDescSeg21 { get; set; }

    public string DescFlexFieldPrivateDescSeg22 { get; set; }

    public string DescFlexFieldPrivateDescSeg23 { get; set; }

    public string DescFlexFieldPrivateDescSeg24 { get; set; }

    public string DescFlexFieldPrivateDescSeg25 { get; set; }

    public string DescFlexFieldPrivateDescSeg26 { get; set; }

    public string DescFlexFieldPrivateDescSeg27 { get; set; }

    public string DescFlexFieldPrivateDescSeg28 { get; set; }

    public string DescFlexFieldPrivateDescSeg29 { get; set; }

    public string DescFlexFieldPrivateDescSeg30 { get; set; }

    public long? Org { get; set; }

    public long? MasterOrg { get; set; }

    public long? WorkCalendar { get; set; }
}
