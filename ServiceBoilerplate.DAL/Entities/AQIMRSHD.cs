using Modules.AQE.AQI;
using ServiceBoilerplate.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBoilerplate.DAL.Entities
{
    /// <summary>
    /// 站点小时监测空气质量指数排名数据（发布数据）
    /// </summary>
    public class AQIMRSHD : IAQICalculate, ICodePeriodicRankEntity
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public int Rank { get; set; }
        /// <summary>
        /// 二氧化硫（SO2）平均浓度（μg/m³）
        /// </summary>
        public double? SO2 { get; set; }
        /// <summary>
        /// 二氧化氮（NO2）平均浓度（μg/m³）
        /// </summary>
        public double? NO2 { get; set; }
        /// <summary>
        /// 一氧化碳（CO）平均浓度（mg/m³）
        /// </summary>
        public double? CO { get; set; }
        /// <summary>
        /// 臭氧（O3）平均浓度（μg/m³）
        /// </summary>
        public double? O3 { get; set; }
        /// <summary>
        /// 颗粒物（粒径小于等于10μm）平均浓度（μg/m³）
        /// </summary>
        public double? PM10 { get; set; }
        /// <summary>
        /// 颗粒物（粒径小于等于2.5μm）平均浓度（μg/m³）
        /// </summary>
        public double? PM25 { get; set; }
        /// <summary>
        /// 空气质量指数
        /// </summary>
        public double? AQI { get; set; }
        /// <summary>
        /// 首要污染物
        /// </summary>
        public string PrimaryPollutant { get; set; }
        /// <summary>
        /// 空气质量指数类别
        /// </summary>
        public string Type { get; set; }
    }
}
