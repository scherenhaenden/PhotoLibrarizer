using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoLibrarizerCli.Tools.Models
{
    public class Files
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public DateTime Inserted { get; set; }
        [Required]
        public string FileName { get; set; }

        public string Hash { get; set; }
    }
}
