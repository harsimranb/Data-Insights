using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnalysis.DomainObjects.Common
{
    /// <summary>
    /// The main project object, where everything starts.
    /// </summary>
    public class Project
    {
        public readonly List<DataSource.DataSource> DataSources = new List<DataSource.DataSource>();

        [DisplayName("Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime ModifiedOn { get; set; }
    }
}
