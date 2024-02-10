using MPJBS.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models
{
    [Table(nameof(WorkImage))]
    public class WorkImage : ModelBase
    {
        //[WorkId] [int] NOT NULL REFERENCES WorkHistory(Id),
        //[ImagePath][nvarchar] (256) NOT NULL,
        public int WorkId { get; set; }
        public string? ImagePath { get; set; }
        [ForeignKey(nameof(WorkId))]
        public virtual WorkHistory? WorkHistory_ { get; set; }
    }
}
