using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAnalysis.DomainObjects.DataSource
{
    /// <summary>
    /// Representation of a data connection saved to our data bases. This contains all the necesarry properties to connect to an external database.
    /// </summary>
    public class DataConnectionInfo
    {
        [Required]
        public long DataConnectionInfoId { get; set; }

        [Required]
        [Display(Name = "Server Name")]
        public string ServerName { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Database Name")]
        public string DatabaseName { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime ModifiedOn { get; set; }

        public DataConnectionType ConnectionType { get; set; }
    }
}
