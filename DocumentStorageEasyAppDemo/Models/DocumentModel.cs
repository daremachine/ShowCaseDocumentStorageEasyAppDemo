using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace DocumentStorageEasyAppDemo.Models
{
    public class DocumentModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string[] Tags { get; set; } = Array.Empty<string>();
        public object? Data { get; set; }
    }
}
