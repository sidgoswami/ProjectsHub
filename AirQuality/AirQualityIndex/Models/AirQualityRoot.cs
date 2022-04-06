using System;
using System.Collections.Generic;

namespace AirQualityIndex.Models
{
    public class AirQualityRoot
    {
        public string index_name { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public string org_type { get; set; }
        public List<string> org { get; set; }
        public List<string> sector { get; set; }
        public string source { get; set; }
        public string catalog_uuid { get; set; }
        public string visualizable { get; set; }
        public int active { get; set; }
        public string created { get; set; }
        public int updated { get; set; }
        public DateTime created_date { get; set; }
        public DateTime updated_date { get; set; }
        public int external_ws { get; set; }
        public string external_ws_url { get; set; }
        public TargetBucket target_bucket { get; set; }
        public List<Field> field { get; set; }
        public string message { get; set; }
        public string version { get; set; }
        public string status { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public string limit { get; set; }
        public string offset { get; set; }
        public List<Record> records { get; set; }
    }
}
