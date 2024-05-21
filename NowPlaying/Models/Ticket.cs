using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace NowPlaying.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        [ValidateNever]
        public Movie Movie { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public IdentityUser ApplicationUser { get; set; }
    }
}
