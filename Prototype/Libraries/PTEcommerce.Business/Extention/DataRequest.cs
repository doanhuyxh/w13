using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTEcommerce.Business
{
    public class DataRequest
    {
        public DataRequest(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
    public class DeleteRequest : DataRequest
    {
        public DeleteRequest(int id) : base(id)
        {
        }
    }
    public class DeleteSelectMessage<TKey>
    {
        public int Id { get; set; }
        public TKey[] Ids { get; set; }
    }
    public class PagingRequest
    {
        public int objId { get; set; }
        public string Filter { get; set; }
        public string Keyword { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Sort { get; set; }
        public string FirstCharCode { get; set; }
        public byte Type { get; set; }
    }
    public class FilterRequestMessage
    {
        public int CompanyId { get; set; }
        public string Category { get; set; }
        public string Keyword { get; set; }
        public bool IsPrint { get; set; }
        public int FilterDate { get; set; }
        public long ProductId { get; set; }
    }
    public class PrintSelectMessage : DeleteSelectMessage<long>
    {
        public int type { get; set; }
        public int CompanyId { get; set; }
    }
    public class ExportRequest
    {
        public int type { get; set; }
        public long[] ids { get; set; }
        public string serverPath { get; set; }
        public string destinationPath { get; set; }
        public string imageBarcodePath { get; set; }
        public string ext { get; set; }

    }
    public class ImportRequest
    {
        public string columnCode { get; set; }
        public string columnName { get; set; }
        public string categoryName { get; set; }
        public string qty { get; set; }
        public string madein { get; set; }
        public string size { get; set; }
        public string marker { get; set; }
    }
}