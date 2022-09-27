using System;
using System.Collections.Generic;
using System.Text;

namespace mail_to_plesanter
{

    public class PleasanterApiResponse<T>
    {
        public decimal? Id { get; set; }
        public decimal? StatusCode { get; set; }
        public string Message { get; set; }
        public PleasanterApiResponseData<T> Response { get; set; }
    }

    public class PleasanterApiResponseData<T>
    {
        public decimal? Offset { get; set; }
        public decimal? PageSize { get; set; }
        public decimal? TotalCount { get; set; }
        public T[] Data { get; set; }
    }

    public class PleasanterApiClassHash
    {
        public string ClassA { get; set; }
        public string ClassB { get; set; }
        public string ClassC { get; set; }
        public string ClassD { get; set; }
        public string ClassE { get; set; }
        public string ClassF { get; set; }
        public string ClassG { get; set; }
        public string ClassH { get; set; }
        public string ClassI { get; set; }
        public string ClassJ { get; set; }
        public string ClassK { get; set; }
        public string ClassL { get; set; }
        public string ClassM { get; set; }
        public string ClassN { get; set; }
        public string ClassO { get; set; }
        public string ClassP { get; set; }
        public string ClassQ { get; set; }
        public string ClassR { get; set; }
        public string ClassS { get; set; }
        public string ClassT { get; set; }
        public string ClassU { get; set; }
        public string ClassV { get; set; }
        public string ClassW { get; set; }
        public string ClassX { get; set; }
        public string ClassY { get; set; }
        public string ClassZ { get; set; }
    }

    public class PleasanterApiDateHash
    {
        public DateTimeOffset? DateA { get; set; }
        public DateTimeOffset? DateB { get; set; }
        public DateTimeOffset? DateC { get; set; }
        public DateTimeOffset? DateD { get; set; }
        public DateTimeOffset? DateE { get; set; }
        public DateTimeOffset? DateF { get; set; }
        public DateTimeOffset? DateG { get; set; }
        public DateTimeOffset? DateH { get; set; }
        public DateTimeOffset? DateI { get; set; }
        public DateTimeOffset? DateJ { get; set; }
        public DateTimeOffset? DateK { get; set; }
        public DateTimeOffset? DateL { get; set; }
        public DateTimeOffset? DateM { get; set; }
        public DateTimeOffset? DateN { get; set; }
        public DateTimeOffset? DateO { get; set; }
        public DateTimeOffset? DateP { get; set; }
        public DateTimeOffset? DateQ { get; set; }
        public DateTimeOffset? DateR { get; set; }
        public DateTimeOffset? DateS { get; set; }
        public DateTimeOffset? DateT { get; set; }
        public DateTimeOffset? DateU { get; set; }
        public DateTimeOffset? DateV { get; set; }
        public DateTimeOffset? DateW { get; set; }
        public DateTimeOffset? DateX { get; set; }
        public DateTimeOffset? DateY { get; set; }
        public DateTimeOffset? DateZ { get; set; }

    }

    public class PleasanterApiNumHash
    {
        public decimal? NumA { get; set; }
        public decimal? NumB { get; set; }
        public decimal? NumC { get; set; }
        public decimal? NumD { get; set; }
        public decimal? NumE { get; set; }
        public decimal? NumF { get; set; }
        public decimal? NumG { get; set; }
        public decimal? NumH { get; set; }
        public decimal? NumI { get; set; }
        public decimal? NumJ { get; set; }
        public decimal? NumK { get; set; }
        public decimal? NumL { get; set; }
        public decimal? NumM { get; set; }
        public decimal? NumN { get; set; }
        public decimal? NumO { get; set; }
        public decimal? NumP { get; set; }
        public decimal? NumQ { get; set; }
        public decimal? NumR { get; set; }
        public decimal? NumS { get; set; }
        public decimal? NumT { get; set; }
        public decimal? NumU { get; set; }
        public decimal? NumV { get; set; }
        public decimal? NumW { get; set; }
        public decimal? NumX { get; set; }
        public decimal? NumY { get; set; }
        public decimal? NumZ { get; set; }
    }

