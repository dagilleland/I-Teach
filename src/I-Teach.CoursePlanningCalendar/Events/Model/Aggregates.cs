using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I_Teach.CoursePlanningCalendar.Events.Model
{
    class Aggregate
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string Type { get; set; }
    }
    class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Guid AggregateId { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string Type { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string Body { get; set; }
        public int SequenceNumber { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