    public class PleasanterApiDescriptionHash
    {
        public string DescriptionA { get; set; }
        public string DescriptionB { get; set; }
        public string DescriptionC { get; set; }
        public string DescriptionD { get; set; }
        public string DescriptionE { get; set; }
        public string DescriptionF { get; set; }
        public string DescriptionG { get; set; }
        public string DescriptionH { get; set; }
        public string DescriptionI { get; set; }
        public string DescriptionJ { get; set; }
        public string DescriptionK { get; set; }
        public string DescriptionL { get; set; }
        public string DescriptionM { get; set; }
        public string DescriptionN { get; set; }
        public string DescriptionO { get; set; }
        public string DescriptionP { get; set; }
        public string DescriptionQ { get; set; }
        public string DescriptionR { get; set; }
        public string DescriptionS { get; set; }
        public string DescriptionT { get; set; }
        public string DescriptionU { get; set; }
        public string DescriptionV { get; set; }
        public string DescriptionW { get; set; }
        public string DescriptionX { get; set; }
        public string DescriptionY { get; set; }
        public string DescriptionZ { get; set; }
    }

    public class PleasanterApiCheckHash
    {
        public bool? CheckA { get; set; }
        public bool? CheckB { get; set; }
        public bool? CheckC { get; set; }
        public bool? CheckD { get; set; }
        public bool? CheckE { get; set; }
        public bool? CheckF { get; set; }
        public bool? CheckG { get; set; }
        public bool? CheckH { get; set; }
        public bool? CheckI { get; set; }
        public bool? CheckJ { get; set; }
        public bool? CheckK { get; set; }
        public bool? CheckL { get; set; }
        public bool? CheckM { get; set; }
        public bool? CheckN { get; set; }
        public bool? CheckO { get; set; }
        public bool? CheckP { get; set; }
        public bool? CheckQ { get; set; }
        public bool? CheckR { get; set; }
        public bool? CheckS { get; set; }
        public bool? CheckT { get; set; }
        public bool? CheckU { get; set; }
        public bool? CheckV { get; set; }
        public bool? CheckW { get; set; }
        public bool? CheckX { get; set; }
        public bool? CheckY { get; set; }
        public bool? CheckZ { get; set; }
    }

    public class PleasanterAttachments
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public decimal? Size { get; set; }
        public string HashCode { get; set; }
    }
    public class PleasanterApiAttachmentsHash
    {
        public List<PleasanterAttachments> AttachmentsA { get; set; }
        public List<PleasanterAttachments> AttachmentsB { get; set; }
        public List<PleasanterAttachments> AttachmentsC { get; set; }
        public List<PleasanterAttachments> AttachmentsD { get; set; }
        public List<PleasanterAttachments> AttachmentsE { get; set; }
        public List<PleasanterAttachments> AttachmentsF { get; set; }
        public List<PleasanterAttachments> AttachmentsG { get; set; }
        public List<PleasanterAttachments> AttachmentsH { get; set; }
        public List<PleasanterAttachments> AttachmentsI { get; set; }
        public List<PleasanterAttachments> AttachmentsJ { get; set; }
        public List<PleasanterAttachments> AttachmentsK { get; set; }
        public List<PleasanterAttachments> AttachmentsL { get; set; }
        public List<PleasanterAttachments> AttachmentsM { get; set; }
        public List<PleasanterAttachments> AttachmentsN { get; set; }
        public List<PleasanterAttachments> AttachmentsO { get; set; }
        public List<PleasanterAttachments> AttachmentsP { get; set; }
        public List<PleasanterAttachments> AttachmentsQ { get; set; }
        public List<PleasanterAttachments> AttachmentsR { get; set; }
        public List<PleasanterAttachments> AttachmentsS { get; set; }
        public List<PleasanterAttachments> AttachmentsT { get; set; }
        public List<PleasanterAttachments> AttachmentsU { get; set; }
        public List<PleasanterAttachments> AttachmentsV { get; set; }
        public List<PleasanterAttachments> AttachmentsW { get; set; }
        public List<PleasanterAttachments> AttachmentsX { get; set; }
        public List<PleasanterAttachments> AttachmentsY { get; set; }
        public List<PleasanterAttachments> AttachmentsZ { get; set; }
    }

    public class PleasanterApiItem
    {
        public long? SiteId { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public long? ResultId { get; set; }
        public long? IssueId { get; set; }
        public long? Ver { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public long? Status { get; set; }
        public long? Manager { get; set; }
        public long? Owner { get; set; }

        public PleasanterApiClassHash ClassHash { get; set; }

        public PleasanterApiNumHash NumHash { get; set; }

        public PleasanterApiDateHash DateHash { get; set; }

        public PleasanterApiDescriptionHash DescriptionHash { get; set; }

        public PleasanterApiCheckHash CheckHash { get; set; }

        public PleasanterApiAttachmentsHash AttachmentsHash { get; set; }
        public string Comments { get; set; }
        public decimal? Creator { get; set; }
        public decimal? Updator { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string ItemTitle { get; set; }
    }
}
